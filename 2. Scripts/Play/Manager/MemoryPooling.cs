/*
    18.11.18
    메모리 풀링
    탄피 효과 및 탄 피격 이펙트 관리
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MemoryPooling : MonoBehaviour
    {

        //Enemy
        //[Header("Zombie")]
        //public GameObject Zombie;
        //public List<GameObject> ZombieList = new List<GameObject>();
        //public int nZombieMax = 60;

        //일반적인 탄
        [Header("Bullet ArrowBlue Pooling")]
        public GameObject BulletObj;
        public List<GameObject> BulletList = new List<GameObject>();
        public int nBulletMaxCount = 15;

        [Header("Bullet ArrowGreen Pooling")]
        public GameObject ArrowGreenObj;
        public List<GameObject> ArrowGreenList = new List<GameObject>();
        public int ArrowGreenCount = 15;

        //[Header("DMG Count")]
        //public GameObject DmgUI;
        //public List<GameObject> DmgUIList = new List<GameObject>();
        //public int nMaxDmgUICount = 30;




        //[Space]
        //[Header("스파크 이펙트")]
        //public ParticleSystem RocketEffect;
        //public List<ParticleSystem> RocketEffectList = new List<ParticleSystem>();
        //public int RocketEffectPoolCount = 15;



        //================================================

        private void Start()
        {
            //  CreateObj("Zombie", "Zombie_", nZombieMax, Zombie, ZombieList);
            //  CreateObj("DmgCounter", "DmgCounter_", nMaxDmgUICount, DmgUI, DmgUIList);

            CreateObj("Bullet", "Bullet_", nBulletMaxCount, BulletObj, BulletList);
            CreateObj("Bullet", "Bullet_", ArrowGreenCount, ArrowGreenObj, ArrowGreenList);

        }


        //====================================================

        //게임 오브젝트 통합

        void CreateObj(string ObjName, string objName_, int MaxCount, GameObject Obj, List<GameObject> ObjList)
        {
            GameObject GameObj = new GameObject(ObjName);

            for (int i = 0; i < MaxCount; i++)
            {
                var obj = Instantiate<GameObject>(Obj, GameObj.transform);
                obj.name = objName_ + i.ToString("00");

                obj.SetActive(false);
                ObjList.Add(obj);
            }

        }

        public GameObject GetObjPool(int MaxCount, List<GameObject> ObjList)
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (ObjList[i].activeSelf == false)
                {
                    return ObjList[i];
                }
            }
            return null;

        }

        public IEnumerator ObjFalse(GameObject obj, float Delay)
        {
            yield return new WaitForSeconds(Delay);
            obj.SetActive(false);
        }

        //-----------------------------------------------------------

        //이펙트 통합 함수
        //게임 오브젝트 통합
        void CreateParticle(string ObjName, string objName_, int MaxCount, ParticleSystem Obj, List<ParticleSystem> ObjList)
        {
            GameObject GameObj = new GameObject(ObjName);

            for (int i = 0; i < MaxCount; i++)
            {
                var obj = Instantiate<ParticleSystem>(Obj, GameObj.transform);
                obj.name = objName_ + i.ToString("00");

                obj.gameObject.SetActive(false);
                ObjList.Add(obj);
            }

        }

        public ParticleSystem GetParticlePool(int MaxCount, List<ParticleSystem> ObjList)
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (ObjList[i].gameObject.activeSelf == false)
                {
                    return ObjList[i];
                }
            }
            return null;

        }

        public IEnumerator ParticleFalse(ParticleSystem obj, float Delay)
        {
            yield return new WaitForSeconds(Delay);
            obj.gameObject.SetActive(false);

        }



        public void ParticlePoolTrue(int MaxCount, List<ParticleSystem> ObjList)
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (ObjList[i].gameObject.activeSelf == true)
                {
                    ObjList[i].gameObject.SetActive(false);
                }
            }

        }

        //=========================================================================

    }

}

