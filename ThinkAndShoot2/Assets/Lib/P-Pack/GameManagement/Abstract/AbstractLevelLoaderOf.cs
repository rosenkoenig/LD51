using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevelLoaderOf<T> : AbstractLevelLoader
{
  [SerializeField] protected T[] levels = null;

  public override void Init()
  {
    loadedLevels = new AbstractLevel[levels.Length];
  }

  /*
  protected void PrepareLevelsAssets()
  {
    foreach (AbstractLevel abstractLevel in levels)
    {
      abstractLevel.gameObject.SetActive(false);
    }
  }*/

  public override void PreloadAllLevels()
  {
    for (int i = 0; i < levels.Length; i++)
    {
      isPreloading = true;
      if (loadedLevels[i] == null)
        PreloadLevel(i, OnPreloadEnds);
    }
  }

  protected void OnPreloadEnds (AbstractLevel lvl)
  {
    if (loadedLevels[loadedLevels.Length-1] != null)
      isPreloading = false;
  }

  public override void LoadLevel(int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    Debug.Log(isPreloading);
    StartCoroutine(waitPreloadThenLoad(levelIdx, callbackMethod));
    //return GameObject.Instantiate(levels[levelIdx].gameObject, levelsParent).GetComponent<AbstractLevel>();
  }

  protected void PreloadLevel (int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    StartCoroutine(loadProcess(levelIdx, callbackMethod));
  }

  IEnumerator waitPreloadThenLoad (int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    while (isPreloading)
      yield return null;

    StartCoroutine(loadProcess(levelIdx, callbackMethod));
  }

  IEnumerator loadProcess(int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    if (loadedLevels[levelIdx] != null)
      callbackMethod?.Invoke(loadedLevels[levelIdx]);
    else
    {
      DoLoadLevel(levelIdx);

      while (!LevelIsLoaded(levelIdx))
        yield return null;

      callbackMethod?.Invoke(loadedLevels[levelIdx]);
    }
  }


  public override void UnLoadLevel(int levelIdx)
  {
    //Destroy(loadedLevels[levelIdx].gameObject);
    DoUnloadLevel(levelIdx);
    loadedLevels[levelIdx] = null;
  }


  public override void ReloadLevel(int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    if (reloadProcessInstance == null)
    {
      reloadProcessInstance = StartCoroutine(reloadProcess(levelIdx, callbackMethod));
    }
  }

  Coroutine reloadProcessInstance = null;
  IEnumerator reloadProcess(int levelIdx, OnLevelIsLoaded callbackMethod)
  {
    UnLoadLevel(levelIdx);

    while (LevelIsLoaded(levelIdx))
      yield return null;

    LoadLevel(levelIdx, callbackMethod);
  }

}
