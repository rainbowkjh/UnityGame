
using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoInfo : MonoBehaviour
{
    [SerializeField, Header("Ammo Text")]
    Text ammoText;

    WeaponeCtrl weaponeCtrl;   

    public void AmmoText(int cur, int max)
    {
        ammoText.text = cur + " / " + max;
    }

}
