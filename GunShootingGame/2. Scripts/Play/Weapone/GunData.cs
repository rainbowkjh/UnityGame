using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    [SerializeField]
    float minDmg;
    [SerializeField]
    float maxDmg;
    [SerializeField]
    int mag;
    [SerializeField]
    int maxMag;
    [SerializeField]
    int curAmmo;
    [SerializeField]
    int maxAmmo;

    public GunData(float minDmg, float maxDmg, int mag, int maxMag,
        int curAmmo, int maxAmmo)
    {
        this.minDmg = minDmg;
        this.maxDmg = maxDmg;
        this.mag = mag;
        this.maxMag = maxMag;
        this.curAmmo = curAmmo;
        this.maxMag = maxMag;
    }

    public float MinDmg
    {
        get
        {
            return minDmg;
        }

        set
        {
            minDmg = value;
        }
    }

    public float MaxDmg
    {
        get
        {
            return maxDmg;
        }

        set
        {
            maxDmg = value;
        }
    }

    public int Mag
    {
        get
        {
            return mag;
        }

        set
        {
            mag = value;
        }
    }

    public int MaxMag
    {
        get
        {
            return maxMag;
        }

        set
        {
            maxMag = value;
        }
    }

    public int CurAmmo
    {
        get
        {
            return curAmmo;
        }

        set
        {
            curAmmo = value;
        }
    }

    public int MaxAmmo
    {
        get
        {
            return maxAmmo;
        }

        set
        {
            maxAmmo = value;
        }
    }
}
