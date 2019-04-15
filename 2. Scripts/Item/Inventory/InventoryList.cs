using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Item
{
    public class InventoryList : MonoBehaviour
    {
        [SerializeField, Header("인벤토리 슬롯")]
        List<GameObject> itemList = new List<GameObject>();
 

        /// <summary>
        /// 아이템을 인벤이 넣을수 있는지 확인한다
        /// </summary>
        /// <returns></returns>
        public bool InvetoryCheck()
        {
            for(int i=0;i<itemList.Count;i++)
            {
                //아이템 슬롯 안에 아이템이 없으면
                //true를 반환하여 아이템을 넣을수 있게 한다
                if(itemList[i].transform.childCount == 0)
                {
                    return true;
                }
            }

            return false;
        }

       
        public List<GameObject> GetInventoryItem()
        {
            List<GameObject> returnItem = new List<GameObject>();
            GameObject obj = new GameObject();

            //인벤토리를 검색한다
            for (int i = 0; i < itemList.Count; i++)
            {
                //아이템이 있으면
                if (itemList[i].transform.childCount != 0)
                {
                    //반환 시킬 리스트에 추가
                    obj = itemList[i];
                    returnItem.Add(obj);
                }
            }

            //아이템을 모아둔 리스트를 반환 시킨다
            return returnItem; 
        }

    }

}

