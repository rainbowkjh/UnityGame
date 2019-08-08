/*
    18.11.18
    메모리 풀링
    탄피 효과 및 탄 피격 이펙트 관리
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPooling : MonoBehaviour {

    #region Bullet&Magic
    
    [Header("BubbleBlue")]
    public GameObject BubbleBlueObj;
    public List<GameObject> BubbleBlueList = new List<GameObject>();
    public int BubbleBluetMaxCount = 15;

    [Header("BubbleRose")]
    public GameObject BubbleRoseObj;
    public List<GameObject> BubbleRoseList = new List<GameObject>();
    public int BubbleRoseMaxCount = 15;

    [Header("Feather")]
    public GameObject FeatherRoseObj;
    public List<GameObject> FeatherList = new List<GameObject>();
    public int FeatherMaxCount = 15;

    [Header("CometBlue")]
    public GameObject CometBlueObj;
    public List<GameObject> CometBlueList = new List<GameObject>();
    public int CometBlueMaxCount = 15;

    [Header("FireBall")]
    public GameObject FireBallObj;
    public List<GameObject> FireBallList = new List<GameObject>();
    public int FireBallCount = 15;


    #endregion

   // [Space]
    #region 근접 무기 이펙트
    [Header("Spark")]
    public GameObject SparkObj;
    public List<GameObject> SparkList = new List<GameObject>();
    public int SparkCount = 15;

    [Header("AttackExplosion")]
    public GameObject AttackExplosionObj;
    public List<GameObject> AttackExplosionList = new List<GameObject>();
    public int AttackExplosionCount = 15;

    #endregion

 //   [Space]
    #region 마우스 왼쪽 클릭 이펙트
    [Header("MouseEffect")]
    public ParticleSystem mouseEffect;
    public List<ParticleSystem> mouseEffectList = new List<ParticleSystem>();
    public int mouseCount = 5;
    #endregion

  //  [Space]
    #region 수류탄 관련 파티클
    [Header("Grenade")]
    public GameObject GrenadeObj;
    public List<GameObject> GrenadeList = new List<GameObject>();
    public int GrenadeMaxCount = 3;

    [Header("Grenade 폭발 이펙트")]
    public ParticleSystem GrenadeExplosion;
    public List<ParticleSystem> GrenadeExplosionList = new List<ParticleSystem>();
    public int GrenadeExplosionCount = 5;
    #endregion

   // [Space]
    #region 회복 아이템 사용 시 이펙트
    [Header("HP회복 이펙트")]
    public ParticleSystem hpRecoveryEffect;
    public List<ParticleSystem> hpRecList = new List<ParticleSystem>();
    public int hpRevCount = 5;

    [Header("회복2 이펙트")]
    public ParticleSystem recoveryEffect;
    public List<ParticleSystem> recList = new List<ParticleSystem>();
    public int revCount = 5;
    #endregion

   // [Space]
    #region 나머지 효과
    [Header("타격 이펙트")]
    public ParticleSystem hitEffect;
    public List<ParticleSystem> hitList = new List<ParticleSystem>();
    public int hitCount = 30;

    [Header("탄 타격 이펙트")]
    public ParticleSystem bulletHitEffect;
    public List<ParticleSystem> bulletHitList = new List<ParticleSystem>();
    public int bulletHitCount = 30;

    [Header("Dmg Counter")]
    public GameObject DmgUIObj;
    public List<GameObject> DmgUIList = new List<GameObject>();
    public int DmgUICount = 30;

    #endregion

    //================================================

    private void Start()
    {
        //발사체
        CreateObj("BubbleBlue", "BubbleBlue_", BubbleBluetMaxCount, BubbleBlueObj, BubbleBlueList);
        CreateObj("BubbleRose", "BubbleRose_", BubbleRoseMaxCount, BubbleRoseObj, BubbleRoseList);
        CreateObj("Feather", "Feather_", FeatherMaxCount, FeatherRoseObj, FeatherList);
        CreateObj("CometBlue", "CometBlue_", CometBlueMaxCount, CometBlueObj, CometBlueList);
        CreateObj("FireBall", "FireBall_", FireBallCount, FireBallObj, FireBallList);

        //근접 무기
        CreateObj("Spark", "Spark_", SparkCount, SparkObj, SparkList);
        CreateObj("AttackExplosion", "AttackExplosion", AttackExplosionCount, AttackExplosionObj, AttackExplosionList);

        //마우스 포인터(클릭) 이펙트
        CreateParticle("MousePointer", "MouseEffect_", mouseCount, mouseEffect, mouseEffectList);        
        CreateObj("Grenade", "Grenade_", GrenadeMaxCount, GrenadeObj, GrenadeList);
        CreateParticle("GrenadeExplosion", "GrenadeExplosion_", GrenadeExplosionCount, GrenadeExplosion, GrenadeExplosionList);

        //상태 회복 이펙트(마법??, 아이템 사용)
        CreateParticle("HPRecovery", "HPRecovery_", hpRevCount, hpRecoveryEffect, hpRecList);
        CreateParticle("Recovery", "Recovery_", revCount, recoveryEffect, recList);

        CreateParticle("Hit", "Hit_", hitCount, hitEffect, hitList);
        CreateParticle("Bullet_Hit", "Bullet_Hit_", bulletHitCount, bulletHitEffect, bulletHitList);
    
        CreateObj("DmgUI", "DmgUI_", DmgUICount, DmgUIObj, DmgUIList);

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

    //Dmg UI 비활성화 안되면 
    //활성화 되어 있는 UI를 강제로 비활성화 시켜서 재사용 시킨다
    public GameObject GetObjPoolEx(int MaxCount, List<GameObject> ObjList)
    {
        for (int i = 0; i < MaxCount; i++)
        {
            if (ObjList[i].activeSelf == true)
            {
                ObjList[i].SetActive(false); //강제로 비활성화 시킨다
            }

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
