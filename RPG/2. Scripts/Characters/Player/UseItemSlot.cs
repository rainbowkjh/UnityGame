using Black.Inventory;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총을 사용할때
/// 사용할수 있는 아이템 퀵 슬롯 제어
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class UseItemSlot : MonoBehaviour
        {
            [SerializeField, Header("UI에서 총을 사용 시 사용되는 퀵슬롯 3개")]
            Transform[] quickSlots;
            PlayerCtrl player;
            /// <summary>
            /// 소모 아이템(회복) 사용 시 
            /// 아이템을 확인 하여
            /// 해당 아이템의 수치를 회복 시켜준다 
            /// </summary>
            [SerializeField]
            BagItemUse bagItemUse;

            private void Start()
            {
                player = GetComponent<PlayerCtrl>();
            }

            /// <summary>
            /// Q = 0
            /// W = 1
            /// E = 2
            /// 키 입력 시 사용
            /// </summary>
            /// <param name="index"></param>
            public void UseItem(int index)
            {
                //회복 아이템
                if(quickSlots[index].GetComponentInChildren<BagItemData>() != null)
                {
                    //Debug.Log("BagItem");
                    BagItemData data = quickSlots[index].GetComponentInChildren<BagItemData>();

                    //각 아이템에 맞는 데이터 처리 및 애니메이션
                    //회복 되는 데이터(HP, STA.....)
                    bagItemUse.UseItemApply(data); //아아템 사용

                   // data.NCount--; //수량을 감소 시킨다 (나중에 아이템에 수량이 붙일수 있다)
                    if(data.NCount <=0) //수량이 0이 되면 삭제한다
                    {
                        Destroy(data.gameObject);
                    }


                }
                //보조 무기(수류탄 등)
                else if(quickSlots[index].GetComponentInChildren<SubItemData>() != null)
                {
                    //마우스 방향을 바라본다
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    }

                    //Debug.Log("SubItem");
                    SubItemData data = quickSlots[index].GetComponentInChildren<SubItemData>();

                    //각 아이템에 맞는 데이터 처리 및 애니메이션
                    //수류탄 던지는 애니 등
                    UseSubItemApply(data);

                    data.NCount--; 
                    if (data.NCount <= 0)
                    {
                        Destroy(data.gameObject);
                    }
                }

            }

           

            #region 보조 무기 아이템(수류탄, 터렛)
            void UseSubItemApply(SubItemData item)
            {
                switch (item._SubItem._ItemType)
                {
                    //수류탄 애니메이션
                    //수류탄을 던진다
                    case Manager.eSubItemType.Grenade:

                        //if (!player.IsAttack && !player.IsReload)
                        //{
                            //정지 시킨다
                            bagItemUse._Player.IsWalk = false;
                            bagItemUse._Player.IsRun = false;
                            bagItemUse._Player.Nav.velocity = Vector3.zero;
                            bagItemUse._Player.Nav.isStopped = true;
                            StartCoroutine(bagItemUse._Player.PlayerGrenade(1.5f));

                            //위에 코루틴을 사용 했기때문에 다른 함수로 딜레이를 준다
                            //던지는 애니메이션이 나오기 전에
                            //수류탄이 던져지는 것을 방지
                            Invoke("GrenadeThowDelay", 1.0f);

                        //}

                        break;

                        //마우스 위치에 터렛을 설치 시킨다
                    case Manager.eSubItemType.Turret:

                        break;
                }
               
            }

            /// <summary>
            /// 수류탄을 잠시 후 던진다
            /// 위에 코루틴을 사용 했기 떄문에(여러개 사용시 렉)
            /// invoke를 사용 
            /// </summary>
            void GrenadeThowDelay()
            {
                GameObject obj = bagItemUse._Player.Pool.GetObjPool(bagItemUse._Player.Pool.GrenadeMaxCount, bagItemUse._Player.Pool.GrenadeList);
                if (obj != null)
                {
                    obj.transform.position = bagItemUse._Player.GrenadeThrowPos.position;
                    obj.transform.rotation = bagItemUse._Player.GrenadeThrowPos.rotation;
                    obj.SetActive(true);

                    obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 5, ForceMode.Impulse);
                }

            }

            #endregion
        }
    }
}