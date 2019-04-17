using _Item;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 장착 및 업그래이드 버튼
/// </summary>
namespace MainScene
{
    namespace Menu
    {
        public class WeaponeSettingBtn : MonoBehaviour
        {
            private WeaponeGameData m_WeaponeData = new WeaponeGameData();

            [SerializeField, Header("아이템 클릭 시 활성화 UI")]
            GameObject m_objClickUI;
            [SerializeField, Header("활성화 된 UI에서 아이템 정보 출력")]
            Text m_ItemInfoTxt;

            [SerializeField, Header("아이템 클릭 중 무기 클릭 시 버튼 생성")]
            GameObject m_objWeaponeBtn;

            [SerializeField, Header("아이템 클릭 중 아이템 클릭 시 버튼 생성")]
            GameObject m_objItemBtn;

            [SerializeField, Header("업그래이드 버튼 클릭 시 활성화 오브젝트")]
            GameObject m_objUpgradeBtn;

            /// <summary>
            /// 무기 정보 창 활성화 상태
            /// </summary>
            bool m_isWeaponeAct = false;
            bool m_isUpgradeAct = false;

        

            void Start()
            {
                BtnInit();
            }

            /// <summary>
            /// 처음에는 모든 버튼 오브젝트를 비활성화 시키고
            /// 해당 버튼을 눌렸을때만 활성화 시킨다
            /// </summary>
            void BtnInit()
            {
                m_objClickUI.SetActive(false);
                m_objItemBtn.SetActive(false);
                m_objWeaponeBtn.SetActive(false);
                m_objUpgradeBtn.SetActive(false);

                m_isWeaponeAct = false;
                m_isUpgradeAct = false;
            }

            /// <summary>
            /// 아이템 클릭하는곳에서
            /// 아이템 타입이 무기이면
            /// 무기의 데이터를 매개변수로 넘겨준다
            /// </summary>
            /// <param name="data"></param>
            public void WeaponeIconClick(WeaponeGameData data)
            {
                m_WeaponeData = data;

                m_isWeaponeAct = !m_isWeaponeAct;

                m_objClickUI.SetActive(m_isWeaponeAct);
                m_objWeaponeBtn.SetActive(m_isWeaponeAct);

                //무기 정보 출력
                m_ItemInfoTxt.text = "Weapone " + m_WeaponeData.WeaponeName + "\n"
                                    + "Type " + m_WeaponeData.WeaponeType + "\n"
                                    + "DMG " + m_WeaponeData.FMinDmg.ToString("N2") + " ~ " + m_WeaponeData.MaxDmg.ToString("N2") + "\n"
                                    + "Mag " + m_WeaponeData.NMag;


                //다른 아이콘 클릭 시 열려 있는 업그래이드 창을 닫는다 
                if (!m_isWeaponeAct)
                {
                    m_isUpgradeAct =false;
                    m_objUpgradeBtn.SetActive(m_isUpgradeAct);
                }
            }

            /// <summary>
            /// 아이템 장착
            /// </summary>
            public void EquipBtn(int n)
            {
                //장착할 아이템이 무기이면
                //미리 장착되어 있는 무기가 있는지 확인 후
                //없으면 장착 있으면 장착 안됨                
                if (n == 0)
                {
                    //장착 슬롯 하위 오브젝트가 있는지 확인
                    if(InventoryList.INVENTORY.TrEquipTr[0].transform.childCount == 0)
                    {
                        GameObject icon = Instantiate(InventoryList.INVENTORY.EquipIcon);
                        icon.transform.SetParent(InventoryList.INVENTORY.TrEquipTr[0].transform);
                        icon.transform.localPosition = Vector3.zero;
                        icon.transform.localRotation = Quaternion.identity;
                        icon.transform.localScale = new Vector3(1,1,1);

                        icon.GetComponent<ItemData>().WeaponeData = 
                            _Item.ItemClick.ITEMICON.GetComponent<ItemData>().WeaponeData;

                        //데이터를 적용했는데 아이콘이 생성되면서
                        //다시 파싱 데이터 접근하는 것을 막음
                        icon.GetComponent<ItemData>().isEquip = true;

                        //아이콘 이미지 적용 
                        icon.GetComponent<ItemData>().SpriteApply();

                        icon.GetComponent<ItemData>().WeaponeData.IsUse = true; //로드할때 사용중인 무기인지 구별

                        //아이템 장착 표시 활성화
                        _Item.ItemClick.ITEMICON.GetComponent<ItemClick>().ItemDataValue.UseIcon.SetActive(true);

                        //Debug.Log("아이템 장착");
                        //Debug.Log("itemData  Min Dmg : " + _Item.ItemClick.ITEMICON.GetComponent<ItemData>().WeaponeData.FMinDmg);
                        //Debug.Log("WeaponeData  Min Dmg : " + icon.WeaponeData.FMinDmg);

                    }
                }

                //장착할 아이템이 소모 아이템이면
                //1번위치에 아이템이 있으면 2번위치에 장착
                //1~2 모두 아이템이 장착되어 있으면 선택된 아이템 장착 안됨
                if (n != 0)
                {

                }

                BtnInit();
            }

            /// <summary>
            /// 해제할 아이템 장착 슬롯의 하위 오브젝트(아이콘)를 제거
            ///  0 무기 1~2 소모아이템
            /// </summary>
            /// <param name="n"></param>
            public void UnEquipBtn(int n)
            {
                if(InventoryList.INVENTORY.TrEquipTr[n].transform.childCount > 0)
                {
                    ItemData icon = InventoryList.INVENTORY.TrEquipTr[n].GetComponentInChildren<ItemData>();

                    if(icon.ItemType == Weapone.ItemType.Weapone)
                    {
                        //클릭한 아이템 정보와 장착되어 있는 아이템 정보가 일치하면 해제
                        //엄청난 확률로?? 데이터가 똑같은 아이템이 두개 있을경우 엉뚱하게 장착해제 할수 있다(수정 해야됨)
                        if (icon.WeaponeData == _Item.ItemClick.ITEMICON.GetComponent<ItemClick>().ItemDataValue.WeaponeData)
                        {
                            icon.GetComponent<ItemData>().WeaponeData.IsUse = false; //로드할때 사용중인 무기인지 구별
                            _Item.ItemClick.ITEMICON.GetComponent<ItemClick>().ItemDataValue.UseIcon.SetActive(false);
                            Destroy(icon.gameObject);
                            BtnInit();
                        }
                    }

                    else if(icon.ItemType == Weapone.ItemType.Item)
                    {

                    }
                }
            }

            /// <summary>
            /// 업그래이드 버튼
            /// </summary>
            public void UpgradeBtnClick()
            {
                m_isUpgradeAct = !m_isUpgradeAct;
                m_objUpgradeBtn.SetActive(m_isUpgradeAct);
            }


            /// <summary>
            /// 선택된 아이템 삭제
            /// </summary>
            public void DeleteBtn()
            {
                //사용하지 않고 있는 아이템만 삭제 가능
                if(!_Item.ItemClick.ITEMICON.GetComponent<ItemClick>().ItemDataValue.WeaponeData.IsUse)
                {
                    //마지막에 클릭한 아이템을 삭제
                    Destroy(_Item.ItemClick.ITEMICON);
                }
                

                BtnInit();
            }
        }

    }
}
