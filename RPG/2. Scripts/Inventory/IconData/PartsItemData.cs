using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Text;

namespace Black
{
    namespace Inventory
    {
        [Serializable]
        public class PartsItem
        {
            private int id;
            private eItemType type;
            private string name;
            private int nLevel; //현재 레벨
            private int nMaxLevel; //최대 레벨(아이템 생성 시 렌덤으로 받아온다 5~30)
            private bool isExplosion;
            private float fMinRage; //폭발 최소 범위 파싱
            private float fMaxRage; //폭발 최대 범위 파싱
            private float fExplosionArea;
            private bool isStun;
            private float fStunMinPer; //기절 확률 최소
            private float fStunMaxPer; //기절 확률 최대 (최소에서 최대 값중 랜덤으로 받는다)
            private float fStunPer;
            private int dmgMinUp; //추가 데미지 (위와 같은 원리)
            private int dmgMaxUp; //추가 데미지
            private int dmgUp;
            private float accUp;
            private int count;
            private int price;
            private string tip;
            private string path;

            public int _Id { get => id; set => id = value; }
            public eItemType _Type { get => type; set => type = value; }
            public string _Name { get => name; set => name = value; }
            public bool _IsExplosion { get => isExplosion; set => isExplosion = value; }
            public bool _IsStun { get => isStun; set => isStun = value; }
            public float _FStunPer { get => fStunPer; set => fStunPer = value; }
            public int _DmgUp { get => dmgUp; set => dmgUp = value; }
            public float _AccUp { get => accUp; set => accUp = value; }
            public int _Count { get => count; set => count = value; }
            public int _Price { get => price; set => price = value; }
            public string _Tip { get => tip; set => tip = value; }
            public string _Path { get => path; set => path = value; }
            public float _FExplosionArea { get => fExplosionArea; set => fExplosionArea = value; }
            public int _NLevel { get => nLevel; set => nLevel = value; }
            public int _NMaxLevel { get => nMaxLevel; set => nMaxLevel = value; }
            public float _FMinRage { get => fMinRage; set => fMinRage = value; }
            public float _FMaxRage { get => fMaxRage; set => fMaxRage = value; }
            public float _FStunMinPer { get => fStunMinPer; set => fStunMinPer = value; }
            public float _FStunMaxPer { get => fStunMaxPer; set => fStunMaxPer = value; }
            public int _DmgMinUp { get => dmgMinUp; set => dmgMinUp = value; }
            public int _DmgMaxUp { get => dmgMaxUp; set => dmgMaxUp = value; }
        }

        public class PartsItemData : ItemDataClass, IPointerEnterHandler, IPointerExitHandler
        {
            PartsItem partsItem = new PartsItem();
            
            public PartsItem _PartsItem { get => partsItem; set => partsItem = value; }

            ParsingData ItemTable;

            [SerializeField, Header("아이템 정보 창")]
            GameObject itemInfoObj;
            [SerializeField]
            Text itemInfo;

            private void Start()
            {            
                itemInfoObj.SetActive(false);
            }


            public void OnPointerEnter(PointerEventData eventData)
            {
                itemInfoObj.SetActive(true);

                StringBuilder sb = new StringBuilder();
                sb.Append(_PartsItem._Name);
                sb.Append("\n");

                sb.Append("LEVLE ");
                sb.Append(_PartsItem._NLevel);
                sb.Append(" / ");
                sb.Append(_PartsItem._NMaxLevel);
                sb.Append("\n");

                sb.Append("Dmage Up ");
                sb.Append(_PartsItem._DmgUp);

                sb.Append("     ");

                sb.Append("Acc Up ");
                sb.Append(_PartsItem._AccUp);

                sb.Append("\n");
                sb.Append("Tip ");
                sb.Append(_PartsItem._Tip);

                sb.Append("\n");
                sb.Append("타격 범위 ");
                sb.Append(_PartsItem._FExplosionArea.ToString("N2"));

                sb.Append("\n");
                sb.Append("기절 확률 ");
                sb.Append(_PartsItem._FStunPer.ToString("N2"));

                sb.Append("\n");
                sb.Append("Buy / Sell Price ");
                sb.Append(_PartsItem._Price);
                sb.Append(" / ");
                sb.Append(_PartsItem._Price / 2);
                
                itemInfo.text = sb.ToString();

            }

            public void OnPointerExit(PointerEventData eventData)
            {
                itemInfoObj.SetActive(false);
            }

            /// <summary>
            /// 아이템 개수가 아닌 레벨을 표시
            /// </summary>
            public void LevelText()
            {
                count.text = "Lv" + _PartsItem._NLevel;
            }


            //private void Start()
            //{
            //    ItemTable = GameObject.Find("ParsingData").GetComponent<ParsingData>();             
            //}


        }


    }
}
