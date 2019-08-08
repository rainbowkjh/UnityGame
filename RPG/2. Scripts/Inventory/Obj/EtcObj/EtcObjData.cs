using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Inventory
    {
        public class EtcObjData : MonoBehaviour
        {
            protected PlayerCtrl player;

            [SerializeField] private int minValue;
            [SerializeField] private int maxValue;

            [SerializeField] protected GameObject iconSpr;

            public int MaxValue { get => maxValue; set => maxValue = value; }
            public int MinValue { get => minValue; set => minValue = value; }

            void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            }

            private void LateUpdate()
            {
                if (!Manager.GameManager.INSTANCE.isSceneMove)
                    iconSpr.transform.LookAt(Camera.main.transform.position);
            }


        }

    }
}
