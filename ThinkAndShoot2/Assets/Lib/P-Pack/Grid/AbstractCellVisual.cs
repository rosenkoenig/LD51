using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCellVisual : AbsstractButtonMouseInteractible
{
  public Cell cell;
  public GridVisual gridVisual;

  public virtual void OnStateChange()
  {

  }

  public virtual void DisplayHoverEffect(CellState cellState)
  {
    
  }

  public virtual void HideHoverEffect()
  {

  }

  protected override void OnClick()
  {
    base.OnClick();
    gridVisual.OnCellVisualIsClicked(this);
  }
}
