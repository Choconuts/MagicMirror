using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using BaiduAip.Speech;
using UnityEngine;

public class recognition : MonoBehaviour
{
    private string apiKey = "vUsetcAC7p4CGudxGNLeRWv5";
    private string secretKey = "qXRSw2rIGkvt36YQS0yQ9PyZahQrtCeH";
    public int audioNum = 2;
    public float inteval = 1.0f;
    private ArrayList _audioClips = new ArrayList();
    
    private AudioClip _clipRecord;
    private Asr _asr;
    
    // Start is called before the first frame update
    void Start()
    {
        _asr = new Asr(apiKey, secretKey);
        StartCoroutine(_asr.GetAccessToken());
        StartCoroutine(Timer());
        _clipRecord = Microphone.Start(null, false, 30, 16000);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(inteval);
            Microphone.End(null);
//            print(_clipRecord.length);
            updateClips();
            AudioClip currentClip = AudioClipHelper.Combine((AudioClip[]) _audioClips.ToArray(typeof(AudioClip)));
//            AudioClip currentClip = AudioClipHelper.Combine(_audioClips);
//            AudioClip currentClip = (AudioClip) _audioClips[0];
            var data = Asr.ConvertAudioClipToPCM16(currentClip);
            StartCoroutine(_asr.Recognize(data, s =>
            {
                String text = s.result != null && s.result.Length > 0 ? s.result[0] : "未识别到声音";
                Debug.Log(DateTime.Now+"："+text);
            }));
        }
    }

    void updateClips()
    {
        _audioClips.Add(_clipRecord);
        _clipRecord = Microphone.Start(null, false, 30, 16000);
//        print(_audioClips.Count);
        if (_audioClips.Count > audioNum)
        {
            _audioClips.RemoveAt(0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
