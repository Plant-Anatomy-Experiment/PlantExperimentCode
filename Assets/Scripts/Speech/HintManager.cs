using System;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    private SpVoice spVoice;
    public int speakRate;
    public int speakVolume;

    public Text hintText;
    public Text hintMission;
    public Text hintStep;


    private int currentStepNum;
    private int currentMission;
    
    private float hintTime;

    private void Awake()
    {
        spVoice = new SpVoiceClass();
        spVoice.Rate = speakRate;
        spVoice.Volume = speakVolume;
        currentStepNum = 0;
    }

    private void Update()
    {
        if (hintTime > 0)
        {
            hintTime -= Time.deltaTime;
            if (hintTime <= 0)
                hintText.gameObject.SetActive(false);
        }
    }

    public void SpeakHint(string text)
    {
        spVoice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }

    public void ChangeHintText(string newText)
    {
        hintText.text = newText;
    }

    public void ChangeHintMission(int mission)
    {
        switch (mission)
        {
            case 1:
                hintMission.text = "当前任务：观察百合花的结构";
                break;
            case 2:
                hintMission.text = "当前任务：百合花的形态分解";
                break;
            case 3:
                hintMission.text = "当前任务：制作雄蕊花粉装片";
                break;
            case 4:
                hintMission.text = "当前任务：用显微镜观察装片";
                break;
            case 5:
                hintMission.text = "当前任务：解剖并观察子房结构";
                break;
            case 6:
                hintMission.text = "当前任务：已完成全部任务";
                break;
        }
    }

    public void ChangeHintStep(int step)
    {
        hintStep.text = "步骤：" + step.ToString().PadLeft(2, '0') + " / 17";
    }

    public void ShowHint(GameManager.LilyPhaseStatus status)
    {
        string newText;
        switch (status)
        {
            case GameManager.LilyPhaseStatus.MagnifierObserveWholeLily:
                newText = "请使用放大镜观察百合花";
                currentMission = 1;
                break;
            case GameManager.LilyPhaseStatus.PutDownCultureDish:
                currentMission = 2;
                newText = "请将培养皿放置在工作区";
                break;
            case GameManager.LilyPhaseStatus.LilyDissectPetal:
                newText = "请撕下一片百合花的花瓣，\n并将其放在培养皿中";
                break;
            case GameManager.LilyPhaseStatus.LilyDissectStamen:
                newText = "请使用镊子取下百合花的一支雄蕊，\n并将其放在培养皿中";
                break;
            case GameManager.LilyPhaseStatus.LilyDissectPistil:
                newText = "请使用镊子取下百合花的雌蕊，\n并将其放在培养皿中";
                break;
            case GameManager.LilyPhaseStatus.PutDownGlassSlide:
                currentMission = 3;
                newText = "请将载玻片放置在工作区";
                break;
            case GameManager.LilyPhaseStatus.DropWaterOnGlassSlide:
                newText = "请使用滴管向载玻片上滴一滴清水";
                break;
            case GameManager.LilyPhaseStatus.TakeLilyStamenInCultureDish:
                newText = "请用镊子夹取培养皿中的雄蕊";
                break;
            case GameManager.LilyPhaseStatus.PutStamenInWaterDrop:
                newText = "请将百合花的雄蕊头部浸在载玻片上的水滴中";
                break;
            case GameManager.LilyPhaseStatus.PutOnCoverGlass:
                newText = "请将百合花雄蕊放回培养皿中，\n用镊子夹取盖玻片并盖在载玻片上";
                break;
            case GameManager.LilyPhaseStatus.PutGlassSlideOnTelescopeBase:
                currentMission = 4;
                newText = "请将装片移动到显微镜的底座上";
                break;
            case GameManager.LilyPhaseStatus.UseTelescopeView:
                newText = "请点击显微镜的镜头，观察花粉装片";
                break;
            case GameManager.LilyPhaseStatus.CutLilyPistilInCultureDish:
                currentMission = 5;
                newText = "请使用解剖刀，将培养皿中雌蕊的子房部分切下";
                break;
            case GameManager.LilyPhaseStatus.TakeOvaryFromCultureDish:
                newText = "请使用镊子将百合花子房夹取到工作区桌面上";
                break;
            case GameManager.LilyPhaseStatus.CutLilyOvary:
                newText = "请使用解剖刀切开百合花子房";
                break;
            case GameManager.LilyPhaseStatus.MagnifierObserveOvary:
                newText = "请使用放大镜，观察百合花子房的内部结构";
                break;
            case GameManager.LilyPhaseStatus.End:
                currentMission = 6;
                newText = "恭喜你完成了本实验";
                break;
            default:
                newText = "";
                break;
        }

        ChangeHintText(newText);
        SpeakHint(newText);
        hintText.gameObject.SetActive(true);
        hintTime = newText.Length * 0.25f;
        
        ChangeHintMission(currentMission);
        currentStepNum += 1;
        ChangeHintStep(currentStepNum);
    }
}