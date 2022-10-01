using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelSequencer : AbstractLevelSequencer
{
  List<int> remainingLevels = null;

  public bool loop = false;
  public bool removeFromListSelectedLevel = true;


  public override int GetFirstLevelIndex()
  {
    ResetRemainingLevels();

    remainingLevels.Remove(0);

    return 0;
  }

  public override int GetNextLevelIndex()
  {

    int nextLevelIdx = curLevelIdx;

    if (loop && remainingLevels.Count <= 0)
    {
      ResetRemainingLevels();
    }

    nextLevelIdx = remainingLevels[Random.Range(0, remainingLevels.Count)];

    if (removeFromListSelectedLevel)
    {
      remainingLevels.Remove(nextLevelIdx);
    }

    return nextLevelIdx;
  }

  protected virtual void ResetRemainingLevels()
  {
    remainingLevels = new List<int>();
    for (int i = 0; i < loadedLevels.Length; i++)
    {
      remainingLevels.Add(i);
    }
  }

  public override bool IsNextLevelAvailable()
  {
    return remainingLevels.Count > 0;
  }
}
