using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 접근하면
/// 발로 차면서 상자가 열리고
/// 아이템을 생성 시킨다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class ChestOpenCtrl : MonoBehaviour
        {
            Animator ani;

            readonly int hashOpen = Animator.StringToHash("Open");

            bool isEnter = false;
            bool isOpen = false; //상자 열기 상태

            PlayerCtrl playerCtrl;

            [SerializeField, Header("회복 아이템 프리펩")]
            GameObject itemObj;
            [SerializeField, Header("나올 가능성 ( randum < itemObjPer )")]
            int itemObjPer = 5;

            [SerializeField, Header("파츠 아이템 프리펩")]
            GameObject partsObj;
            [SerializeField, Header("나올 가능성 ( itemObjPer =< randum < perObjPer )")]
            int perObjPer = 7;

            [SerializeField, Header("수류탄 아이템 프리펩")]
            GameObject subItemObj;
            [SerializeField, Header("나올 가능성 perObjPer =< randum =< subItemObj")]
            int subObjPer = 10;

            [SerializeField, Header("아이템 생성 위치")]
            Transform[] itemDropPos = new Transform[3];

            ParsingData parsingData;

            AudioSource _audio;
            [SerializeField]
            AudioClip sfx;

            private void Start()
            {
                ani = GetComponent<Animator>();
                playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                parsingData = GameObject.Find("ParsingData").GetComponent<ParsingData>();

                _audio = GetComponent<AudioSource>();
            }

            private void Update()
            {
                if (isEnter)
                {
                    if (!isOpen)
                        StartCoroutine(ChestOpenItem());
                }
            }

            /// <summary>
            /// 스피어 콜라이더에 트리거로 설정하여
            /// 인식하도록 한다
            /// 박스 콜라이더는 상자를 통과하지 못하도록 막는 역할
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {
                    isEnter = true;
                }
            }

            /// <summary>
            /// 아이템 상자 열기
            /// </summary>
            /// <returns></returns>
            IEnumerator ChestOpenItem()
            {
                GameManager.INSTANCE.SFXPlay(_audio, sfx);
                isOpen = true;
                playerCtrl.Stop();
                playerCtrl.IsChestOpen = true;
                playerCtrl.AniCtrl.AniChestOpen();
                yield return new WaitForSeconds(0.5f);

                playerCtrl.IsChestOpen = false;
                ani.SetTrigger(hashOpen);

                yield return new WaitForSeconds(0.5f);
                //아이템 드랍
                ItemDropPercent();

            }

            /// <summary>
            /// 아이템 드랍 및 확률
            /// </summary>
            private void ItemDropPercent()
            {
                int itemCount = Random.Range(0, 3); //아이템 떨어 뜨릴 개수
                GameObject obj = null;

                for (int i = 0; i < itemCount; i++)
                {
                    int itemTypeRan = Random.Range(0, 10);

                    #region 소모 아이템(회복 등)
                    if (itemTypeRan < itemObjPer)
                    {
                        obj = Instantiate(itemObj);  //회복 아이템       

                        int itemListCount = parsingData.BagItemList.Count; //리스트에 있는 아이템의 개수를 가져온다
                        int randumID = Random.Range(0, itemListCount); //아이템의 아이디 값을 랜덤으로 뽑는다

                        for (int j = 0; j < parsingData.BagItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.BagItemList[j].id.Equals(randumID))
                            {
                                //해당 아이디의 아이템 이름(아이템 이름 지정이 아니고
                                //아이템을 생성하기 위해 이름을 작성 시키는 부분)을 가져와 적용 시킨다
                                //적용 시킨후 Start를 접근하면서 초기화 시킨다
                                obj.GetComponent<ItemObj>().itemName = parsingData.BagItemList[j].name;
                                
                            }
                        }

                    }
                    #endregion

                    #region 파츠 아이템
                    else if (itemObjPer <= itemTypeRan &&
                        itemTypeRan < perObjPer)
                    {
                        obj = Instantiate(partsObj); //파츠 아이템

                        int itemListCount = parsingData.PartsItemList.Count; //리스트에 있는 아이템의 개수를 가져온다
                        int randumID = Random.Range(0, itemListCount); //아이템의 아이디 값을 랜덤으로 뽑는다

                        for (int j = 0; j < parsingData.PartsItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.PartsItemList[j].id.Equals(randumID))
                            {
                                obj.GetComponent<PartsObj>().itemName = parsingData.PartsItemList[j].name; //해당 아이디의 아이템 이름을 가져온다
                                
                            }
                        }

                    }
                    #endregion

                    #region 수류탄 등 서브 무기
                    else if (perObjPer <= itemTypeRan
                        && itemTypeRan <= subObjPer)
                    {
                        obj = Instantiate(subItemObj); //수류탄 등 아이템

                        int itemListCount = parsingData.SubItemList.Count; //리스트에 있는 아이템의 개수를 가져온다
                        int randumID = Random.Range(0, itemListCount); //아이템의 아이디 값을 랜덤으로 뽑는다

                        for (int j = 0; j < parsingData.SubItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.SubItemList[j].id.Equals(randumID))
                            {
                                obj.GetComponent<SubItemObj>().itemName = parsingData.SubItemList[j].name; //해당 아이디의 아이템 이름을 가져온다                                
                            }
                        }
                    }
                    #endregion

                    obj.transform.position = itemDropPos[i].position;

                }
            }

        }

    }
}
