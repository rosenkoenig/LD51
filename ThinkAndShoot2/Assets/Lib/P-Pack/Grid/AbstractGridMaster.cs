using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGridMaster : MonoBehaviour
{

  [SerializeField]
  protected Vector2 _gridSize;
  public Vector2 GridSize { get { return _gridSize; } }

  protected Cell[,] _cells;

  protected GridVisual visual;
  public GridVisual GetGridVisual { get { return visual; } }

  [SerializeField]
  protected AbstractCellVisual _defaultCellVisual;
  public AbstractCellVisual DefaultCellVisual { get { return _defaultCellVisual; } }

  [SerializeField]
  protected float _gridStep = 5f;
  public float GridStep { get { return _gridStep; } }

  [SerializeField]
  protected Transform _gridVisualParent;
  public Transform GridVisualParent { get { return _gridVisualParent; } }

  public bool useZAsTop = false;

  public virtual void Init()
  {
    CreateGrid();
  }

  void CreateGrid()
  {
    _cells = new Cell[(int)_gridSize.x, (int)_gridSize.y];
    for (int x = 0; x < _gridSize.x; x++)
    {
      for (int y = 0; y < _gridSize.y; y++)
      {
        CreateCell(x, y);
      }
    }

    visual = new GridVisual();
    visual.InitVisual(this, useZAsTop);

  }

  protected virtual void CreateCell (int x, int y)
  {
    Cell newCell = new Cell();
    newCell.coordinates = new Point(x, y);
    _cells[x, y] = newCell;
  }

  public Cell GetCellAtCoordinate(int x, int y)
  {
    if (x < 0 || x >= GridSize.x)
      return null;
    if (y < 0 || y >= GridSize.y)
      return null;

    return _cells[x, y];
  }
  public Cell GetCellAtCoordinate(Vector2 coordinate)
  {
    return GetCellAtCoordinate((int)coordinate.x, (int)coordinate.y);
  }

  public bool CellIsAvailable(Cell cell, GridShape shape)
  {
    bool available = cell.IsEmpty;
    foreach (Cell shapeHoveredCell in GetCellsHoveredByShape(cell, shape, true))
    {
      if (shapeHoveredCell == null || shapeHoveredCell.IsEmpty == false) available = false;
    }

    List<Cell> cellsHoveredByInvisible = GetCellsHoveredByInvisibleInShape(cell, shape);
    if (cellsHoveredByInvisible != null)
    {
      foreach (Cell cellHoveredByInvisibles in cellsHoveredByInvisible)
      {
        if (cellHoveredByInvisibles != null)
        {
          bool isEmptyOrInvisible = cellHoveredByInvisibles.IsEmpty || cellHoveredByInvisibles.state == CellState.INVISIBLE_FILL;

          if (cellHoveredByInvisibles == null || !isEmptyOrInvisible) available = false;
        }
      }
    }


    return available;
  }

  public List<Cell> GetCellsHoveredByShape(Cell cell, GridShape gridShape, bool onlyVisibleOffsets)
  {
    List<Cell> cellsHovered = new List<Cell>();

    if (gridShape != null && gridShape.GetOffsets() != null)
    {
      foreach (Vector2 offset in gridShape.GetOffsets())
      {
        Vector2 calculatedCoordinate = cell.coordinates.asVector2 + offset;
        Cell curCell = GetCellAtCoordinate(calculatedCoordinate);

        if (curCell != null)
          cellsHovered.Add(curCell);
      }

      if (!onlyVisibleOffsets)
      {
        List<Cell> invisibleCellsHovered = GetCellsHoveredByInvisibleInShape(cell, gridShape);

        if (invisibleCellsHovered != null) cellsHovered.AddRange(invisibleCellsHovered);
      }
    }

    return cellsHovered;
  }

  public List<Cell> GetCellsHoveredByInvisibleInShape(Cell cell, GridShape gridShape)
  {
    List<Cell> cellsHovered = null;

    if (gridShape != null && gridShape.GetInvisibleOffsets() != null)
    {
      foreach (Vector2 offset in gridShape.GetInvisibleOffsets())
      {
        Vector2 calculatedCoordinate = cell.coordinates.asVector2 + offset;
        Cell curCell = GetCellAtCoordinate(calculatedCoordinate);

        if (cellsHovered == null) cellsHovered = new List<Cell>();
        cellsHovered.Add(curCell);
      }
    }


    return cellsHovered;
  }

  public List<Cell> GetAdjacentCells(Cell cell)
  {
    List<Cell> adjacentCells = new List<Cell>();

    Cell topCell = GetCellAtCoordinate((int)cell.coordinates.x, (int)cell.coordinates.y - 1);
    if (topCell != null) adjacentCells.Add(topCell);

    Cell botCell = GetCellAtCoordinate((int)cell.coordinates.x, (int)cell.coordinates.y + 1);
    if (botCell != null) adjacentCells.Add(botCell);

    Cell rightCell = GetCellAtCoordinate((int)cell.coordinates.x + 1, (int)cell.coordinates.y);
    if (rightCell != null) adjacentCells.Add(rightCell);

    Cell leftCell = GetCellAtCoordinate((int)cell.coordinates.x - 1, (int)cell.coordinates.y);
    if (leftCell != null) adjacentCells.Add(leftCell);

    return adjacentCells;
  }

  public List<Cell> GetAdjacentCellsWithStateFilter (Cell cell, CellState cellState)
  {
    List<Cell> adjacentCells = GetAdjacentCells(cell);
    List<Cell> matchingAdjacentcells = new List<Cell>();

    foreach (Cell adjCell in adjacentCells)
    {
      if (adjCell.state == cellState) matchingAdjacentcells.Add(adjCell);
    }

    return matchingAdjacentcells;
  }

  public List<Cell> GetAdjacentCellsWithContentFilter(Cell cell, string contentId)
  {
    List<Cell> adjacentCells = GetAdjacentCells(cell);
    List<Cell> matchingAdjacentcells = new List<Cell>();

    foreach (Cell adjCell in adjacentCells)
    {
      if (adjCell.content != null && adjCell.content.contentId == contentId) matchingAdjacentcells.Add(adjCell);
    }

    return matchingAdjacentcells;
  }

  public List<Cell> GetAdjacentCellsToShape(Cell pivotCell, GridShape shape, bool onlyVisibleOffsets)
  {
    List<Cell> adjacentCellsToShape = new List<Cell>();

    adjacentCellsToShape.AddRange(GetAdjacentCells(pivotCell));

    if (shape != null)
    {
      foreach (Cell inShapeCell in GetCellsHoveredByShape(pivotCell, shape, onlyVisibleOffsets))
      {
        adjacentCellsToShape.AddRange(GetAdjacentCells(inShapeCell));
      }
    }

    adjacentCellsToShape = CleanListOfAdjacentCellsToShape(adjacentCellsToShape, pivotCell, shape);

    return adjacentCellsToShape;
  }

  List<Cell> CleanListOfAdjacentCellsToShape(List<Cell> list, Cell pivotCell, GridShape shape)
  {
    List<Cell> cleanList = new List<Cell>(list);

    cleanList.Remove(pivotCell);

    foreach (Cell cell in list)
    {
      foreach (Cell cellHoveredByShape in GetCellsHoveredByShape(pivotCell, shape, true))
      {
        if (cell.coordinates == cellHoveredByShape.coordinates) cleanList.Remove(cell);
      }
    }

    return cleanList;
  }

  public bool OneAdjacentCellIsNotEmpty(Cell cell, GridShape shape)
  {
    bool oneIsNotEmpty = false;

    foreach (Cell adjCell in GetAdjacentCellsToShape(cell, shape, true))
    {
      if (adjCell.IsFilled == true) oneIsNotEmpty = true;
    }

    return oneIsNotEmpty;
  }
  
  List<AbstractGridContent> GetAdjGridContents(List<AbstractGridContent> gridContents)
  {
    List<AbstractGridContent> result = new List<AbstractGridContent>();
    foreach (AbstractGridContent gc in gridContents)
    {
      Cell cell = GetCellAtCoordinate(gc.pivotCellCoordinate);

      List<Cell> adjacentCells = GetAdjacentCellsToShape(cell, gc.shape, true);

      foreach (Cell adjCell in adjacentCells)
      {
        if (adjCell != null && gridContents.Contains(adjCell.content) == false)
        {
          if (adjCell.content != null && gridContents.Contains(adjCell.content) == false) result.Add(adjCell.content);
        }
      }
    }

    return result;
  }

  public void FillGridWithContent(AbstractGridContent part)
  {
    Cell pivotCell = GetCellAtCoordinate(part.pivotCellCoordinate);
    pivotCell.ChangeState(CellState.FILLED);
    pivotCell.FillWithGridContent(part);

    List<Cell> cellsHoveredByInvisibleShape = GetCellsHoveredByInvisibleInShape(pivotCell, part.shape);
    if (cellsHoveredByInvisibleShape != null && cellsHoveredByInvisibleShape.Count > 0)
    {
      foreach (Cell cell in cellsHoveredByInvisibleShape)
      {
        if (cell != null)
        {
          cell.ChangeState(CellState.INVISIBLE_FILL);
          cell.FillWithGridContent(part);
        }
      }
    }

    foreach (Cell cell in GetCellsHoveredByShape(pivotCell, part.shape, true))
    {
      cell.ChangeState(CellState.FILLED);
      cell.FillWithGridContent(part);
    }
  }

  public void DisplayGrid(bool state)
  {
    visual.DisplayGrid(state);
  }

  public void ToggleGrid()
  {
    visual.ToggleDisplayGrid();
  }

  public Vector3 GetCellPositionAtCoordinate(Vector2 coordinates)
  {
    return visual.GetCellPositionAtCoordinate(coordinates);
  }

  public AbstractCellVisual GetCloserCellVisualToPoint(Vector3 from)
  {
    return visual.GetCloserCell(from);
  }

  public virtual void OnCellIsClicked(Cell cell)
  {

  }

}
