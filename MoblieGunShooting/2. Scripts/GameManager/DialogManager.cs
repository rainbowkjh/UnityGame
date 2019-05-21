using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 대화 내용을 관리
/// 우선은...
/// 플레이어가 특정 위치에 도착 시
/// 택스트 출력 하는 방식으로 한다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class DialogManager : MonoBehaviour
        {
            [SerializeField,Header("파싱(출력 텍스트) 해올 파일 이름")]
            string dialogFileName;

            [SerializeField, Header("화면에 출력 할 내용을 관리")]
            List<string> dialogList = new List<string>();

            /// <summary>
            /// 파싱 데이터를 담아
            /// 리스트에 Add 시키는 역할
            /// </summary>
            string tempData;

            [SerializeField, Header("Player에서 dialog를 출력 시킬 Text 오브젝트")]
            Text dialogText;
            
            [SerializeField, Header("팁 또는 NPC 대화 시 효과음(무전음)")]
            AudioClip[] _sfx;

            /// <summary>
            /// 이 오디오는 2D로 설정!!
            /// </summary>
            AudioSource _audio;

            #region 외부에서 리스트 내용을 가져갈 수 있게 Set, Get
            public List<string> DialogList
            {
                get
                {
                    return dialogList;
                }

                set
                {
                    dialogList = value;
                }
            }

            public Text DialogText
            {
                get
                {
                    return dialogText;
                }

                set
                {
                    dialogText = value;
                }
            }

            public AudioClip[] Sfx
            {
                get
                {
                    return _sfx;
                }

                set
                {
                    _sfx = value;
                }
            }
            #endregion

            private void Awake()
            {
                LoadTextParsing();
                _audio = GetComponent<AudioSource>();
            }


            void LoadTextParsing()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(dialogFileName);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Dialog");

                foreach (XmlNode node in all_node)
                {
                    tempData = node.SelectSingleNode("Text").InnerText;

                    DialogList.Add(tempData);
                    tempData = null;
                }

            }

            public void SfxPlay()
            {
                _audio.volume = GameManager.INSTANCE.volume.sfx * 2;
                _audio.PlayOneShot(_sfx[0]);
            }

            private void OnDestroy()
            {
                DialogList.Clear();
                DialogList = null;
            }
        }

    }
}
