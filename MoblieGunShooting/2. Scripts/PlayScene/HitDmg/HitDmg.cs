using Black.CameraUtil;
using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 엑스트라 적 또는 플레이어에 해당
/// 엑스트라 - 플레이어가 접근해서 생성 되는 적이 아니고
///           다른 방법으로 존재하는적
///           (주로 플레이어가 이동하면서 싸우게 되는 적, 보스 급)
/// </summary>
namespace Black
{
    namespace DmgManager
    {
        public class HitDmg : MonoBehaviour
        {
            float dmg;

            [SerializeField,Header("피격 대상(스크립트 적용된 캐릭터)")]
            protected CharactersData charData;

            [SerializeField, Header("플레이어의 경우 피격 당하면 카메라 흔들림")]
            shakeCamera shakeCam;

            /// <summary>
            /// 머리 부위인지 구분(추가 데미지)
            /// </summary>
            [SerializeField, Header("Head Dmg")]
            bool isHeadDmg = false;

            public bool IsHeadDmg
            {
                get
                {
                    return isHeadDmg;
                }

                set
                {
                    isHeadDmg = value;
                }
            }

            protected ItemManager player;
            protected Black.UI.PlayerUI playerUI;

            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemManager>();
                playerUI = GameObject.FindGameObjectWithTag("Player").GetComponent<Black.UI.PlayerUI>();

            }

            //private void Start()
            //{
            //    charData = GetComponent<CharactersData>();
            //}

            /// <summary>
            /// 일반 타격
            /// </summary>
            /// <param name="dmg"></param>
            virtual public void HitDamage(float dmg)
            {
                if(charData != null)
                {
                    if (charData.Hp > 0)
                    {
                        charData.Hp -= dmg;

                        //HitDmg 스크립트 적용 된 대상이 플레이어
                        if (this.transform.tag.Equals("Player"))
                        {
                            //Debug.Log("Hit");  

                            //플레이어 피격 UI
                            PlayerCtrl player = GetComponent<PlayerCtrl>();
                            player.PlayerUI.PainSprite(dmg);
                            shakeCam.IsPostSwitch = true;
                            shakeCam.HitPostSetting();


                            StartCoroutine(shakeCam.ShakeCamera(0.3f, 0.3f, 0.3f));
                        }
                    }


                    if (charData.Hp <= 0 &&
                        charData.IsLive)
                    {
                        charData.Hp = 0;
                        charData.IsLive = false;

                        //생성되는 적이 아니라 엑스트라 적
                        if (this.transform.tag.Equals("Enemy"))
                        {
                            //player.NUpgradePoint += 50;
                            playerUI.PointValue();

                            StartCoroutine(EnemyDisable());                            
                        }

                    }
                }
                
            }

            /// <summary>
            /// 헤드 샷 데미지 
            /// 1.5배
            /// </summary>
            /// <param name="dmg"></param>
            virtual public void HeadDamage(float dmg)
            {
                float damage = dmg * 1.5f;

                if(charData != null)
                {   
                    if(charData.Hp > 0)
                    {
                        charData.Hp -= damage;
                    }

                    if(charData.Hp <= 0 &&
                        charData.IsLive)
                    {
                        charData.Hp = 0;
                        charData.IsLive = false;

                        //생성되는 적이 아니라 엑스트라 적
                        if (this.transform.tag.Equals("Enemy"))
                        {
                            //player.NUpgradePoint += 100;
                            playerUI.PointValue();

                            StartCoroutine(EnemyDisable());
                        }
                    }

                    Debug.Log("Head Shot");
                }
                
            }


            protected IEnumerator EnemyDisable()
            {                
                yield return new WaitForSeconds(1.5f);
                //Debug.Log("오브젝트 비활성화");
                gameObject.SetActive(false);
            }

        }

    }
}
