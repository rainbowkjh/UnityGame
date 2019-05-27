using Black.Characters;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI에 있는 업그래이드 관리 오브젝트에 적용
/// 현재 데이터를 보여주고
/// 업그래이드 버튼을 클릭하면
/// 수치가 오른다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class UpgradeManager : MonoBehaviour
        {
            //업그래이드 항목의 데이터를 보여줄 Text
            [SerializeField, Header("업데이트 데이터 Text")]
            Text[] statsData;

            //플레이어의 HP정보와 각 무기의 데미지, 탄창 정보
            [SerializeField, Header("PlayerObj")]
            PlayerCtrl player;

            [SerializeField, Header("각 무기 Obj")]
            WeaponeCtrl[] weapone;

            [SerializeField, Header("각 무기 레벨 Text")]
            Text[] levelText;

            [SerializeField, Header("업그래이드 필요 포인트 Text")]
            Text[] usePoint;

            [SerializeField, Header("현재 포인트")]
            Text curPoint;

            #region 업그래이드 필요 포인트
            [SerializeField, Header("HP 업그래이드 필요")]
            int hpUpgradePoint = 1000;

            [SerializeField, Header("Recovery 업그래이드 필요")]
            int recoveryUpgradePoint = 300;

            [SerializeField, Header("Grenade 업그래이드 필요")]
            int GrenadeUpgradePoint = 500;

            [SerializeField, Header("Pistol 업그래이드 필요")]
            int PistolUpgradePoint = 300;

            [SerializeField, Header("AR 업그래이드 필요")]
            int ArUpgradePoint = 700;

            [SerializeField, Header("SG 업그래이드 필요")]
            int SgUpgradePoint = 500;
            #endregion

            #region 수정 하고 const로 막을 것!!
            [SerializeField, Header("아이템 소지량 증가 개수")]
            int nCountUp = 1;

            [SerializeField, Header("최소 데미지 증가")]
            float minDmgUp = 5;

            [SerializeField, Header("최대 데미지 증가")]
            float maxDmgUp = 5;

            [SerializeField, Header("권총 탄 증가 수")]
            int nPistolMag = 5;

            [SerializeField, Header("라이플 탄 증가 수")]
            int nArMag = 5;

            [SerializeField, Header("샷건 탄 증가 수")]
            int nSgMag = 5;
            #endregion


            //private void Start()
            //{
            //    StatsDataPrint();
            //}

            /// <summary>
                /// upgrade로 하기에는 비효율적인데
                /// 무기 데이터 불러오는 순서가 때문에 적용 되지 않고,
                /// 게임 시작 전 잠깐 사용하는 것이기 때문에
                /// 부담이 적을꺼 같아 Update로 데이터 정보를 출력
                /// </summary>
            private void Update()
            {
                StatsLevel();
                StatsDataPrint();

                CurPointValue(); //현재 남은 포인트
                UseUpgradePointPrint(); //필요한 포인트
            }

            /// <summary>
            /// 레벨 출력
            /// </summary>
            void StatsLevel()
            {
                levelText[0].text = "";
                levelText[1].text = "Level " + player.GetComponent<ItemManager>().RecoveryLevel + "/10";
                levelText[2].text = "Level " + player.GetComponent<ItemManager>().GrenadeLevel + "/10";

                levelText[3].text = "Level " + weapone[0].NLevel + "/" + weapone[0].NMaxLevel;
                levelText[4].text = "Level " + weapone[1].NLevel + "/" + weapone[1].NMaxLevel;
                levelText[5].text = "Level " + weapone[2].NLevel + "/" + weapone[2].NMaxLevel;
            }

            /// <summary>
            /// 활성화가 되면 현재 데이터를 보여준다
            /// </summary>
            void StatsDataPrint()
            {
                statsData[0].text = player.MaxHp.ToString("N0") + " / Max + " + 50;

                statsData[1].text = player.GetComponent<ItemManager>().RecoveryHP + " / Recovery+" + 10 + "\n" + "\n"
                    + player.GetComponent<ItemManager>().NMaxRecorveryCount + " / Count +" + nCountUp;

                statsData[2].text = player.GetComponent<ItemManager>().GreadeDMG + " / DMG +" + 5 + "\n" + "\n"
                        + player.GetComponent<ItemManager>().NMaxGrenadeCount + " / Count +" + nCountUp;

                statsData[3].text = weapone[0].FMinDmg + " /Min Dmg +" + minDmgUp.ToString("N0") + "\n\n"
                        + weapone[0].MaxDmg + " /Max Dmg +" + maxDmgUp.ToString("N0")+ "\n\n"
                        + weapone[0].NMag + " /Bullet +" + nPistolMag;

                statsData[4].text = weapone[1].FMinDmg + " /Min Dmg +" + minDmgUp.ToString("N0") + "\n\n"
                        + weapone[1].MaxDmg + " /Max Dmg +" + maxDmgUp.ToString("N0") + "\n\n"
                        + weapone[1].NMag + " /Bullet +" + nPistolMag;

                statsData[5].text = weapone[2].FMinDmg + " /Min Dmg +" + minDmgUp.ToString("N0") + "\n\n"
                        + weapone[2].MaxDmg + " /Max Dmg +" + maxDmgUp.ToString("N0") + "\n\n"
                        + weapone[2].NMag + " /Bullet +" + nPistolMag;


            }

            /// <summary>
            /// 현재 남은 포인트를 보여준다
            /// </summary>
            void CurPointValue()
            {
                curPoint.text = player.GetComponent<ItemManager>().NUpgradePoint.ToString();                
            }

            /// <summary>
            /// 각 항목 별 필요한 포인트를 보여준다
            /// </summary>
            void UseUpgradePointPrint()
            {
                usePoint[0].text = hpUpgradePoint.ToString();
                usePoint[1].text = recoveryUpgradePoint.ToString();
                usePoint[2].text = GrenadeUpgradePoint.ToString();

                usePoint[3].text = PistolUpgradePoint.ToString();
                usePoint[4].text = ArUpgradePoint.ToString();
                usePoint[5].text = SgUpgradePoint.ToString();

            }

            //버튼을 누르면 함수를 호출하여
            //변경된 데이터를 보여준다

            /// <summary>
                /// HP 증가
                /// </summary>
            public void PlayerHPUpgrade()
            {
                if (player.GetComponent<ItemManager>().NUpgradePoint >= hpUpgradePoint)
                {
                    GameManager.INSTANCE.BtnSfx();

                    player.MaxHp += 50;

                    player.GetComponent<ItemManager>().NUpgradePoint -= hpUpgradePoint;
                }
                    
               // StatsDataPrint();
            }

            /// <summary>
            /// 회복량 증가
            /// </summary>
            public void RecoveryValueUp()
            {
                if (player.GetComponent<ItemManager>().NUpgradePoint >= recoveryUpgradePoint)
                {
                    if (player.GetComponent<ItemManager>().RecoveryLevel < 10)
                    {
                        GameManager.INSTANCE.BtnSfx();

                        player.GetComponent<ItemManager>().RecoveryHP += 10;
                        player.GetComponent<ItemManager>().RecoveryLevel++;

                        player.GetComponent<ItemManager>().NUpgradePoint -= recoveryUpgradePoint;
                    }
                }
                    
                    
              //  StatsDataPrint();
            }

            /// <summary>
            /// 회복 아이템 개수 증가
            /// </summary>
            public void RecoveryCountUp()
            {
                if (player.GetComponent<ItemManager>().NUpgradePoint >= recoveryUpgradePoint)
                {
                    if (player.GetComponent<ItemManager>().RecoveryLevel < 10)
                    {
                        GameManager.INSTANCE.BtnSfx();

                        player.GetComponent<ItemManager>().NMaxRecorveryCount += nCountUp;
                        player.GetComponent<ItemManager>().RecoveryLevel++;

                        player.GetComponent<ItemManager>().NUpgradePoint -= recoveryUpgradePoint;
                    }
                }
                    
             //   StatsDataPrint();
            }

            /// <summary>
            /// 수류탄 데미지 증가
            /// </summary>
            public void GrenadeValueUp()
            {
                if (player.GetComponent<ItemManager>().NUpgradePoint >= GrenadeUpgradePoint)
                {
                    if (player.GetComponent<ItemManager>().GrenadeLevel < 10)
                    {
                        GameManager.INSTANCE.BtnSfx();

                        player.GetComponent<ItemManager>().GreadeDMG += 5;
                        player.GetComponent<ItemManager>().GrenadeLevel++;

                        player.GetComponent<ItemManager>().NUpgradePoint -= GrenadeUpgradePoint;
                    }
                }
                    
             //   StatsDataPrint();
            }

            /// <summary>
            /// 수류탄 개수 증가
            /// </summary>
            public void GrenadeCountUp()
            {
                if (player.GetComponent<ItemManager>().NUpgradePoint >= GrenadeUpgradePoint)
                {
                    if (player.GetComponent<ItemManager>().GrenadeLevel < 10)
                    {
                        GameManager.INSTANCE.BtnSfx();

                        player.GetComponent<ItemManager>().NMaxGrenadeCount += nCountUp;
                        player.GetComponent<ItemManager>().GrenadeLevel++;

                        player.GetComponent<ItemManager>().NUpgradePoint -= GrenadeUpgradePoint;
                    }
                }
                    
               // StatsDataPrint();
            }

            /// <summary>
            /// 각 무기 버튼에 넣고
            /// 데이터를 오릴 무기의 인덱스 값을 지정해준다
            /// </summary>
            /// <param name="index"></param>
            public void WeaponeMinDmgUp(int index)
            {

                int point = PointValue(index);

                //최대 레벨이 아닐때
                if (weapone[index].NLevel < weapone[index].NMaxLevel
                    && player.GetComponent<ItemManager>().NUpgradePoint >= point)
                {
                    if (weapone[index].FMinDmg < weapone[index].MaxDmg)
                    {
                        GameManager.INSTANCE.BtnSfx();

                        weapone[index].FMinDmg += minDmgUp;
                        weapone[index].NLevel++; //레벨 증가

                        weapone[index].WeaponePartsAct(); //파츠 활성화

                        player.GetComponent<ItemManager>().NUpgradePoint -= point;
                    }
                }

            }

            /// <summary>
            /// 무기 별 포인트 값을 가져온다
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            int PointValue(int index)
            {
                int point = 0;

                if (index == 0)
                    point = PistolUpgradePoint;

                else if (index == 1)
                    point = ArUpgradePoint;

                else if (index == 2)
                    point = SgUpgradePoint;

                return point;
            }

            public void WeaponeMaxDmgUp(int index)
            {
                int point = PointValue(index);

                if (weapone[index].NLevel < weapone[index].NMaxLevel
                    && player.GetComponent<ItemManager>().NUpgradePoint >= point)
                {
                    GameManager.INSTANCE.BtnSfx();

                    weapone[index].MaxDmg += maxDmgUp;
                    weapone[index].NLevel++; //레벨 증가

                    weapone[index].WeaponePartsAct(); //파츠 활성화

                    player.GetComponent<ItemManager>().NUpgradePoint -= point;
                }

            }

            public void WeaponeMagUp(int index)
            {
                int point = PointValue(index);

                if (weapone[index].NLevel < weapone[index].NMaxLevel
                    && player.GetComponent<ItemManager>().NUpgradePoint >= point)
                {
                    GameManager.INSTANCE.BtnSfx();

                    if (index == 0)
                        weapone[index].NMag += nPistolMag;

                    else if (index == 1)
                        weapone[index].NMag += nArMag;

                    else if (index == 2)
                        weapone[index].NMag += nSgMag;

                    weapone[index].NLevel++; //레벨 증가

                    weapone[index].WeaponePartsAct(); //파츠 활성화

                    player.GetComponent<ItemManager>().NUpgradePoint -= point;
                }
            }

        }

    }
}
