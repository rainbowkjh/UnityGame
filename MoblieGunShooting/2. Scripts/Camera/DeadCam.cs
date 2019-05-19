using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCam : MonoBehaviour
{
    Animator ani;

    readonly int hashDeadCam = Animator.StringToHash("DeadCam");

    public Animator Ani
    {
        get
        {
            return ani;
        }

        set
        {
            ani = value;
        }
    }

    private void Start()
    {
        Ani = GetComponent<Animator>();

        //쉐이크 카메라와 충돌로 사용할 때 활성화 시킨다
        Ani.enabled = false;
    }

    public void DeadCamAni()
    {
        //Debug.Log("Ani : " + Ani);
        Ani.enabled = true;
        Ani.SetTrigger(hashDeadCam);
    }

}
