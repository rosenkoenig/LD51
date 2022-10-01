using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractLevelSequencer : MonoBehaviour
{
  public AbstractLevelLoader levelLoader = null;
  protected AbstractLevel[] loadedLevels { get { return levelLoader.loadedLevels; } }

  public AbstractLevel currentLevel = null;
  protected int curLevelIdx = 0;

  public virtual void Init()
  {
    levelLoader.Init();
  }

  protected virtual void SetCurrentLevelIndex(int levelId, OnLevelIsLoaded onLevelIsLoaded)
  {
    if (loadedLevels[levelId] == null)
    {
      levelLoader.LoadLevel(levelId, (AbstractLevel lvl) => { currentLevel = lvl; onLevelIsLoaded?.Invoke(lvl); });
    }
    else
    {
      currentLevel = (loadedLevels[levelId]);
      onLevelIsLoaded?.Invoke(currentLevel);
    }

    curLevelIdx = levelId;

  }

  public virtual void ForceLevelIndex(int levelIdx, OnLevelIsLoaded onLevelIsLoaded)
  {
    SetCurrentLevelIndex(levelIdx, onLevelIsLoaded);
  }

  public virtual void SetFirstLevelAsCurrent(OnLevelIsLoaded onLevelIsLoaded)
  {
    SetCurrentLevelIndex(GetFirstLevelIndex(), onLevelIsLoaded);
  }

  public virtual void SetNextLevelAsCurrent(OnLevelIsLoaded onLevelIsLoaded)
  {
    SetCurrentLevelIndex(GetNextLevelIndex(), onLevelIsLoaded);
  }

  //GETTERS
  public int GetLevelIndex(AbstractLevel level)
  {
    if (loadedLevels != null)
    {
      for (int i = 0; i < loadedLevels.Length; i++)
      {
        if (loadedLevels[i] && loadedLevels[i].name == level.name) return i;
      }
    }

    return -1;
  }

  public abstract int GetNextLevelIndex();

  public abstract int GetFirstLevelIndex();

  public abstract bool IsNextLevelAvailable();

  //INDIRECTIONS
  public virtual void ReloadLevel(AbstractLevel level, OnLevelIsLoaded onLevelIsLoaded)
  {
    levelLoader.ReloadLevel(GetLevelIndex(level), onLevelIsLoaded);
  }

  public virtual void DestroyCurrentLevel()
  {
    levelLoader.UnLoadLevel(curLevelIdx);
    currentLevel = null;
    curLevelIdx = -1;
  }

  public virtual void DestroyLevel(AbstractLevel lvl)
  {
    levelLoader.UnLoadLevel(GetLevelIndex(lvl));
  }

  //TO MOVE
  /* public virtual void ChangeLevel()
   {
     int nextLevelIdx = curLevelIdx;

     if (randomLevelOrder)
     {
       if (remainingLevels.Count <= 0)
       {
         ResetRemainingLevels();
       }

       nextLevelIdx = GetLevelIndex(remainingLevels[Random.Range(0, remainingLevels.Count)]);
     }
     else
     {
       nextLevelIdx++;

       if (nextLevelIdx >= loadedLevels.Length)
         nextLevelIdx = 0;
     }

     curLevelIdx = nextLevelIdx;
   }*/
  /*
  //TO MOVE
  protected virtual void ResetRemainingLevels()
  {
    remainingLevels = new List<AbstractLevel>();
    foreach (AbstractLevel abstractLevel in loadedLevels)
    {
      remainingLevels.Add(abstractLevel);
    }
  }

  protected virtual void RemoveLevel (AbstractLevel level)
  {
    //TODO
   /* int levelId = GetLevelIndex(level);
    loadedLevels[levelId] = null;
    Destroy(currentLevel.gameObject);
    PreloadAllLevels();*/
  //}
}
