using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnLevelIsLoaded(AbstractLevel levelLoaded);

public abstract class AbstractLevelLoader : MonoBehaviour
{
  public bool preload = false;
  public System.Action onPreloadIsEnded = null;
  [ReadOnly] public bool isPreloading = false;
  [ReadOnly] public AbstractLevel[] loadedLevels = null;

  public abstract void Init();
  public abstract void LoadLevel(int levelIdx, OnLevelIsLoaded callbackMethod);
  protected abstract void DoLoadLevel(int levelIdx);
  public abstract void UnLoadLevel(int levelIdx);
  protected abstract void DoUnloadLevel(int levelIdx);
  public abstract void ReloadLevel(int levelIdx, OnLevelIsLoaded callbackMethod);
  protected abstract bool LevelIsLoaded(int levelIdx);

  public virtual void PreloadAllLevels() { }
}
