using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridShape
{

  public Vector2[] offsets;
  [HideInInspector]
  public Vector2[] _internalOffsets;

  public Vector2[] invisibleOffsets;
  [HideInInspector]
  public Vector2[] _internalInvisibleOffsets;

  bool isInit = false;

  int _rotation = 0;

  public int Rotation
  {
    get { return _rotation; }
  }

  public void Rotate(bool clockWise)
  {
    if (!clockWise)
    {
      Vector2[] newOffsets = new Vector2[_internalOffsets.Length];
      for (int i = 0; i < newOffsets.Length; i++)
      {
        newOffsets[i].x = _internalOffsets[i].y;
        newOffsets[i].y = -_internalOffsets[i].x;
      }

      _internalOffsets = newOffsets;

      Vector2[] newInvisibleOffsets = new Vector2[_internalInvisibleOffsets.Length];
      for (int i = 0; i < newInvisibleOffsets.Length; i++)
      {
        newInvisibleOffsets[i].x = _internalInvisibleOffsets[i].y;
        newInvisibleOffsets[i].y = -_internalInvisibleOffsets[i].x;
      }

      _internalInvisibleOffsets = newInvisibleOffsets;

      _rotation--;
    }
    else
    {
      Vector2[] newOffsets = new Vector2[_internalOffsets.Length];
      for (int i = 0; i < newOffsets.Length; i++)
      {
        newOffsets[i].x = -_internalOffsets[i].y;
        newOffsets[i].y = _internalOffsets[i].x;
      }

      _internalOffsets = newOffsets;

      Vector2[] newInvisibleOffsets = new Vector2[_internalInvisibleOffsets.Length];
      for (int i = 0; i < newInvisibleOffsets.Length; i++)
      {
        newInvisibleOffsets[i].x = -_internalInvisibleOffsets[i].y;
        newInvisibleOffsets[i].y = _internalInvisibleOffsets[i].x;
      }

      _internalInvisibleOffsets = newInvisibleOffsets;

      _rotation++;
    }

    _rotation %= 4;

    if (_rotation < 0) _rotation += 4;

    Debug.Log(_rotation);
  }

  public void Init()
  {
    _internalOffsets = offsets;
    _internalInvisibleOffsets = invisibleOffsets;
    isInit = true;
  }

  public Vector2[] GetOffsets()
  {
    if (!isInit)
    {
      Init();
    }

    return _internalOffsets;
  }

  public Vector2[] GetInvisibleOffsets()
  {
    if (!isInit)
    {
      Init();
    }

    return _internalInvisibleOffsets;
  }
}
