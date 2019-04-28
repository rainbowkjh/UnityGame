using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{
    public class WeaponeCtrl : MonoBehaviour
    {
        [SerializeField, Header("무기 장착 위치")]
        private GameObject[] weaponePos;
        int useWeapone = 0; //WeaponePos의 인덱스 값으로 사용 중인 무기를 활성화

        #region Set,Get
        public int UseWeapone
        {
            get
            {
                return useWeapone;
            }

            set
            {
                useWeapone = value;
            }
        }

        public GameObject[] WeaponePos
        {
            get
            {
                return weaponePos;
            }

            set
            {
                weaponePos = value;
            }
        }
        #endregion

        private void Start()
        {
            weaponePos[0].SetActive(true);
            weaponePos[1].SetActive(false);
        }

        public void GunFire()
        {
            //Test
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMaskPlayer = 1 << 8;
            layerMaskPlayer = ~layerMaskPlayer;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskPlayer))
            {
                // Debug.Log("Hit Info : " + hit.transform.gameObject.name);
                weaponePos[UseWeapone].GetComponentInChildren<GunData>().Mag--;

                float ranDmg = Random.Range(weaponePos[UseWeapone].GetComponentInChildren<GunData>().MinDmg,
                                weaponePos[UseWeapone].GetComponentInChildren<GunData>().MaxDmg);

                if (hit.transform.tag.Equals("Enemy"))
                {
                    hit.transform.GetComponent<DmgCounter>().DmgCount(ranDmg);
                }
                
                
            }

        }

        public void GunReload()
        {
            weaponePos[UseWeapone].GetComponentInChildren<GunData>().Mag
                = weaponePos[UseWeapone].GetComponentInChildren<GunData>().MaxMag;
        }

        public void WeaponeChange()
        {
            for(int i=0;i<weaponePos.Length;i++)
            {
                weaponePos[i].SetActive(false);
            }

            WeaponePos[UseWeapone].SetActive(true);
        }
    }

}
