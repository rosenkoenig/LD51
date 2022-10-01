using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGridContent : MonoBehaviour
{

  public GridShape shape;
  [HideInInspector]
  public Vector2 pivotCellCoordinate, lastPivotCellCoordinate;
  public int lastRotation = 0;
  public string contentId = "";
  public string localizedName = "";

  [HideInInspector]
  public List<Cell> occupiedCells = new List<Cell>();
  

  public void Rotate(bool clockWise)
  {
    shape.Rotate(clockWise);
    transform.Rotate(transform.forward, clockWise ? 90f : -90f);
  }

  public void SetRotation(int rotation)
  {
    for (int i = 0; i < rotation; i++)
    {
      Rotate(true);
    }
  }
}
