using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual
{
  public AbstractGridMaster gridMaster;
  AbstractCellVisual[,] cellVisuals;

  bool gridIsDisplayed = false;

  public void InitVisual(AbstractGridMaster master, bool useZAsTop)
  {
    gridMaster = master;

    cellVisuals = new AbstractCellVisual[(int)gridMaster.GridSize.x, (int)gridMaster.GridSize.y];

    DrawGrid((int)gridMaster.GridSize.x, (int)gridMaster.GridSize.y, useZAsTop);

    gridMaster.GridVisualParent.gameObject.SetActive(false);
  }

  void DrawGrid(int sizeX, int sizeY, bool useZAsTop)
  {
    Vector2 offset = new Vector2(((float)(sizeX - 1) * gridMaster.GridStep) / 2f, ((float)(sizeY - 1) * gridMaster.GridStep) / 2f);

    for (int x = 0; x < sizeX; x++)
    {
      for (int y = 0; y < sizeY; y++)
      {
        GameObject newInst = GameObject.Instantiate(gridMaster.DefaultCellVisual.gameObject);
        AbstractCellVisual visual = newInst.GetComponent<AbstractCellVisual>();
        visual.cell = gridMaster.GetCellAtCoordinate(x, y);
        visual.gridVisual = this;
        visual.cell.OnStateChange += visual.OnStateChange;
        visual.OnStateChange();
        visual.cell.visual = visual;

        cellVisuals[x, y] = visual;

        newInst.transform.parent = gridMaster.GridVisualParent;
        newInst.transform.localPosition = new Vector3(gridMaster.GridStep * (float)x - offset.x, useZAsTop ? 0f : gridMaster.GridStep * (float)y - offset.y, useZAsTop ? gridMaster.GridStep * (float)y - offset.y  : 0f);
      }
    }
  }

  public void ClearVisualHoverEffect()
  {
    foreach (AbstractCellVisual cv in cellVisuals)
    {
      cv.HideHoverEffect();
    }
  }

  public virtual void DisplayGrid(bool state)
  {
    gridIsDisplayed = state;
    gridMaster.GridVisualParent.gameObject.SetActive(state);
  }

  public void ToggleDisplayGrid()
  {
    DisplayGrid(!gridIsDisplayed);
  }

  public Vector3 GetCellPositionAtCoordinate(Vector2 coordinates)
  {
    return gridMaster.GetCellAtCoordinate(coordinates).visual.transform.position;
  }

  public AbstractCellVisual GetCloserCell(Vector3 from)
  {
    AbstractCellVisual closer = null;
    float minDist = float.MaxValue;
    foreach (AbstractCellVisual cv in cellVisuals)
    {
      float curDist = Vector3.Distance(cv.transform.position, from);
      if (curDist < minDist)
      {
        minDist = curDist;
        closer = cv;
      }
    }

    return closer;
  }

  public void OnCellVisualIsClicked(AbstractCellVisual cellVisual)
  {
    gridMaster.OnCellIsClicked(cellVisual.cell);
  }
}
