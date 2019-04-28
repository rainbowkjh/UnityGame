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

        #region Bullet
        [Header("Bullet ArrowBlue Pooling")]
        public GameObject BulletObj;
        public List<GameObject> BulletList = new List<GameObject>();
        public int nBulletMaxCount = 15;

        [Header("Bullet ArrowGreen Pooling")]
        public GameObject ArrowGreenObj;
        public List<GameObject> ArrowGreenList = new List<GameObject>();
        public int ArrowGreenCount = 15;

        [Header("Bullet BubbleRose Pooling")]
        public GameObject BubbleRoseObj;
        public List<GameObject> BubbleRoseList = new List<GameObject>();
        public int BubbleRoseCount = 15;

        [Header("Bullet BulletGreen Pooling")]
        public GameObject BulletGreenObj;
        public List<GameObject> BulletGreenList = new List<GameObject>();
        public int BulletGreenCount = 15;

        [Header("Bullet BulletOrange Pooling")]
        public GameObject BulletOrangeObj;
        public List<GameObject> BulletOrangeList = new List<GameObject>();
        public int BulletOrangeCount = 15;

        [Header("Bullet Comet_Blue Pooling")]
        public GameObject CometBlueObj;
        public List<GameObject> CometBlueList = new List<GameObject>();
        public int CometBlueCount = 15;

        [Header("Bullet ElectricOrangeBall Pooling")]
        public GameObject ElectricOrangeBallObj;
        public List<GameObject> ElectricOrangeBallList = new List<GameObject>();
        public int ElectricOrangeBallCount = 15;

        [Header("Bullet Feather Pooling")]
        public GameObject FeatherObj;
        public List<GameObject> FeatherList = new List<GameObject>();
        public int FeatherCount = 15;

        [Header("Bullet Fireball Pooling")]
        public GameObject FireballObj;
        public List<GameObject> FireballList = new List<GameObject>();
        public int FireballCount = 15;

        [Header("Bullet RadioactiveBall Pooling")]
        public GameObject RadioactiveBallObj;
        public List<GameObject> RadioactiveBallList = new List<GameObject>();
        public int RadioactiveBallCount = 15;

        [Header("Bullet SpellOrange Pooling")]
        public GameObject SpellOrangeObj;
        public List<GameObject> SpellOrangeList = new List<GameObject>();
        public int SpellOrangeCount = 15;

        [Header("Bullet SpinYellow Pooling")]
        public GameObject SpinYellowObj;
        public List<GameObject> SpinYellowList = new List<GameObject>();
        public int SpinYellowCount = 15;
        #endregion

        [Header("DMG Count")]
        public GameObject DmgUI;
        public List<GameObject> DmgUIList = new List<GameObject>();
        public int nMaxDmgUICount = 30;

        //[Space]
        //[Header("스파크 이펙트")]
        //public ParticleSystem RocketEffect;
        //public List<ParticleSystem> RocketEffectList = new List<ParticleSystem>();
        //public int RocketEffectPoolCount = 15;



        //================================================

        private void Start()
        {
            //  CreateObj("Zombie", "Zombie_", nZombieMax, Zombie, ZombieList);
            CreateObj("DmgCounter", "DmgCounter_", nMaxDmgUICount, DmgUI, DmgUIList);
          
            #region Bullet
            CreateObj("Bullet", "Bullet_", nBulletMaxCount, BulletObj, BulletList);
            CreateObj("Bullet", "Bullet_", ArrowGreenCount, ArrowGreenObj, ArrowGreenList);
            CreateObj("Bullet", "Bullet_", BubbleRoseCount, BubbleRoseObj, BubbleRoseList);

            CreateObj("Bullet", "Bullet_", BulletGreenCount, BulletGreenObj, BulletGreenList);
            CreateObj("Bullet", "Bullet_", BulletOrangeCount, BulletOrangeObj, BulletOrangeList);

            CreateObj("Bullet", "Bullet_", CometBlueCount, CometBlueObj, CometBlueList);
            CreateObj("Bullet", "Bullet_", ElectricOrangeBallCount, ElectricOrangeBallObj, ElectricOrangeBallList);

            CreateObj("Bullet", "Bullet_", FeatherCount, FeatherObj, FeatherList);
            CreateObj("Bullet", "Bullet_", FireballCount, FireballObj, FireballList);

            CreateObj("Bullet", "Bullet_", RadioactiveBallCount, RadioactiveBallObj, RadioactiveBallList);
            CreateObj("Bullet", "Bullet_", SpellOrangeCount, SpellOrangeObj, SpellOrangeList);
            CreateObj("Bullet", "Bullet_", SpinYellowCount, SpinYellowObj, SpinYellowList);
            #endregion


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

