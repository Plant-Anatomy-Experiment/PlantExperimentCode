using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum PhaseStatus
    {
        Start,
        PutDownGlassSlide,
        DropWaterOnGlass,
        TearOnionSlice,
        CoverGlass,
        IodineDye,
        PutOnTelescope,
        End
    }

    public enum LilyPhaseStatus
    {
        MagnifierObserveWholeLily,
        PutDownCultureDish,
        LilyDissectPetal,
        LilyDissectStamen,
        LilyDissectPistil,
        PutDownGlassSlide,
        DropWaterOnGlassSlide,
        TakeLilyStamenInCultureDish,
        PutStamenInWaterDrop,
        PutOnCoverGlass,
        PutGlassSlideOnTelescopeBase,
        UseTelescopeView,
        CutLilyPistilInCultureDish,
        TakeOvaryFromCultureDish,
        CutLilyOvary,
        MagnifierObserveOvary,
        End
    }

    public enum ToolStatus
    {
        Empty,
        Tweezers,
        TweezersWithOnionSlice,
        TweezersWithCoverGlass,
        Needle,
        Scalpel,
        DropperWithWater,
        DropperWithIodine,
        GlassSlide,
        Blotter,
        Magnifier,
        TweezersWithLilyStamen,
        TweezersWithLilyPistil,
        LilyPetal,
        CultureDish,
        PollenGlassSlide,
        TweezersWithOvary
    }

    public enum SelectStatus
    {
        Empty,
        ToolsArea,
        Tweezers,
        Needle,
        Scalpel,
        GlassSlideInToolArea,
        CoverGlass,
        DropperWithWater,
        DropperWithIodine,
        Blotter,
        Magnifier,
        WorkArea,
        Onion,
        OnionSlice,
        WholeLily,
        LilyPetal,
        LilyStamen,
        LilyPistil,
        GlassSlideInWorkArea,
        IodinePointOnGlassSlide,
        BlotterPointOnGlassSlide,
        TelescopeBase,
        Telescope,
        CutOvary,
        CultureDishInToolArea,
        CultureDishInWorkArea,
        LilyStamenInCultureDish,
        LilyPistilInCultureDish,
        OvaryInCultureDish,
        OvaryOnWorkArea
    }

    public SelectStatus selectStatus;

    public ToolStatus toolStatus;

    public PhaseStatus phaseStatus;

    public LilyPhaseStatus lilyPhaseStatus;

    public TargetGlassSlide targetGlassSlide;

    public LilyGlassSlideInteractiveController lilyGlassSlide;
    public LilyCultureDishInteractiveController lilyCultureDish;
    public LilyInteractiveController lily;
    public LilyTelescopeIneractiveController lilyTelescope;


    public OnionPart onionPart;

    public Tool[] toolsInHand;
    public Tool[] toolsOnDesk;

    public HintManager hintManager;
    
    public enum CameraState
    {
        DeskView,
        MicroscopeView,
        MagnifierView
    }

    public CameraState cameraState;

    public CameraController deskCameraController;
    public ChaseCameraController chaseCameraController;
    public MagnifierCameraController magnifierCameraController;
    public Camera microscopeTargetCamera;

    public MicroscopeImageController microscopeImageController;
    public GameObject magnifierWholeLily;
    public GameObject magnifierCuttedOvary;

    public GameObject Ovary;
    public GameObject CutOvary;

    public void EnterMagnifierView()
    {
        deskCameraController.gameObject.SetActive(false);
        magnifierCameraController.gameObject.SetActive(true);
        cameraState = CameraState.MagnifierView;
    }

    public void EnterMagnifierViewWholeLily()
    {
        deskCameraController.gameObject.SetActive(false);
        magnifierCameraController.gameObject.SetActive(true);
        cameraState = CameraState.MagnifierView;
        magnifierWholeLily.SetActive(true);
        magnifierCuttedOvary.SetActive(false);
        lily.ObserveWholeLily();
    }

    public void EnterMagnifierViewCuttedOvary()
    {
        deskCameraController.gameObject.SetActive(false);
        magnifierCameraController.gameObject.SetActive(true);
        cameraState = CameraState.MagnifierView;
        magnifierWholeLily.SetActive(false);
        magnifierCuttedOvary.SetActive(true);
    }


    public void ExitMagnifierView()
    {
        deskCameraController.gameObject.SetActive(true);
        magnifierCameraController.gameObject.SetActive(false);
        cameraState = CameraState.DeskView;
        if (lilyPhaseStatus == LilyPhaseStatus.MagnifierObserveWholeLily)
        {
            lilyPhaseStatus = LilyPhaseStatus.PutDownCultureDish;
            hintManager.ShowHint(lilyPhaseStatus);
        }
        else if (lilyPhaseStatus == LilyPhaseStatus.MagnifierObserveOvary)
        {
            lilyPhaseStatus = LilyPhaseStatus.End;
            hintManager.ShowHint(lilyPhaseStatus);
        }
    }

    public void EnterMicroscopeView()
    {
        Camera deskCamera = deskCameraController.cameraControlled;
        deskCameraController.gameObject.SetActive(false);
        chaseCameraController.gameObject.SetActive(true);
        chaseCameraController.CameraChase(deskCamera.transform, deskCamera.fieldOfView,
            microscopeTargetCamera.transform, microscopeTargetCamera.fieldOfView);
        Invoke("EnterMicroscopeInvokeEnableMicroscopeCamera", 1);
    }

    public void EnterMicroscopeInvokeEnableMicroscopeCamera()
    {
        microscopeTargetCamera.gameObject.SetActive(true);
        cameraState = CameraState.MicroscopeView;
        microscopeImageController.gameObject.SetActive(true);
    }

    public void ChangeMicroscopeLens()
    {
        microscopeImageController.ChangeLens();
    }

    public void ExitMicroscopeView()
    {
        microscopeImageController.gameObject.SetActive(false);
        Camera deskCamera = deskCameraController.cameraControlled;
        chaseCameraController.gameObject.SetActive(true);
        chaseCameraController.CameraChase(microscopeTargetCamera.transform, microscopeTargetCamera.fieldOfView,
            deskCamera.transform, deskCamera.fieldOfView);
        Invoke("ExitMicroscopeInvokeDisableMicroscopeCamera", 1);
    }

    public void ExitMicroscopeInvokeDisableMicroscopeCamera()
    {
        microscopeTargetCamera.gameObject.SetActive(false);
        deskCameraController.gameObject.SetActive(true);
        cameraState = CameraState.DeskView;
        lilyPhaseStatus = LilyPhaseStatus.CutLilyPistilInCultureDish;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void Awake()
    {
        instance = this;
        selectStatus = SelectStatus.Empty;
        toolStatus = ToolStatus.Empty;
        phaseStatus = PhaseStatus.PutDownGlassSlide;
        cameraState = CameraState.DeskView;
        lilyPhaseStatus = LilyPhaseStatus.MagnifierObserveWholeLily;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    void Update()
    {
        CheckLilyExperimentStep();
    }

    private void CheckLilyExperimentStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (toolStatus == ToolStatus.Magnifier)
            {
                if (selectStatus == SelectStatus.WholeLily &&
                    lilyPhaseStatus == LilyPhaseStatus.MagnifierObserveWholeLily)
                {
                    EnterMagnifierViewWholeLily();
                    return;
                }

                if (selectStatus == SelectStatus.CutOvary &&
                    lilyPhaseStatus == LilyPhaseStatus.MagnifierObserveOvary)
                {
                    EnterMagnifierViewCuttedOvary();
                    return;
                }
            }

            if (toolStatus == ToolStatus.Empty)
            {
                if (selectStatus == SelectStatus.LilyPetal && lilyPhaseStatus == LilyPhaseStatus.LilyDissectPetal)
                {
                    TakeLilyPetal();
                    return;
                }
            }

            if (toolStatus == ToolStatus.Tweezers)
            {
                if (selectStatus == SelectStatus.LilyStamen && lilyPhaseStatus == LilyPhaseStatus.LilyDissectStamen)
                {
                    TakeLilyStamen();
                    return;
                }

                if (selectStatus == SelectStatus.LilyPistil && lilyPhaseStatus == LilyPhaseStatus.LilyDissectPistil)
                {
                    TakeLilyPistil();
                    return;
                }

                if (selectStatus == SelectStatus.LilyStamenInCultureDish &&
                    lilyPhaseStatus == LilyPhaseStatus.TakeLilyStamenInCultureDish)
                {
                    TakeLilyStamenFromCultureDish();
                    return;
                }

                if (selectStatus == SelectStatus.CoverGlass && lilyPhaseStatus == LilyPhaseStatus.PutOnCoverGlass)
                {
                    LilyTakeUpCoverGlass();
                    return;
                }

                if (selectStatus == SelectStatus.OvaryInCultureDish &&
                    lilyPhaseStatus == LilyPhaseStatus.TakeOvaryFromCultureDish)
                {
                    TakeOvaryFromCultureDish();
                    return;
                }
            }

            if (toolStatus == ToolStatus.TweezersWithCoverGlass)
            {
                if (selectStatus == SelectStatus.GlassSlideInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.PutOnCoverGlass)
                {
                    LilyPutOnCoverGlass();
                    return;
                }
            }

            if (toolStatus == ToolStatus.TweezersWithOvary)
            {
                if (selectStatus == SelectStatus.WorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.TakeOvaryFromCultureDish)
                {
                    PutDownLilyOvary();
                    return;
                }
            }

            if (toolStatus == ToolStatus.Scalpel)
            {
                if (selectStatus == SelectStatus.LilyPistilInCultureDish &&
                    lilyPhaseStatus == LilyPhaseStatus.CutLilyPistilInCultureDish)
                {
                    CutLilyPistilInCultureDish();
                    return;
                }

                if (selectStatus == SelectStatus.OvaryOnWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.CutLilyOvary)
                {
                    CutLilyOvary();
                    return;
                }
            }

            if (toolStatus == ToolStatus.LilyPetal)
            {
                if (selectStatus == SelectStatus.CultureDishInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.LilyDissectPetal)
                {
                    PutLilyPetalInCultureDish();
                    return;
                }
            }

            if (toolStatus == ToolStatus.TweezersWithLilyStamen)
            {
                if (selectStatus == SelectStatus.CultureDishInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.LilyDissectStamen)
                {
                    PutLilyStamenInCultureDish();
                    return;
                }

                if (selectStatus == SelectStatus.GlassSlideInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.PutStamenInWaterDrop)
                {
                    PutLilyStamenInWaterDrop();
                    return;
                }

                if (selectStatus == SelectStatus.CultureDishInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.PutStamenInWaterDrop ||
                    lilyPhaseStatus == LilyPhaseStatus.PutOnCoverGlass)
                {
                    PutBackLilyStamen();
                    return;
                }
            }

            if (toolStatus == ToolStatus.DropperWithWater)
            {
                if (selectStatus == SelectStatus.GlassSlideInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.DropWaterOnGlassSlide)
                {
                    LilyDropWaterOnGlassSlide();
                    return;
                }
            }

            if (toolStatus == ToolStatus.TweezersWithLilyPistil)
            {
                if (selectStatus == SelectStatus.CultureDishInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.LilyDissectPistil)
                {
                    PutLilyPistilInCultureDish();
                    return;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraState == CameraState.MicroscopeView)
            {
                ExitMicroscopeView();
                return;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (cameraState == CameraState.MagnifierView)
            {
                ExitMagnifierView();
                return;
            }

            if (cameraState == CameraState.MicroscopeView)
            {
                ChangeMicroscopeLens();
                return;
            }

            if (toolStatus == ToolStatus.Empty)
            {
                if (selectStatus == SelectStatus.Tweezers ||
                    selectStatus == SelectStatus.Needle ||
                    selectStatus == SelectStatus.Scalpel ||
                    selectStatus == SelectStatus.GlassSlideInToolArea ||
                    selectStatus == SelectStatus.DropperWithWater ||
                    selectStatus == SelectStatus.DropperWithIodine ||
                    selectStatus == SelectStatus.Blotter ||
                    selectStatus == SelectStatus.Magnifier ||
                    selectStatus == SelectStatus.CultureDishInToolArea
                )
                {
                    TakeToolFromDesk(selectStatus);
                    return;
                }
                
                if (selectStatus == SelectStatus.GlassSlideInWorkArea &&
                    lilyPhaseStatus == LilyPhaseStatus.PutGlassSlideOnTelescopeBase)
                {
                    TakeLilyGlassSlide();
                    return;
                }
            }
            
            if (toolStatus == ToolStatus.PollenGlassSlide)
            {
                if (selectStatus == SelectStatus.TelescopeBase &&
                    lilyPhaseStatus == LilyPhaseStatus.PutGlassSlideOnTelescopeBase)
                {
                    PutGlassSlideOnTelescopeBase();
                    return;
                }
            }

            if (toolStatus == ToolStatus.Tweezers ||
                toolStatus == ToolStatus.Needle ||
                toolStatus == ToolStatus.Scalpel ||
                toolStatus == ToolStatus.GlassSlide ||
                toolStatus == ToolStatus.DropperWithWater ||
                toolStatus == ToolStatus.DropperWithIodine ||
                toolStatus == ToolStatus.Blotter ||
                toolStatus == ToolStatus.Magnifier ||
                toolStatus == ToolStatus.CultureDish
            )
            {
                if (selectStatus == SelectStatus.ToolsArea)
                {
                    PutToolOnDesk();
                    return;
                }
            }

            if (toolStatus == ToolStatus.Empty)
            {
                if (selectStatus == SelectStatus.Telescope && lilyPhaseStatus == LilyPhaseStatus.UseTelescopeView)
                {
                    EnterMicroscopeView();
                    return;
                }
            }

            if (toolStatus == ToolStatus.GlassSlide)
            {
                if (selectStatus == SelectStatus.WorkArea && lilyPhaseStatus == LilyPhaseStatus.PutDownGlassSlide)
                {
                    LilyExperimentPutGlassSlideOnWorkArea();
                    return;
                }
            }

            if (toolStatus == ToolStatus.CultureDish)
            {
                if (selectStatus == SelectStatus.WorkArea && lilyPhaseStatus == LilyPhaseStatus.PutDownCultureDish)
                {
                    LilyExperimentPutCultureDishOnWorkArea();
                    return;
                }
            }
        }
    }

    private void CheckOnionExperimentStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (toolStatus == ToolStatus.DropperWithWater)
            {
                if (selectStatus == SelectStatus.GlassSlideInWorkArea && phaseStatus == PhaseStatus.DropWaterOnGlass)
                {
                    DropWaterOnGlassSlide();
                }
            }

            if (toolStatus == ToolStatus.Scalpel)
            {
                if (selectStatus == SelectStatus.Onion && phaseStatus == PhaseStatus.TearOnionSlice)
                {
                    KnifeCutOnionPart();
                }
            }

            if (toolStatus == ToolStatus.Tweezers)
            {
                if (selectStatus == SelectStatus.OnionSlice && phaseStatus == PhaseStatus.TearOnionSlice)
                {
                    TweezersTearOnionSlice();
                }
            }

            if (toolStatus == ToolStatus.TweezersWithOnionSlice)
            {
                if (selectStatus == SelectStatus.GlassSlideInWorkArea && phaseStatus == PhaseStatus.TearOnionSlice)
                {
                    PutOnionSliceOnGlassSlide();
                }
            }

            if (toolStatus == ToolStatus.Tweezers)
            {
                if (selectStatus == SelectStatus.CoverGlass && phaseStatus == PhaseStatus.CoverGlass)
                {
                    TweezersPickUpCoverGlass();
                }
            }

            if (toolStatus == ToolStatus.TweezersWithCoverGlass)
            {
                if (selectStatus == SelectStatus.GlassSlideInWorkArea && phaseStatus == PhaseStatus.CoverGlass)
                {
                    CoverGlassForOnionSlice();
                }
            }

            if (toolStatus == ToolStatus.DropperWithIodine)
            {
                if (selectStatus == SelectStatus.IodinePointOnGlassSlide && phaseStatus == PhaseStatus.IodineDye)
                {
                    DropIodineOnGlassSlide();
                }
            }

            if (toolStatus == ToolStatus.Blotter)
            {
                if (selectStatus == SelectStatus.BlotterPointOnGlassSlide && phaseStatus == PhaseStatus.IodineDye)
                {
                    PutBlotterOnGlassSlide();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (toolStatus == ToolStatus.Empty)
            {
                if (selectStatus == SelectStatus.Tweezers ||
                    selectStatus == SelectStatus.Needle ||
                    selectStatus == SelectStatus.Scalpel ||
                    selectStatus == SelectStatus.GlassSlideInToolArea ||
                    selectStatus == SelectStatus.DropperWithWater ||
                    selectStatus == SelectStatus.DropperWithIodine ||
                    selectStatus == SelectStatus.Blotter
                )
                {
                    TakeToolFromDesk(selectStatus);
                }
            }

            if (toolStatus == ToolStatus.Tweezers ||
                toolStatus == ToolStatus.Needle ||
                toolStatus == ToolStatus.Scalpel ||
                toolStatus == ToolStatus.GlassSlide ||
                toolStatus == ToolStatus.DropperWithWater ||
                toolStatus == ToolStatus.DropperWithIodine ||
                toolStatus == ToolStatus.Blotter
            )
            {
                if (selectStatus == SelectStatus.ToolsArea)
                {
                    PutToolOnDesk();
                }
            }

            if (toolStatus == ToolStatus.GlassSlide)
            {
                if (selectStatus == SelectStatus.WorkArea && phaseStatus == PhaseStatus.PutDownGlassSlide)
                {
                    PutGlassSlideOnWorkArea();
                }
            }
        }
    }

    private void TakeToolFromDesk(SelectStatus selectStatus)
    {
        switch (selectStatus)
        {
            case SelectStatus.Tweezers:
                toolStatus = ToolStatus.Tweezers;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.Tweezers)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.Needle:
                toolStatus = ToolStatus.Needle;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.Needle)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.Scalpel:
                toolStatus = ToolStatus.Scalpel;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.Scalpel)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.GlassSlideInToolArea:
                toolStatus = ToolStatus.GlassSlide;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.GlassSlide)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.CultureDishInToolArea:
                toolStatus = ToolStatus.CultureDish;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.CultureDish)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.DropperWithWater:
                toolStatus = ToolStatus.DropperWithWater;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.DropperWithWater)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.DropperWithIodine:
                toolStatus = ToolStatus.DropperWithIodine;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.DropperWithIodine)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.Magnifier:
                toolStatus = ToolStatus.Magnifier;
                foreach (var tool in toolsOnDesk)
                    if (tool.toolStatus == ToolStatus.Magnifier)
                        tool.gameObject.SetActive(false);
                break;
            case SelectStatus.Blotter:
                toolStatus = ToolStatus.Blotter;
                break;
        }

        ShowTool();
    }

    private void PutToolOnDesk()
    {
        if (toolStatus == ToolStatus.Tweezers ||
            toolStatus == ToolStatus.Needle ||
            toolStatus == ToolStatus.Scalpel ||
            toolStatus == ToolStatus.GlassSlide ||
            toolStatus == ToolStatus.DropperWithWater ||
            toolStatus == ToolStatus.DropperWithIodine ||
            toolStatus == ToolStatus.Magnifier ||
            toolStatus == ToolStatus.CultureDish
        )
            foreach (var tool in toolsOnDesk)
                if (tool.toolStatus == toolStatus)
                    tool.gameObject.SetActive(true);
        toolStatus = ToolStatus.Empty;
        ShowTool();
    }

    private void PutGlassSlideOnWorkArea()
    {
        toolStatus = ToolStatus.Empty;
        ShowTool();
        targetGlassSlide.gameObject.SetActive(true);
        phaseStatus = PhaseStatus.DropWaterOnGlass;
    }

    private void LilyExperimentPutGlassSlideOnWorkArea()
    {
        toolStatus = ToolStatus.Empty;
        ShowTool();
        lilyGlassSlide.gameObject.SetActive(true);
        lilyPhaseStatus = LilyPhaseStatus.DropWaterOnGlassSlide;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void LilyExperimentPutCultureDishOnWorkArea()
    {
        toolStatus = ToolStatus.Empty;
        ShowTool();
        lilyCultureDish.gameObject.SetActive(true);
        lilyPhaseStatus = LilyPhaseStatus.LilyDissectPetal;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeLilyPetal()
    {
        toolStatus = ToolStatus.LilyPetal;
        ShowTool();
        lily.TakeLilyPetal();
    }

    private void PutLilyPetalInCultureDish()
    {
        toolStatus = ToolStatus.Empty;
        ShowTool();
        lilyCultureDish.PutPetalOnDish();
        lilyPhaseStatus = LilyPhaseStatus.LilyDissectStamen;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeLilyStamen()
    {
        toolStatus = ToolStatus.TweezersWithLilyStamen;
        ShowTool();
        lily.TakeLilyStamen();
    }

    private void PutLilyStamenInCultureDish()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        lilyCultureDish.PutStamenOnDish();
        lilyPhaseStatus = LilyPhaseStatus.LilyDissectPistil;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeLilyPistil()
    {
        toolStatus = ToolStatus.TweezersWithLilyPistil;
        ShowTool();
        lily.TakeLilyPistil();
    }

    private void PutLilyPistilInCultureDish()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        lilyCultureDish.PutPistilOnDish();
        lilyPhaseStatus = LilyPhaseStatus.PutDownGlassSlide;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void LilyDropWaterOnGlassSlide()
    {
        lilyGlassSlide.DropWater();
        lilyPhaseStatus = LilyPhaseStatus.TakeLilyStamenInCultureDish;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeLilyStamenFromCultureDish()
    {
        toolStatus = ToolStatus.TweezersWithLilyStamen;
        ShowTool();
        lilyCultureDish.TakeStamenFromDish();
        lilyPhaseStatus = LilyPhaseStatus.PutStamenInWaterDrop;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void PutBackLilyStamen()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        lilyCultureDish.PutStamenOnDish();
    }

    private void PutLilyStamenInWaterDrop()
    {
        lilyGlassSlide.PutStamenInWaterDrop();
        lilyPhaseStatus = LilyPhaseStatus.PutOnCoverGlass;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void LilyTakeUpCoverGlass()
    {
        toolStatus = ToolStatus.TweezersWithCoverGlass;
        ShowTool();
    }

    private void LilyPutOnCoverGlass()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        lilyGlassSlide.PutCoverGlass();
        lilyPhaseStatus = LilyPhaseStatus.PutGlassSlideOnTelescopeBase;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeLilyGlassSlide()
    {
        toolStatus = ToolStatus.PollenGlassSlide;
        ShowTool();
        lilyGlassSlide.gameObject.SetActive(false);
    }

    private void PutGlassSlideOnTelescopeBase()
    {
        toolStatus = ToolStatus.Empty;
        ShowTool();
        lilyTelescope.PutGlassSlideOnTelescopeBase();
        lilyPhaseStatus = LilyPhaseStatus.UseTelescopeView;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void CutLilyPistilInCultureDish()
    {
        lilyCultureDish.CutPistil();
        lilyPhaseStatus = LilyPhaseStatus.TakeOvaryFromCultureDish;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void TakeOvaryFromCultureDish()
    {
        toolStatus = ToolStatus.TweezersWithOvary;
        ShowTool();
        lilyCultureDish.TakeOvaryFromDish();
    }

    private void PutDownLilyOvary()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        Ovary.SetActive(true);
        lilyPhaseStatus = LilyPhaseStatus.CutLilyOvary;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void CutLilyOvary()
    {
        Ovary.SetActive(false);
        CutOvary.SetActive(true);
        lilyPhaseStatus = LilyPhaseStatus.MagnifierObserveOvary;
        hintManager.ShowHint(lilyPhaseStatus);
    }

    private void DropWaterOnGlassSlide()
    {
        targetGlassSlide.DropWater();
        phaseStatus = PhaseStatus.TearOnionSlice;
    }

    private void KnifeCutOnionPart()
    {
        onionPart.CutWithKnife();
    }

    private void TweezersTearOnionSlice()
    {
        toolStatus = ToolStatus.TweezersWithOnionSlice;
        ShowTool();
        onionPart.TakeOnionSlice();
    }

    private void PutOnionSliceOnGlassSlide()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        targetGlassSlide.PutOnionSlice();
        phaseStatus = PhaseStatus.CoverGlass;
    }

    private void TweezersPickUpCoverGlass()
    {
        toolStatus = ToolStatus.TweezersWithCoverGlass;
        ShowTool();
    }

    private void CoverGlassForOnionSlice()
    {
        toolStatus = ToolStatus.Tweezers;
        ShowTool();
        targetGlassSlide.PutCoverGlass();
        phaseStatus = PhaseStatus.IodineDye;
    }

    private void DropIodineOnGlassSlide()
    {
        targetGlassSlide.DropIodine();
    }

    private void PutBlotterOnGlassSlide()
    {
        targetGlassSlide.PutOnBlotter();
        phaseStatus = PhaseStatus.PutOnTelescope;
    }

    private void ShowTool()
    {
        foreach (var tool in toolsInHand)
            if (tool.toolStatus == toolStatus)
                tool.gameObject.SetActive(true);
            else
                tool.gameObject.SetActive(false);
    }
}