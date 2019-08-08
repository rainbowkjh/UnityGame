using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 헬기 등 제어 (상위 오브젝트)
/// 
/// VehCtrl이 원래는 탑승차량 생각해서 만들었는데
/// 이벤트 연출시 캐릭터에도 비슷하게 사용 되서
/// 같이 사용 
/// </summary>
namespace Black
{
    namespace Veh
    {
        public class VehCtrl : MonoBehaviour
        {
            [SerializeField]
           protected bool isLive = true; //이동 가능
            [SerializeField]
            protected bool isMove = true;
            [SerializeField]
            protected float moveSpeed;
            [SerializeField]
            private bool isAttack = false; //공격 하도록 함

            //[SerializeField, Header("이동 경로")]
            //protected GameObject movePosObj;

            [SerializeField]
            private Transform nextMovePos;

            [SerializeField, Header("폭발 후 사라지기까지 딜레이")]
            float disDelay = 3.0f;

            [SerializeField, Header("헬기 터질때 회전 속도")]
            protected float ExplosionRotSpeed = 150f;

            /// <summary>
            /// 폭발 후 외전 시킴
            /// </summary>
            private bool isRot = false;

            /// <summary>
            /// 슬로우 효과
            /// </summary>
            private bool isSlow = false;

            /// <summary>
            /// 플레이어와 일정거리가 되면 카메라를 흔든다.
            /// </summary>
            [SerializeField]
            private bool isShake = false;

           protected Characters.PlayerCtrl player;

            [SerializeField]
            protected ShakeCamera shakeCam;

            protected AudioSource _audio;

            public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }        
            public bool IsLive { get => isLive; set => isLive = value; }
            public bool IsRot { get => isRot; set => isRot = value; }
            public bool IsSlow { get => isSlow; set => isSlow = value; }
            public bool IsShake { get => isShake; set => isShake = value; }
            public bool IsAttack { get => isAttack; set => isAttack = value; }
            public Transform NextMovePos { get => nextMovePos; set => nextMovePos = value; }

            protected virtual void Start()
            {
                _audio = GetComponent<AudioSource>();                

                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
            }

            /// <summary>
            /// live가 false가 되면
            /// 잠시 후 오브젝트를 비활성화 또는 삭제
            /// </summary>
            /// <returns></returns>
            protected IEnumerator ObjDis()
            {
                yield return new WaitForSeconds(disDelay);

                // Manager.GameManager.INSTANCE.PlayerSpeed = 1.0f;
                Time.timeScale = 1.0f;

                Destroy(gameObject);
                //gameObject.SetActive(false);
            }



        }

    }
}
