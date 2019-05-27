using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    public class AlarmCtrl : MonoBehaviour
    {
        AudioSource _audio;
        [SerializeField, Header("효과음")]
        AudioClip[] _sfx;

        [SerializeField,Header("이벤트 중일때 효과음 재생")]
        StageManager stage;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(stage.IsStart)
            {
                if (!_audio.isPlaying)
                {
                    //Debug.Log("Alarm");
                    _audio.volume = GameManager.INSTANCE.volume.sfx;
                    _audio.PlayOneShot(_sfx[0]);
                }
                    
            }
        }


    }

}
