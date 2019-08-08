using Black.PlayerUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        public class HitDmg : MonoBehaviour
        {
            CharactersData charData;
            CharactersAniCtrl aniCtrl;
            [SerializeField]
            bool isPlayer = false;
            UIBar playerUI = null;

            MemoryPooling pool;

            AudioSource _audio;
            [SerializeField,Header("피격 효과음")]
            AudioClip[] _sfx;

            private void Start()
            {
                charData = GetComponent<CharactersData>();
                aniCtrl = charData.GetComponent<CharactersAniCtrl>();
                pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();

                _audio = GetComponent<AudioSource>();

          
                if (isPlayer)
                    playerUI = GetComponent<UIBar>();
            }

            /// <summary>
            /// 피격 받음
            /// </summary>
            /// <param name="dmg"></param>
            public void HitDmage(Transform target, int dmg)
            {
                int ranSfx = Random.Range(0, _sfx.Length);
                Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[ranSfx]);


                //피격
                if (charData.NHp > 0 && charData.IsLive && !charData.IsRoll)
                {
                    charData.NHp -= dmg;

                    //피격 애니메이션 실행
                    //(기절 상태가 아닐때, 스킬을 사용하고 있지 않을때)         
                    //Player hit 애니메이션으로 다중 적에게 공격 받으면 빠져나올수가 없어 애니를 실행하지 않는다
                    if (!charData.IsStun && !charData.IsSkill && !isPlayer)
                        StartCoroutine(HitDelay());

                    charData.Nav.velocity = Vector3.zero;
                    charData.Nav.isStopped = true;

                    
                }

                //다운
                if(charData.NHp <=0 && charData.IsLive)
                {
                    charData.IsLive = false;
                    charData.IsStun = false; //기절 상태 해제 후 쓰러짐
                    charData.IsWalk = false;
                    charData.IsRun = false;
                    charData.IsAttack = false;
                    charData.IsSkill = false;
                    charData.IsReload = false;

                    charData.NHp = 0;

                    //aniCtrl.AniStun(charData.IsStun);
                    //쓰러진는 애니메이션
                    charData.AniCtrl.AniDown();

                    if (!isPlayer)
                    {
                        StartCoroutine(EnemyDis());
                    }

                        
                }

                //플레이어에 적용 되어 있을 경우
                //피격 시 UI 동기화
                if(isPlayer)
                {
                    playerUI.HpBar();

                    PlayerCtrl shake = GetComponent<PlayerCtrl>();
                    shake.ShakeCam.ShakeCamAct(0.1f,0.3f,0.3f); //피격 효과
                    
                    //Post 프로세싱 효과 추가

                }

                //적 캐릭터일때
                else if(!isPlayer)
                {
                    //Boss 캐릭터이면
                    BossUi ui = GetComponent<BossUi>();

                    if(ui != null)
                    {
                        ui.HPFill(charData.NHp, charData.NMaxHp);
                    }
                }
                
                if(charData.IsLive)
                {
                    if (!charData.IsRoll)
                        DmgValUI<int>(target, dmg);

                    else if (charData.IsRoll)
                        DmgValUI<int>(target, 0);
                }
            }

            /// <summary>
            /// 피격 당할때 잠깐 정지 시킨다(적 캐릭터만 해당)
            /// </summary>
            /// <returns></returns>
            private IEnumerator HitDelay()
            {
                charData.AniCtrl.AniHit();
                charData.IsHit = true;
                yield return new WaitForSeconds(1.0f);
                charData.IsHit = false;
            }

            //기절 효과 발동 시
            //이동 불가능 상태 딜레이
            public void StunDelayAni(float stunPer)
            {
                if (charData.NHp > 0 && charData.IsLive)
                {
                    if (StunPer(stunPer))
                        StartCoroutine(StunDelay(2));
                }
                    
            }
            
            /// <summary>
            /// 행동 불가능 상태 시간
            /// </summary>
            /// <param name="delay"></param>
            /// <returns></returns>
            private IEnumerator StunDelay(float delay)
            {
                charData.IsStun = true;
                aniCtrl.AniStun(charData.IsStun);

                charData.Condition.StunEffect();

                yield return new WaitForSeconds(delay);
                charData.IsStun = false;

                charData.Condition.StunEffecStop();

                if (charData.IsLive)
                    aniCtrl.AniStun(charData.IsStun);

            }

            /// <summary>
            /// 기절 효과 확률
            /// </summary>
            /// <returns></returns>
            private bool StunPer(float stunPer)
            {                
                float ran = Random.Range(0, 100);

                if (ran <= stunPer)
                    return true;
                else
                    return false;

            }

            /// <summary>
            /// 적 캐릭터 HP가 0이 되면
            /// 잠시 후 비활성화 시킨다
            /// </summary>
            /// <returns></returns>
            private IEnumerator EnemyDis()
            {
                GetComponent<CapsuleCollider>().enabled = false; //콜라이더를 끈다

                EnemyCtrl enemy = GetComponent<EnemyCtrl>();
                enemy.EnableObj();

                yield return new WaitForSeconds(3f);

                //Boss 캐릭터이면
                BossUi ui = GetComponent<BossUi>();
                if (ui != null)
                    ui.UiOff();

                gameObject.SetActive(false);
            }

            /// <summary>
            /// 데미지 수치  UI 출력
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="target"></param>
            /// <param name="dmg"></param>
            private void DmgValUI<T>(Transform target, T dmg)
            {
                GameObject obj = pool.GetObjPool(pool.DmgUICount, pool.DmgUIList);

                if (obj != null)
                {
                    obj.transform.position = new Vector3(target.position.x, target.position.y + 1, target.position.z);
                    obj.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                 
                    obj.SetActive(true);

                    DmgValUI ui = obj.GetComponent<DmgValUI>();
                    if (ui != null)
                        StartCoroutine(ui.DmageValueUI(dmg));

                }


            }

        }

    }
}
