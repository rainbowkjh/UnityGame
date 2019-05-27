/*
    18.11.18
    메모리 풀링
    탄피 효과 및 탄 피격 이펙트 관리
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Manager
    {
        public class MemoryPooling : MonoBehaviour
        {

            //Enemy
            [Header("Zombie")]
            public GameObject Zombie;
            public List<GameObject> ZombieList = new List<GameObject>();
            public int nZombieMax = 15;

            [Header("SelfDestructZombie")]
            public GameObject sdZombie;
            public List<GameObject> sdZombieList = new List<GameObject>();
            public int nSdZombieMax = 15;

            [Header("Grenade")]
            public GameObject Grenade;
            public List<GameObject> GrenadeList = new List<GameObject>();
            public int nGrenadeMax = 15;


            //================================================

            private void Start()
            {
                CreateObj("Zombie", "Zombie_", nZombieMax, Zombie, ZombieList);
                CreateObj("SDZombie", "SDZombie_", nSdZombieMax, sdZombie, sdZombieList);
                CreateObj("Grenade", "Grenade_", nGrenadeMax, Grenade, GrenadeList);
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
}

