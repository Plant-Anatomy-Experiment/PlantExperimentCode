using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class SpeechDemo : MonoBehaviour
{
    private SpVoice spVoice;
    public int speakRate;
    public int speakVolume;
    private void Start()
    {
        spVoice = new SpVoiceClass();
        spVoice.Rate = speakRate;
        spVoice.Volume = speakVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spVoice.Speak("请使用放大镜观察百合花", SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }
    }
}
