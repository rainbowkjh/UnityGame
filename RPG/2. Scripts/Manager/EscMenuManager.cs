using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 플레이 중 ESC 메뉴 호출
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class EscMenuManager : MonoBehaviour
        {

            [SerializeField, Header("저장 슬롯 활성화")]
            GameObject saveSlotListObj;
            bool isSlotAct =false; //슬롯 버튼 활성화 상태
                        
            PlayerCtrl player;

            [SerializeField, Header("저장 시 데이터 정보 출력")]
            Text[] saveDataText;

            [SerializeField]
            LoadingManager loading;

            [SerializeField, Header("메뉴 버튼들")]
            GameObject[] btnObjs;

            CanvasGroup menuCg; //로딩 중 메뉴 창을 숨기는 역할

            /// <summary>
            /// 옵션 창
            /// </summary>
            bool isOption = false;
            [SerializeField, Header("옵션 창")]
            GameObject optionObj;

            public LoadingManager Loading { get => loading; set => loading = value; }
            public CanvasGroup MenuCg { get => menuCg; set => menuCg = value; }

            private void Start()
            {
                MenuCg = GetComponent<CanvasGroup>();
                MenuCg.alpha = 1;

                //스테이지에서 넘어 왔으면
                //퀵 로드
                if (GameManager.INSTANCE.isSceneMove)
                {
                    GameManager.INSTANCE.isSceneMove = false;
                    GameManager.INSTANCE.QuickLoadData();
                }

                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                saveSlotListObj.SetActive(false); //저장 슬롯을 비활설화 시킴
                optionObj.SetActive(false);

                for (int i=0;i<GameManager.INSTANCE.NSlotCount;i++)
                {
                    SavaDataInfo(i);
                }

                Loading.LoadObj.SetActive(false); //로딩 오브젝트 비활성화
                Loading.MainCam.SetActive(true); //게임 카메라 활성화
            }


            /// <summary>
            /// 메뉴 창을 닫는다
            /// </summary>
            public void MenuClose()
            {
                GameManager.INSTANCE.isMenu = false;
               
                for(int i=0;i< btnObjs.Length;i++)
                {
                    //btnObjs[i].alpha = 0;
                    //btnObjs[i].blocksRaycasts = false;
                    //btnObjs[i].interactable = false;

                    btnObjs[i].SetActive(false);
                }
            }

            /// <summary>
            /// 저장 버튼을 클릭
            /// 저장 슬롯 활성화
            /// </summary>
            public void SaveBtn()
            {
                isSlotAct = !isSlotAct;

                if(isSlotAct)
                {
                    saveSlotListObj.SetActive(true);
                }
                else
                {
                    saveSlotListObj.SetActive(false);
                }
            }

            /// <summary>
            /// 해당 슬롯에 데이터를 저장한다
            /// </summary>
            /// <param name="index"></param>
            public void DataSave(int index)
            {
                //데이터 저장
                GameManager.INSTANCE.SaveData(player, index);

                SavaDataInfo(index);
            }

            /// <summary>
            /// 저장 슬롯에 
            /// 저장된 파일잉 있으면 정보를 보여준다
            /// </summary>
            void SavaDataInfo(int index)
            {
                PlayerData data = GameManager.INSTANCE._DataManager.Load(index);

                StringBuilder sb = new StringBuilder();

                if (data.charName != "")
                {                    
                    sb.Append(data.charName);
                    sb.Append(" / ");

                    sb.Append("<color=#00ff00>");
                    sb.Append("MaxHP ");
                    sb.Append("</color>");
                    sb.Append(data.nMaxHp);
                    sb.Append(" / ");

                    sb.Append("<color=#0000ff>");
                    sb.Append("MaxMana ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxMana);
                    sb.Append(" / ");

                    sb.Append("<color=#ffff00>");
                    sb.Append("MaxThirst ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxThirst);
                    sb.Append(" / ");

                    sb.Append("<color=#ff0000>");
                    sb.Append("MaxSatiety ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxSatiety);
                    sb.Append(" / ");

                    sb.Append("Money ");
                    sb.Append(data.nMoney);
                    sb.Append(" / ");

                    sb.Append("Material ");
                    sb.Append(data.nMaterial);
                    sb.Append(" / ");

                    sb.Append("PartsMaterial ");
                    sb.Append(data.nPartsMaterial);

                    //저장 슬롯에 저장 정보를 출력
                    saveDataText[index].text = sb.ToString();
                }
            }

            /// <summary>
            /// 세이브 창을 닫느다
            /// </summary>
            public void SaveSlotClose()
            {
                isSlotAct = false;
                saveSlotListObj.SetActive(false);
            }

            /// <summary>
            /// 옵션 버튼 클릭(옵션 메뉴 활성화)
            /// </summary>
            public void OptionBtn()
            {
                isOption = !isOption;

                if (isOption)
                {
                    optionObj.SetActive(true);
                }

                if (!isOption)
                {
                    optionObj.SetActive(false);
                }
            }

            /// <summary>
            /// 메인으로 돌아간다
            /// </summary>
            public void MainBtn()
            {
                //데이터를 저장하지는 않는다

                GameManager.INSTANCE.isSceneMove = true;
                //로딩 UI 추가!
                StartCoroutine(Loading.LoadStage("Lobby"));

                MenuClose(); //메뉴를 닫으면서 로딩이 멈춘다;;

                MenuCg.alpha = 0; //메뉴 창만 투명하게 한다
               
            }

            /// <summary>
            /// 게임을 종료한다
            /// </summary>
            public void QuitBtn()
            {

            }


        }

    }
}
