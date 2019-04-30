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
            //Debug.Log("Dmg : " + dmg);
            charInfo.CharactersData.FHP -= dmg;

            if(charInfo.CharactersData.IsLive)
            {
                if (charInfo.CharactersData.FHP > 0)
                    charInfo.HitAni();

                if (charInfo.CharactersData.FHP <= 0)
                {
                    charInfo.CharactersData.FHP = 0;
                    charInfo.CharactersData.IsLive = false;
                    charInfo.DownAni();

                    if (this.gameObject.tag.Equals("Enemy"))
                    {
                        StartCoroutine(DisObj(1.5f));
                    }
                }
            }

            
        }

        IEnumerator DisObj(float delay)
        {
            yield return new WaitForSeconds(delay);
            this.gameObject.SetActive(false);
        }

    }

}
