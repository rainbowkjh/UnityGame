/*
    18.11.11
    카메라 흔들림 효과
    공격, 피격 당할떄 등..
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    public Transform shakeCam;
    private Vector3 originPos;
    private Quaternion originRot;

	// Use this for initialization
	void Start () {
        originPos = shakeCam.localPosition;
        originRot = shakeCam.localRotation;

	}
	
    //흔들리는 시간, 좌표, 회전 값 0.05,0.03,0.1
    public IEnumerator ShakeCamAct(float duration = 0.05f, float magnitudePos = 0.1f, float magnitudeRot =0.1f)
    {
        float passTime = 0.0f;

        while(passTime<duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCam.localPosition = shakePos * magnitudePos;

            Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));
            shakeCam.localRotation = Quaternion.Euler(shakeRot);

            passTime += Time.deltaTime;

            yield return null;
        }

        shakeCam.localPosition = originPos;
        shakeCam.localRotation = originRot;
    }

    
}
