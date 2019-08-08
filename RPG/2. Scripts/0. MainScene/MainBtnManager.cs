using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 선택 화면에서
/// 버튼 관리
/// </summary>
namespace Black
{
    namespace MainScene
    {
        public class MainBtnManager : MonoBehaviour
        {
            [SerializeField]
            Transform cameraTr;

            [SerializeField, Header("카메라 이동 동선")]
            Transform[] camMovePos;


            [SerializeField, Header("캐릭터 잠금 관리")]
            bool[] isLock;

            [SerializeField,Header("캐릭터 잠금 이미지")]
            GameObject lockSpr;

            AudioSource _audio;

            [SerializeField]
            AudioClip[] _sfx;
            

            [Space]
            #region 데이터 로드
            [SerializeField]
            GameObject loadDataObj;
            bool isloadDataBtn =false; //데이터 로드 버튼 클릭 활성화

            [SerializeField, Header("로드 정보")]
            Text[] slotText;

            #endregion

            #region 로딩 관련
            /*
            [SerializeField]
            GameObject loadObj;

            [SerializeField]
            Slider loadFill;

            [SerializeField]
            GameObject mainCam;

            bool isLoadStart = false; 
            */

                [SerializeField]
            LoadingManager loading;
            #endregion

            #region Player
            [SerializeField, Header("플레이어 능력치를 보여줄 경우 필요")]
            ParsingData parsingData;
            #endregion

            [SerializeField, Header("로드가 되면 버튼을 비활성화 시킨다")]
            GameObject[] btnObj;

            [SerializeField, Header("캐릭터 기술 영상 오브젝트")]
            UnityEngine.Video.VideoPlayer[] skillMovie;
            bool isMoviePlay = false;

            /// <summary>
            /// 옵션 창
            /// </summary>
            bool isOption = false;
            [SerializeField, Header("옵션 창")]
            GameObject optionObj;


            private void Start()
            {
                GameManager.INSTANCE.isSceneMove = false;

                loading.LoadObj.SetActive(false); //로딩 오브젝트 비활성화
                loading.MainCam.SetActive(true); //게임 카메라 활성화

                loadDataObj.SetActive(false);
                optionObj.SetActive(false);

                _audio = GetComponent<AudioSource>();

                cameraTr.position = camMovePos[GameManager.INSTANCE.CurPlayerIndenx].position;
                cameraTr.rotation = camMovePos[GameManager.INSTANCE.CurPlayerIndenx].rotation;

                for(int i=0;i<GameManager.INSTANCE.NSlotCount;i++)
                {
                    SaveInfo(i);                    
                }

                MovieInit(); 

                LockCheck();
            }

            private void LateUpdate()
            {
                CameraMove();

                if(loading.IsLoadStart)
                {
                    BtnDis(); //버튼 비활성화

                    loading.IsLoadStart = false;

                    StartCoroutine(loading.LoadStage("SafeArea"));
                }
            }


            /// <summary>
            /// 저장 정보를 보여준다
            /// </summary>
            /// <param name="index"></param>
            void SaveInfo(int index)
            {
                PlayerData data = GameManager.INSTANCE._DataManager.Load(index);

                StringBuilder sb = new StringBuilder();

                if (data.charName != "")
                {                    
                    sb.Append(data.charName);
                    sb.Append("\n\n");

                    sb.Append("<color=#00ff00>");
                    sb.Append("MaxHP ");
                    sb.Append("</color>");
                    sb.Append(data.nMaxHp);
                    sb.Append("\n");

                    sb.Append("<color=#0000ff>");
                    sb.Append("MaxMana ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxMana);
                    sb.Append("\n");

                    sb.Append("<color=#ffff00>");
                    sb.Append("MaxThirst ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxThirst);
                    sb.Append("\n");

                    sb.Append("<color=#ff0000>");
                    sb.Append("MaxSatiety ");
                    sb.Append("</color>");
                    sb.Append(data.fMaxSatiety);
                    sb.Append("\n\n");

                    sb.Append("Money ");
                    sb.Append(data.nMoney);
                    sb.Append("\n");

                    sb.Append("Material ");
                    sb.Append(data.nMaterial);
                    sb.Append("\n");

                    sb.Append("PartsMaterial ");
                    sb.Append(data.nPartsMaterial);
                }

                else
                {
                    sb.Append("Slot");
                }
                


                slotText[index].text = sb.ToString();
            }

            /// <summary>
            /// 데이터 로드 버튼 클릭 시
            /// 슬롯 활성화
            /// </summary>
            public void DataLoadBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isloadDataBtn = !isloadDataBtn;

                if (isloadDataBtn)
                {
                    loadDataObj.SetActive(true);
                }
                else
                {
                    loadDataObj.SetActive(false);
                }
            }

            /// <summary>
            /// 각 슬롯에 인덱스 값을 주고
            /// 슬롯에 저장된 데이터를 불러온다
            /// </summary>
            /// <param name="index"></param>
            public void SlotDataLoad(int index)
            {
                PlayerData data = GameManager.INSTANCE._DataManager.Load(index);

                if (!isLock[GameManager.INSTANCE.CurPlayerIndenx]
                    && data.charName != "")
                {
                    loading.IsLoadStart = true;
                    GameManager.INSTANCE.LoadData(index);

                    //창을 닫는다
                    DataLoadBtn();
                }
            }

            /// <summary>
            /// 로드 창 닫기
            /// </summary>
            public void SlotClose()
            {
                

                isloadDataBtn = false;
                loadDataObj.SetActive(false); ;
            }

            /// <summary>
            /// 캐릭터 잠금 확인
            /// </summary>
            void LockCheck()
            {
                //캐릭터 잠금 상태이면
                if (isLock[GameManager.INSTANCE.CurPlayerIndenx])
                {
                    lockSpr.SetActive(true);
                }
                //해제 상태이면
                else
                {
                    lockSpr.SetActive(false);
                }
            }

            /// <summary>
            /// 오른쪽 버튼
            /// </summary>
            public void RightBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                MovieInit(); //영상이 재생 중일 수도 있기 때문에 비활성화 시킨다

                GameManager.INSTANCE.CurPlayerIndenx++;
                if(GameManager.INSTANCE.CurPlayerIndenx >= camMovePos.Length)
                {
                    GameManager.INSTANCE.CurPlayerIndenx = 0;
                }

                
            }

