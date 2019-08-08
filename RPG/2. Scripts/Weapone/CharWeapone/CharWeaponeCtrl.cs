using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 총이 아닌 무기 제어
/// 총 처럼 공격 시 발사체를 생성하여
/// 발사체로 데미지를 준다
/// 근접 캐릭터는 발사체를 날리지 않고 제자리에 생성
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class CharWeaponeCtrl : WeaponeData
        {

            #region 스킬 동작에 맞게 이펙트 반복 횟수 딜레이 설정
            [SerializeField, Header("스킬 데미지 이펙트 반복 횟수")]
            int[] skillRot = new int[3];

            [SerializeField, Header("스킬 데미지 이펙트 반복 딜레이")]
            float[] skillDelay = new float[3];

            [SerializeField, Header("스킬 데미지 이펙트 날리려면 속도를 넣어준다")]
            float[] skillMoveSpeed = new float[3];
            #endregion

            [SerializeField,Header("스킬 사용 시 광역 데미지 사용")]
            bool[] isRageDmg = new bool[3];

            [SerializeField, Header("스킬 사용 추가 데미지")]
            int[] nDmgPlus = new int[3];

            /// <summary>
            /// 공격 데미지를 적용 시키는 오브젝트
            /// 마법 발사체 또는 총알 같은...
            /// 근접은 발사체를 제자리에 둔다
            /// </summary>
            public void Attack()
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if(!player.IsAttack)
                    {
                        StartCoroutine(player.ShakeCam.ShakeCamAct(0.05f, 0.1f, 0.1f)); //카메라 흔들림
                        StartCoroutine(AttackDelay());
                    }
                    
                }
            }
            
            IEnumerator AttackDelay()
            {                
                player.IsAttack = true;

                int ran = Random.Range(0, 3);
                player.AniCtrl.AniAttackID(ran);
                player.AniCtrl.AniAttack();                
                GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[0]);

                player.FSatiety -= 1f;

                Muzzle.Play(); //공격 이펙트
                player.Stop();

                yield return new WaitForSeconds(0.5f);
                BulletSetting(true); //속성 이펙트

                yield return new WaitForSeconds(0.5f);
                
                player.IsAttack = false;
            }


            /// <summary>
            /// 스킬 사용 시 이펙트 실행
            /// 위에는 공격 버튼 클릭 할때 한번만 실행
            /// 이 함수는 하번 호출할때
            /// 캐릭터의 행동에 맞게 여러번 발생
            /// skillndex는 사용 스킬 번호
            /// rot 는 반복 횟수
            /// delay는 다음 이펙트 실행까지 딜레이
            /// speed는 파티클를 날린다         
            /// rageDmg 발사체를 날리지 않으면
            /// 캐릭터 주변으로 데미지를 준다(광역 데미지와 같은 원리로)
            /// </summary>
            /// <param name="isPlayer"></param>
            public IEnumerator BulletSetting(bool isPlayer,int skillIndex)
            {
                //광역 피해 사용 시
                if (isRageDmg[skillIndex])
                {
                    GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[skillIndex + 1]);

                    Collider[] colls = Physics.OverlapSphere(this.transform.position, 5, LayerMask.GetMask("Enemy"));

                    if (colls.Length > 0)
                    {
                        for (int i = 0; i < colls.Length; i++)
                        {
                            if (colls[i].GetComponent<HitDmg>())
                            {
                                Transform target = colls[i].GetComponent<EnemyCtrl>()._HitInfo;

                                int minDmg = NMinDmg + player.NBuffDmg + nDmgPlus[skillIndex]; //최소 데미지 + 버프데미지 + 스킬 데미지
                                int maxDmg = NMaxDmg + player.NBuffDmg + nDmgPlus[skillIndex];

                                colls[i].GetComponent<HitDmg>().HitDmage(target, Random.Range(minDmg, maxDmg));

                            }

                        }
                    }
                }


                for (int i = 0; i < skillRot[skillIndex]; i++)
                {        
                    switch (BulletType)
                    {
                        case eProjectilesType.BubbleBlue:
                            obj = pool.GetObjPool(pool.BubbleBluetMaxCount, pool.BubbleBlueList);
                            break;

                        case eProjectilesType.BubbleRose:
                            obj = pool.GetObjPool(pool.BubbleRoseMaxCount, pool.BubbleRoseList);
                            break;

                        case eProjectilesType.Feather:
                            obj = pool.GetObjPool(pool.FeatherMaxCount, pool.FeatherList);
                            break;

                        case eProjectilesType.CometBlue:
                            obj = pool.GetObjPool(pool.CometBlueMaxCount, pool.CometBlueList);
                            break;

                        case eProjectilesType.FireBall:
                            obj = pool.GetObjPool(pool.FireBallCount, pool.FireBallList);
                            break;

                        case eProjectilesType.Spark:
                            obj = pool.GetObjPool(pool.SparkCount, pool.SparkList);
                            break;

                        case eProjectilesType.AttackExplosion:
                            obj = pool.GetObjPool(pool.AttackExplosionCount, pool.AttackExplosionList);
                            break;
                    }

                    yield return new WaitForSeconds(skillDelay[skillIndex]);

                    //과부하 발생 시 이중 루프 삭제!!---------------------------------------------
                    //for문 명령어만 삭제 안의 내용은 사용한다
                    for(int j=0;j<firePos.Length;j++)
                    {
                        if (obj != null)
                        {
                            obj.transform.position = firePos[j].transform.position;
                            obj.transform.rotation = firePos[j].transform.rotation;
                            obj.SetActive(true);

                            ProjectilesMove objData = obj.GetComponent<ProjectilesMove>();

                            //탄의 속성값 적용
                            objData.MinDmg = NMinDmg + player.NBuffDmg + nDmgPlus[skillIndex]; //최소 데미지 + 버프데미지 + 스킬 데미지
                            objData.MaxDmg = NMaxDmg + player.NBuffDmg + nDmgPlus[skillIndex]; //최대 데미지
                            objData.MoveSpeed = skillMoveSpeed[skillIndex]; //속도
                            objData.IsExplosion = IsExplosion; //폭발 속성 유무 100%
                            objData.FExplosionArea = FExplosionArea; //폭발 범위
                            objData.IsStun = IsStun; //기절 속성 유무
                            objData.FStunPer = FStunPer; //기절 속성 확률
                            objData.FBulletDis = FBulletDis;

                            obj.GetComponent<ProjectilesMove>().IsPlayerBullet = isPlayer;

                        }
                    }
                    //-------------------------------------------------------

                    GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[skillIndex + 1]);

                    //yield return new WaitForSeconds(skillDelay[skillIndex]);
                }



            }

        }

    }
}
