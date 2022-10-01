using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellState
{
  EMPTY, INVISIBLE_FILL, FILLED, UNDEFINED
}

[System.Serializable]
public class Cell
{

  public Point coordinates;

  public bool IsEmpty { get { return state == CellState.EMPTY; } }
  public bool IsFilled { get { return state == CellState.FILLED; } }
  public System.Action OnStateChange = null;
  public CellState state = CellState.EMPTY;
  public System.Action<bool> OnShapeHover;
  public AbstractCellVisual visual;

  Node node = null;

  public AbstractGridContent content = null;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void FillWithGridContent(AbstractGridContent newContent)
  {
    content = newContent;
  }

  public void ChangeState(CellState newState)
  {
    state = newState;

    if (OnStateChange != null) OnStateChange();
  }

  public Node GetNode ()
  {
    if(node == null)
    {
      node = new Node(1f, coordinates.x, coordinates.y);
    }

    return node;
  }
}
