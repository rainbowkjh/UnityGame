using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class DmgCounter : MonoBehaviour
    {
        CharactersInfo charInfo;

        private void Start()
        {
            charInfo = GetComponent<CharactersInfo>();
        }

        public void DmgCount(float dmg)
        {
            Debug.Log("Dmg : " + dmg);
            charInfo.CharactersData.FHP -= dmg;
        }
    }

}