            /// <summary>
            /// 왼쪽 버튼
            /// </summary>
            public void LeftBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                MovieInit(); //영상이 재생 중일 수도 있기 때문에 비활성화 시킨다

                GameManager.INSTANCE.CurPlayerIndenx--;
                if (GameManager.INSTANCE.CurPlayerIndenx < 0)
                {
                    GameManager.INSTANCE.CurPlayerIndenx = camMovePos.Length -1;
                }

            }

            /// <summary>
            /// 카메라 이동
            /// </summary>
            private void CameraMove()
            {
                cameraTr.position = Vector3.Slerp(cameraTr.position, camMovePos[GameManager.INSTANCE.CurPlayerIndenx].position, 10 * Time.deltaTime);
                cameraTr.rotation = Quaternion.Slerp(cameraTr.rotation, camMovePos[GameManager.INSTANCE.CurPlayerIndenx].rotation, 10 * Time.deltaTime);

                LockCheck();
            }

            /// <summary>
            /// New 버튼
            /// </summary>
            public void StartBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (!isLock[GameManager.INSTANCE.CurPlayerIndenx])
                {
                    //SceneManager.LoadScene("SafeArea");
                    loading.IsLoadStart = true;
                    //GameManager.INSTANCE.LoadData();
                    GameManager.INSTANCE.NewGame();
                }
                    
            }

            /// <summary>
            /// 로드할때 버튼을 비활성화 시킨다
            /// </summary>
            void BtnDis()
            {
                for(int i=0;i<btnObj.Length;i++)
                {
                    btnObj[i].SetActive(false);
                }
            }

            /*
            IEnumerator LoadStage()
            {

                mainCam.SetActive(false);
                loadObj.SetActive(true);
                yield return null;

                AsyncOperation async = Application.LoadLevelAsync("SafeArea");

                while(!async.isDone)
                {
                    yield return null;

                    loadFill.value = async.progress;                 
                }
                
            } 
                 */


            /// <summary>
            /// 영상을 재생 시키는 오브젝트를 비활성화 시킨다            
            /// </summary>
            void MovieInit()
            {            
                for (int i = 0; i < skillMovie.Length; i++)
                {
                    skillMovie[i].gameObject.SetActive(false);
                }
            }

            /// <summary>
            /// 스킬 영상 재생
            /// </summary>
            public void MoviePlay()
            {
                isMoviePlay = !isMoviePlay;

                if(isMoviePlay)
                {
                    MovieInit();
                    skillMovie[GameManager.INSTANCE.CurPlayerIndenx].gameObject.SetActive(true);
                    //skillMovie[GameManager.INSTANCE.CurPlayerIndenx].Vol = GameManager.INSTANCE.sfxVolume;
                    skillMovie[GameManager.INSTANCE.CurPlayerIndenx].Play();
                }
                else
                {
                    MovieInit();
                }
            }

            /// <summary>
            /// 옵션 창 활성화
            /// </summary>
            public void OptionBtn()
            {
                isOption = !isOption;

                if(isOption)
                {
                    optionObj.SetActive(true);
                }

                if(!isOption)
                {
                    optionObj.SetActive(false);
                }
            }

        }
        //Class End
    }
}
