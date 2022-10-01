using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLevelLoader : AbstractLevelLoaderOf<AbstractLevel>
{
  //List<GameObject> levels = default;

  protected override void DoLoadLevel(int levelIdx)
  {
    // levels[levelIdx]
    loadedLevels[levelIdx] = levels[levelIdx];
  }

  protected override void DoUnloadLevel(int levelIdx)
  {
    loadedLevels[levelIdx] = null;
  }

  protected override bool LevelIsLoaded(int levelIdx)
  {
    return true;
  }
}
