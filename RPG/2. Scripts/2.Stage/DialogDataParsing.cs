using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;

/// <summary>
/// 각 스테이지에 해당하는
/// 대화창의 XML 파일을 불러온다
/// </summary>
namespace Black
{
    namespace Manager
    {
        [Serializable]
        public class DialogData
        {
            public int id;
            public string talker;
            public string log;
        }

        public class DialogDataParsing : MonoBehaviour
        {
            [SerializeField]
            eDialogDataPath dataPath;

            [SerializeField]
            List<DialogData> diaLogList = new List<DialogData>();
           
            public List<DialogData> DiaLogList { get => diaLogList; set => diaLogList = value; }

            private void Awake()
            {
                LoadData();
            }

            //private void Start()
            //{
            //    LoadData();
            //}

            private void OnDestroy()
            {
                if (DiaLogList != null)
                    DiaLogList.Clear();
            }

            /// <summary>
            /// 인스펙터 창에서
            /// 디아로그를 쉽게 선택할수 있게
            /// enum으로 관리 한다
            /// </summary>
            /// <returns></returns>
            string DataPathApply()
            {
                string path = null;

                switch (dataPath)
                {
                    case eDialogDataPath.City:
                        path = "CityMapDialog";
                        break;
                }

                return path;
            }

            /// <summary>
            /// 파싱 데이터 로드
            /// </summary>
            void LoadData()
            {
                string path = DataPathApply();

                TextAsset textAsset = (TextAsset)Resources.Load(path);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Dialog");
                foreach (XmlNode node in all_node)
                {
                    DialogData dialog = new DialogData();

                    dialog.id = Int32.Parse( node.SelectSingleNode("ID").InnerText);
                    dialog.talker = node.SelectSingleNode("Talker").InnerText;
                    dialog.log = node.SelectSingleNode("Log").InnerText;

                    DiaLogList.Add(dialog);
                }
            }


        }
        //class End
    }
}