using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevelMaster : MonoBehaviour
{
  public AbstractLevelSequencer levelSequencer = null;

  public AbstractLevel currentLevel { get { return levelSequencer.currentLevel; } }

  public bool currentLevelIsLoaded { get { return currentLevel != null && levelSequencer.GetLevelIndex(currentLevel) >= 0; } }

  public bool autoOpenFirstLevel = true;
  public bool unloadLevelOnClose = true;

  public virtual void Init()
  {
    levelSequencer.Init();
  }

  public virtual void OnGameIsLaunched()
  {
    if (levelSequencer.levelLoader.preload)
    {
      levelSequencer.levelLoader.PreloadAllLevels();
    }

    SetFirstLevelAsCurrent();
  }

  public virtual void OnGameIsStarted()
  {
    StartCurrentLevel();
  }

  public virtual void OnGameEnds()
  {
  }


  protected virtual void SetFirstLevelAsCurrent()
  {
    levelSequencer.SetFirstLevelAsCurrent(OnFirstLevelLoaded);
  }

  public virtual void StartCurrentLevel()
  {
    if (currentLevel.displayed == false)
      currentLevel.DisplayLevel();

    currentLevel.StartLevel();
  }

  public virtual void StopCurrentLevel()
  {
    StopLevel(currentLevel);
  }

  public virtual void StopLevel(AbstractLevel lvl)
  {
    lvl.StopLevel();
  }

  protected virtual void CloseCurrentLevel()
  {
    currentLevel.HideLevel();

    if (unloadLevelOnClose)
      levelSequencer.DestroyCurrentLevel();
  }

  public virtual void CloseLevel(AbstractLevel lvl)
  {
    lvl.HideLevel();
    levelSequencer.DestroyLevel(lvl);
  }

  public virtual void OpenCurrentLevel()
  {
    OpenLevel(currentLevel);
  }

  public virtual void OpenLevel(AbstractLevel lvl)
  {
    lvl.DisplayLevel();
  }

  public virtual void SetNextLevelAsCurrent(OnLevelIsLoaded callbackMethod)
  {
    if (levelSequencer.IsNextLevelAvailable())
      levelSequencer.SetNextLevelAsCurrent(callbackMethod);
    else
      OnLevelSequenceOver();
  }

  public virtual void SetNextLevelAsCurrentAndOpen(bool closePrevious)
  {
    if (closePrevious) CloseCurrentLevel();

    if (levelSequencer.IsNextLevelAvailable())
    {
      levelSequencer.SetNextLevelAsCurrent((AbstractLevel lvl) => { OpenCurrentLevel(); });
    }
    else
      OnLevelSequenceOver();
  }

  public virtual void SetLevelIndexAsCurrent(int index, OnLevelIsLoaded callbackMethod)
  {
    levelSequencer.ForceLevelIndex(index, callbackMethod);
  }

  public virtual void RestartCurrentLevel()
  {
    StopCurrentLevel();
    levelSequencer.ReloadLevel(currentLevel, (AbstractLevel lvl) => { OnLevelLoaded(lvl); OpenCurrentLevel(); });
  }

  protected virtual void OnLevelLoaded(AbstractLevel lvl) { }

  protected virtual void OnFirstLevelLoaded(AbstractLevel lvl)
  {
    if (autoOpenFirstLevel)
      OpenCurrentLevel();

    OnLevelLoaded(lvl);
  }

  protected virtual void OnLevelSequenceOver()
  {
    GameMaster.Instance.EndGame("");
  }
}
