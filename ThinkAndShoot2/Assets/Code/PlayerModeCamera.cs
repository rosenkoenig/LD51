using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerModeCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cameraModeFPS;
    CinemachineVirtualCamera cameraModeTop;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.Instance.gameLevel.playerModeHandler.onModeChanged += OnModeChanged;
        cameraModeTop = GameMaster.Instance.gameLevel.levelTopCam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnModeChanged (PlayerMode playerMode)
    {
        cameraModeFPS.enabled = playerMode == PlayerMode._FPS;
        cameraModeTop.enabled = playerMode == PlayerMode._TOP;
    }
}
