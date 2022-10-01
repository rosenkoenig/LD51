using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueLevelSequencer : AbstractLevelSequencer
{
  public override int GetFirstLevelIndex()
  {
    return 0;
  }

  public override int GetNextLevelIndex()
  {
    return 0;
  }

  public override bool IsNextLevelAvailable()
  {
    return false;
  }
}
