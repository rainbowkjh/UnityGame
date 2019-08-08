using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 데이터를 관리 한다
/// 게임을 저장 로드 할때 인벤 정보 저장 불러오기
/// 아이템 습득 시 해당 인벤이 공간이 없으면
/// 습득 불가 등
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class InventoryData : MonoBehaviour
        {
            [SerializeField, Header("가방 인벤 슬롯")]
            List<GameObject> bagInvenList = new List<GameObject>();
            [SerializeField, Header("파츠 인벤 슬롯")]
            List<GameObject> partsInvenList = new List<GameObject>();
            [SerializeField, Header("보조 무기 슬롯")]
            List<GameObject> subInvenList = new List<GameObject>();

            //아이템 습득 시 데이터를 넘기면서 (GetComponent)
            //아이콘을 생성 시킨다(인벤에 여유 공간이 있을떼)
            //[SerializeField]
            //BagItemData bagItemIcon;
            //[SerializeField]
            //PartsItemData partsItemIcon;
            //[SerializeField]
            //SubItemData subItemIcon;
                       
            //public BagItemData BagItemIcon { get => bagItemIcon; set => bagItemIcon = value; }
            //public PartsItemData PartsItemIcon { get => partsItemIcon; set => partsItemIcon = value; }
            //public SubItemData SubItemIcon { get => subItemIcon; set => subItemIcon = value; }
            public PlayerCtrl Player { get => player; set => player = value; }
            public List<GameObject> BagInvenList { get => bagInvenList; set => bagInvenList = value; }
            public List<GameObject> PartsInvenList { get => partsInvenList; set => partsInvenList = value; }
            public List<GameObject> SubInvenList { get => subInvenList; set => subInvenList = value; }

            GameObject obj;

            [SerializeField]
            PlayerCtrl player; //인벤 무게의 값을 가져오기 위해 연결

            [SerializeField, Header("Bag Item 인벤 무게")]
            Text weightText;

            private void Start()
            {                
                #region 플레이어 인벤토리 로드
                GameManager.INSTANCE.LoadBagItem(BagInvenList, 
                    GameManager.INSTANCE.loadPlayerData.playerBagInven);

                GameManager.INSTANCE.LoadPartsItem(PartsInvenList,
                  GameManager.INSTANCE.loadPlayerData.playerPartsInven);

                GameManager.INSTANCE.LoadSubtsItem(SubInvenList,
                  GameManager.INSTANCE.loadPlayerData.playerSubInven);

                WeightTextPrint();
                #endregion
            }

            /// <summary>
            /// 인벤의 슬롯을 확인(아이템 습득)
            /// </summary>
            /// <returns></returns>
            public bool BagSlot(BagItem item)
            {
                bool isSlot = false;

                //인벤의 슬롯을 확인한다
                for(int i=0; i<BagInvenList.Count;i++)
                {
                    //슬롯이 하나라도 비어있으면 true
                    if (BagInvenList[i].transform.childCount == 0)
                    {
                        //무게가 넘어 가면
                        if ((Player.FWeight += item._Weight) > Player.FMaxWeight)
                        {
                            Player.FWeight -= item._Weight; //무게 감소
                            isSlot = false; //습득 실패
                            WeightTextPrint(); //무게 값 동기화

                            break; //루프 종료
                        }

                        isSlot = true;
                        // Debug.Log("------");

                        //데이터가 증발하지 않도록 Obj에 넣는다
                        //Obj는 따로 삭제 조건이 되면 아이콘을 삭제한다
                        obj =  Instantiate(GameManager.INSTANCE.BagItemIcon, BagInvenList[i].transform).gameObject;
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;

                        //생성된 객체에 데이터를 적용
                        obj.GetComponent<BagItemData>()._BagItem = item;
                        obj.GetComponent<BagItemData>().IconSpr.sprite = 
                            Resources.Load<Sprite>(obj.GetComponent<BagItemData>()._BagItem._Path);
                        obj.GetComponent<BagItemData>().NCount++;
                        obj.GetComponent<BagItemData>().CountText();

                        //player.FWeight += obj.GetComponent<BagItemData>()._BagItem._Weight; //무게 증가
                        WeightTextPrint();
                        break;
                    }

                    //슬롯에 아이템이 있으면 추가하려는 아이템인지 확인
                    //인벤에 있는 아이템이면 개수만 증가
                    else if(BagInvenList[i].transform.childCount == 1)
                    {
                        if(BagInvenList[i].GetComponentInChildren<BagItemData>()._BagItem._Name.Equals(item._Name))
                        {
                            //무게가 넘어 가면
                            if ((Player.FWeight += item._Weight) > Player.FMaxWeight)
                            {
                                Player.FWeight -= item._Weight; //무게 감소
                                isSlot = false; //습득 실패
                                WeightTextPrint();

                                break;
                            }

                            isSlot = true;
                            BagInvenList[i].GetComponentInChildren<BagItemData>().NCount++;
                            BagInvenList[i].GetComponentInChildren<BagItemData>().CountText();

                            WeightTextPrint();
                            break;
                        }
                    }
                }

                return isSlot;
            }

            public bool PartsSlot(PartsItem item)
            {
                bool isSlot = false;

                //슬롯이 하나라도 비어있으면 true
                for (int i = 0; i < PartsInvenList.Count; i++)
                {
                    if (PartsInvenList[i].transform.childCount == 0)
                    {
                        isSlot = true;
                        // Debug.Log("------");

                        //데이터가 증발하지 않도록 Obj에 넣는다
                        //Obj는 따로 삭제 조건이 되면 아이콘을 삭제한다
                        obj = Instantiate(GameManager.INSTANCE.PartsItemIcon, PartsInvenList[i].transform).gameObject;
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;

                        //생성된 객체에 데이터를 적용
                        obj.GetComponent<PartsItemData>()._PartsItem = item;
                        obj.GetComponent<PartsItemData>().IconSpr.sprite =
                            Resources.Load<Sprite>(obj.GetComponent<PartsItemData>()._PartsItem._Path);
                        obj.GetComponent<PartsItemData>().NCount++;
                        obj.GetComponent<PartsItemData>().LevelText();

                        break;
                    }
                }

                return isSlot;
            }
            
            public bool SubItemSlot(SubItem item)
            {
                bool isSlot = false;

                //슬롯이 하나라도 비어있으면 true
                for (int i = 0; i < SubInvenList.Count; i++)
                {
                    //아이템 인벤에 추가
                    if (SubInvenList[i].transform.childCount == 0)
                    {
                        isSlot = true;
                        // Debug.Log("------");

                        //데이터가 증발하지 않도록 Obj에 넣는다
                        //Obj는 따로 삭제 조건이 되면 아이콘을 삭제한다
                        obj = Instantiate(GameManager.INSTANCE.SubItemIcon, SubInvenList[i].transform).gameObject;
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;

                        //생성된 객체에 데이터를 적용
                        obj.GetComponent<SubItemData>()._SubItem = item;
                        obj.GetComponent<SubItemData>().IconSpr.sprite =
                            Resources.Load<Sprite>(obj.GetComponent<SubItemData>()._SubItem._Path);
                        obj.GetComponent<SubItemData>().NCount++;
                        obj.GetComponent<SubItemData>().CountText();

                        break;
                    }
                }

                return isSlot;
            }


            //인벤의 무게를 출력 시킨다.
            public void WeightTextPrint()
            {
                StringBuilder sb = new StringBuilder();
                if (Player.FWeight < 0)
                    Player.FWeight = 0;

                sb.Append(Player.FWeight.ToString("N2"));
                sb.Append(" / ");
                sb.Append(Player.FMaxWeight.ToString("N2"));

                weightText.text = sb.ToString();
            }

            /// <summary>
            /// 가방의 무게를 채크 해준다
            /// </summary>
            /// <returns></returns>
            public bool BagWeight()
            {
                bool isFull = true;

                for(int i=0;i< BagInvenList.Count;i++)
                {
                    if(BagInvenList[i].transform.childCount > 0)
                    {
                        BagItemData bagData = BagInvenList[i].transform.GetComponentInChildren<BagItemData>();
                        Player.FWeight += (bagData._BagItem._Weight * bagData.NCount);
                    }
                }

                if (player.FWeight < player.FMaxWeight)
                    isFull = false;

                return isFull;
            }

            /// <summary>
            /// 빈 슬롯에 Bag 아이템을 놓을 경우
            /// 같은 아이템이 다른 슬롯에 있는지 확인 후
            /// 있으면 슬롯에 있는 아이템의 개수를 증가
            /// treu를 반환 (IconDrop 에서 이동 중인 아이템의 오브젝트를 삭제)
            /// false 이면 아이콘을 원래 위치로 돌려보냄
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public bool bagItemEqual(BagItemData item)
            {
                bool isEqual = false;

                for (int i=0; i<BagInvenList.Count;i++)
                {
                    //슬롯에 추가하려는 아이템이 인벤에 있는지 확인
                    if (BagInvenList[i].GetComponentInChildren<BagItemData>())
                    {
                        //아이템의 상위 오브젝트가 ...
                        //간단히 말해서 아이콘 자기 자신일 경우를 제외하고 검색
                        if(item.transform.parent != BagInvenList[i].transform)
                        {
                            //같은 타입의 아이템이 있으면 같은 이름의 아이템이 있는지 확인
                            if (item._BagItem._Name.Equals(BagInvenList[i].GetComponentInChildren<BagItemData>()._BagItem._Name))
                            {
                                #region 수정 전
                                ////있으면 이동중인 아이템의 개수를 슬롯에 있는 아이템에 증가 시키고
                                ////루프 종료
                                //bagInvenList[i].GetComponentInChildren<BagItemData>().NCount += item.NCount;
                                //bagInvenList[i].GetComponentInChildren<BagItemData>().CountText(); //아이템 개수 표시

                                ////플레이어의 무게를 
                                ////추가 하는 아이템의 무게 * 개수를 더해준다
                                //player.FWeight += (item._BagItem._Weight * item.NCount);

                                //if(player.FWeight > player.FMaxWeight)
                                //{
                                //    int j = 0;
                                //    for (j = item.NCount; j > 0; j--)
                                //    {
                                //        bagInvenList[i].GetComponentInChildren<BagItemData>().NCount--; //슬롯의 아이템 개수를 다시 줄인다
                                //        player.FWeight -= (item._BagItem._Weight * j); //햔제 무게에서 아이템의 무게의 개수만큼 빼준다
                                //        j--;

                                //        if (player.FWeight <= player.FMaxWeight)
                                //            break;
                                //    }


                                //    item.NCount = j;
                                //    item.CountText();
                                //}
                                #endregion

                                //임시 무게 값을 구한다
                                float tempWeight = player.FWeight + (item._BagItem._Weight * item.NCount);

                                //무게를 넘기면
                                if (tempWeight > player.FMaxWeight)
                                {
                                    int j = 1;
                                    for(j=1; j<=item.NCount;j++)
                                    {
                                        tempWeight = player.FWeight + (item._BagItem._Weight * j);

                                        if(tempWeight > player.FMaxWeight)
                                        {
                                            j--;

                                            tempWeight = player.FWeight + (item._BagItem._Weight * j);

                                            break;
                                        }
                                    }

                                    item.NCount -= j;
                                    BagInvenList[i].GetComponentInChildren<BagItemData>().NCount += j;
                                    BagInvenList[i].GetComponentInChildren<BagItemData>().CountText(); //아이템 개수 표시

                                    player.FWeight = tempWeight;
                                }

                                //넘기지 않으면
                                else if(tempWeight <= player.FMaxWeight)
                                {
                                    //아이템의 개수를 슬롯에 있는 아이템에 추가
                                    BagInvenList[i].GetComponentInChildren<BagItemData>().NCount += item.NCount;
                                    BagInvenList[i].GetComponentInChildren<BagItemData>().CountText(); //아이템 개수 표시

                                    //플레이어의 무게를 
                                    //추가 하는 아이템의 무게 * 개수를 더해준다
                                    player.FWeight += (item._BagItem._Weight * item.NCount);

                                    item.NCount = 0; //IconDrop에서 삭제 유도
                                }

                                isEqual = true;
                                break;
                            }
                        }
                        
                    }
                    
                }

                return isEqual;
            }

        
            /// <summary>
            /// 플레이어의 가방 슬롯에
            /// 빈 공간이 있는지만 확인
            /// </summary>
            /// <returns></returns>
            public bool BagSlotCheck()
            {
                bool isTrue = false;

                for(int i=0;i<bagInvenList.Count;i++)
                {
                    if(bagInvenList[i].transform.childCount == 0)
                    {
                        //하나라고 빈 슬롯이 있으면
                        //true를 반환
                        isTrue = true;
                        return isTrue;                        
                    }
                }

                //위에 빈 슬롯을 하나라도 못 찾으면
                return isTrue;
            }

        }

    }
}
