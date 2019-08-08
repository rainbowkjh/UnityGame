using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Manager
    {
        public class CameraCtrl : MonoBehaviour
        {
            [SerializeField, Header("높이")]
            float hei;
            [SerializeField, Header("거리")]
            float dis;
            [SerializeField, Header("속도")]
            float camMove;
            [SerializeField, Header("타겟 눈 높이??")]
            float offset;

            float originHei;
            float originDis;
            float originSpeed;

            /// <summary>
            /// 플레이어 타겟
            /// </summary>
            Transform targetTr;

            [SerializeField]
            LayerMask objLayer;

            public float Hei { get => hei; set => hei = value; }            
            public float CamMove { get => camMove; set => camMove = value; }
            public float Dis { get => dis; set => dis = value; }
            public float OriginHei { get => originHei; set => originHei = value; }
            public float OriginDis { get => originDis; set => originDis = value; }
            public float OriginSpeed { get => originSpeed; set => originSpeed = value; }

            private void Start()
            {
                targetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

                OriginHei = hei; //초기 높이 값 저장(장애물이 화면을 가리면 높이가 변경)
                OriginDis = Dis;
                originSpeed = camMove;
            }

            #region 장애물 처리
            /*
             private void Update()
            {
                #region 투명화
                
                 RaycastHit[] hits;
                hits = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity, objLayer);

                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    Renderer renderer = hit.transform.GetComponent<Renderer>();

                    //랜더 투명 
                    if (renderer)
                    {
                        renderer.material.shader = Shader.Find("Transparent/Diffuse");
                        Color tempColor = renderer.material.color;
                        tempColor.a = 0.3f;
                        renderer.material.color = tempColor;
                    }

                    // hit.transform.GetComponent<MeshRenderer>().enabled = false;
                }
                
            }
            #endregion        
             */
            
            #endregion

            private void LateUpdate()
            {
                Vector3 pos = targetTr.position + (-Vector3.forward * Dis) + Vector3.up * Hei;
                transform.position = Vector3.Slerp(transform.position, pos, CamMove * Time.deltaTime);
                transform.LookAt(targetTr.position + Vector3.up * offset);
            }
             
            /// <summary>
            /// 카메라의 장애물 감지 콜라이더를
            /// 높이가 변경 되지 않는 높이로 계속 이동을 시키면서
            /// 장애물을 감지 시킨다
            /// (장애물을 만나면 카메라와 장애물 감지 위치가 서로 다르다)
            /// </summary>
            public void OriginPosMove(Transform collTr)
            {
                Vector3 pos = targetTr.position + (-Vector3.forward * OriginDis) + Vector3.up * OriginHei;
                collTr.position = Vector3.Slerp(collTr.position, pos, CamMove * Time.deltaTime);
                collTr.LookAt(targetTr.position + Vector3.up * offset);
            }

        }

    }
}
