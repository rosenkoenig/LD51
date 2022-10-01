using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : AbstractLevel
{
    public PlayerModeHandler playerModeHandler;
    public Cinemachine.CinemachineVirtualCamera levelTopCam;

    public override string GetLevelName()
    {
        return "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerModeHandler.UpdatePlayerModeTimer();
    }

    public override void StartLevel()
    {
        base.StartLevel();

        playerModeHandler.SetMode(PlayerMode._TOP);
    }
}
