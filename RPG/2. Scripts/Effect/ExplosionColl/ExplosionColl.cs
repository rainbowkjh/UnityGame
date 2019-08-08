using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace EffectCtrl
    {
        public class ExplosionColl : MonoBehaviour
        {


            [SerializeField, Header("콜라이더 감지 할 오브젝트 태그")]
            eObjTag objTag;

            string triggerTag;

            /// <summary>
            /// 이펙트 연출 한번만
            /// </summary>
            bool isEnter = false;

            [SerializeField, Header("폭발 이펙트")]
            ParticleSystem[] explosionPar;
            [SerializeField, Header("폭발 효과로 튕결 낸 경우 방향")]
            Vector3 exploVec;

            [SerializeField, Header("밀어내는 힘")]
            float addForce = 0.5f;

            [SerializeField, Header("폭발 후 오브젝트를 회전 시킴")]
            bool isRot = false;

            AudioSource _audio;
            [SerializeField]
            AudioClip _sfx;

            [SerializeField, Header("슬로우 효과")]
            bool isSlow = false;

            [SerializeField, Header("이벤트 연츌로 폭발 시 플레이어 정지")]
            bool isStop = false;


            [SerializeField,Header("폭발 시 카메라 흔들림")]
            bool isShake = false;

            [SerializeField]
            ShakeCamera shake;

            [SerializeField, Header("폭발 효과 반복")]
            bool isExplosionRot = false;

            private void Start()
            {
                for(int i=0;i<explosionPar.Length;i++)
                {
                    explosionPar[i].gameObject.SetActive(false);
                }
                
                _audio = GetComponent<AudioSource>();
                triggerTag = GameManager.INSTANCE.ObjTagSetting(objTag);

            }

            private void OnTriggerEnter(Collider other)
            {
                if (!isEnter)
                {
                    if (other.transform.CompareTag(triggerTag))
                    {
                        //폭발 이벤트를 해당 태그가
                        //감지 될때마다 발생하는 것을 막는다
                        if (!isExplosionRot)
                            isEnter = true;

                        Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx);

                        for (int i = 0; i < explosionPar.Length; i++)
                        {
                            explosionPar[i].gameObject.SetActive(true);
                            if (!explosionPar[i].isPlaying)
                                explosionPar[i].Play();
                        }

                        

                        //리지디 바디가 있으면
                        //폭빌 효과로 튕겨(?) 낸다
                        Rigidbody rid = other.GetComponent<Rigidbody>();
                        if (rid != null)
                        {
                            rid.useGravity = enabled;
                            rid.AddForce(exploVec * addForce, ForceMode.Impulse);
                        }

                        if(isRot)
                        {
                            rid.GetComponent<Veh.VehCtrl>().IsRot = true;
                        }

                        if(isSlow)
                        {
                            rid.GetComponent<Veh.VehCtrl>().IsSlow = true;
                        }

                        if (isShake)
                            StartCoroutine(shake.ShakeCamAct(0.2f, 0.5f, 0.5f));
                    }
                }
            }
        }

    }
}

