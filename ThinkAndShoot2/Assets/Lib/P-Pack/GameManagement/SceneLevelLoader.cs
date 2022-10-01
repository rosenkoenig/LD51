using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelLoader : AbstractLevelLoaderOf<int>
{
  public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;

  protected override void DoLoadLevel(int levelIdx)
  {
    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levels[levelIdx], loadSceneMode);

    StartCoroutine(checkSceneLoad(asyncOperation, levelIdx));
  }

  IEnumerator checkSceneLoad (AsyncOperation asyncOperation, int levelIdx)
  {
    while (asyncOperation.isDone == false)
      yield return null;

    Scene levelScene = SceneManager.GetSceneByBuildIndex(levels[levelIdx]);
    
    foreach(GameObject rootGo in levelScene.GetRootGameObjects())
    {
      AbstractLevel lvl = rootGo.GetComponent<AbstractLevel>();
      if (lvl)
      {
        loadedLevels[levelIdx] = lvl;
      }
    }
  }

  protected override void DoUnloadLevel(int levelIdx)
  {
    SceneManager.UnloadSceneAsync(levels[levelIdx]);
  }

  protected override bool LevelIsLoaded(int levelIdx)
  {
    return SceneManager.GetSceneByBuildIndex(levels[levelIdx]).isLoaded;
  }
}
