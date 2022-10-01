using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : AbstractLevelMaster
{
    public override void OnGameEnds()
    {
        base.OnGameEnds();

        CloseCurrentLevel();
    }
}
