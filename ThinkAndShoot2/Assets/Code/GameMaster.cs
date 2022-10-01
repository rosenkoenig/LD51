using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : AbstractGameMaster
{
    public static GameMaster Instance;

    public GameLevel gameLevel
    {
        get
        {
            return abstractLevelMaster.currentLevel as GameLevel;
        }
    }


    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    protected override void OnGameIsLoaded()
    {
        base.OnGameIsLoaded();

        StartCoroutine(waitAndStartLevel());
    }

    protected override void OnGameStarts()
    {
        base.OnGameStarts();
    }

    protected override void OnGameEnds()
    {
        base.OnGameEnds();

        UIMaster ui = uiMaster as UIMaster;
        ui.DisplayGameOver();
    }

    IEnumerator waitAndStartLevel ()
    {
        while(!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        StartGame();
    }

    private void Update()
    {
        
    }
}
