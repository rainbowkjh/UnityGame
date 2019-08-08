using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Manager
    {
        
        public class LoadingTipText : MonoBehaviour
        {
            string dataPath = "LoadingTipText";
            [SerializeField]
            List<string> tips = new List<string>();
            string tip;

            [SerializeField]
            Text tipText;

            void Start()
            {
                LoadTipText();

                int randText = Random.Range(0, tips.Count);
                tipText.text = tips[randText];
            }

            /// <summary>
            /// 종료 시 리스트 정리
            /// </summary>
            private void OnDestroy()
            {
                if (tips != null)
                    tips.Clear();
            }

            /// <summary>
            /// xml 파일의 내용을 불러온다
            /// </summary>
            void LoadTipText()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(dataPath);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Loading");
                foreach (XmlNode node in all_node)
                {
                    tip = node.SelectSingleNode("Tip").InnerText;

                    tips.Add(tip);
                }

            }
        }

    }
}

