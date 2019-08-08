using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 오브젝트의 기본 데이터
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class ItemObjData : MonoBehaviour
        {
            public string itemName;

            protected InventoryData inven;
            protected ParsingData ItemTable;
            protected eItemType itemType;

            [SerializeField] GameObject nameInfoObj;
            [SerializeField] protected Image itemIconSpr;

            protected PlayerCtrl player;

            virtual protected void Start()
            {
                inven = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();
                ItemTable = GameObject.Find("ParsingData").GetComponent<ParsingData>();
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            }

            private void LateUpdate()
            {
                //스테이지 이동 로딩 중
                //카메라를 추적하지 못하게 하기 위해
                //씬 이동 시작 시 에는 카메라 추적을 안함
                //씬 이동이 끝나면서 isSceneMove가 false가 되면
                //추적 시작
                if (!GameManager.INSTANCE.isSceneMove)
                {
                    nameInfoObj.transform.LookAt(new Vector3(Camera.main.transform.position.x,
                        Camera.main.transform.position.y, Camera.main.transform.position.z));
                }
                    
            }

        }

        public interface IItemObj
        {
            void DataInit();
        }

    }

}

