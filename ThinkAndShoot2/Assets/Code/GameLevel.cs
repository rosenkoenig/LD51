using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : AbstractLevel
{
    public PlayerMode startMode = PlayerMode._FPS;
    public PlayerModeHandler playerModeHandler;
    public Cinemachine.CinemachineVirtualCamera levelTopCam;

    public List<Enemy> levelEnemies;

    public EnemyGroupsSpawner m_groupsSpawner;

    public override string GetLevelName()
    {
        return "";
    }

    // Start is called before the first frame update
    void Start()
    {
        levelEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(displayed)
        {
            playerModeHandler.UpdatePlayerModeTimer();
            m_groupsSpawner.UpdateChapters();
        }
    }

    public override void StartLevel()
    {
        base.StartLevel();

        playerModeHandler.SetMode(PlayerMode._FPS);
        m_groupsSpawner.OnLevelStarts();
    }
}
