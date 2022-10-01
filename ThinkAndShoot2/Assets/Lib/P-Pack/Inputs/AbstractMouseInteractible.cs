using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMouseInteractible : MonoBehaviour
{
  protected PointAndClickInputs pointClickInputs = null;

  // Update is called once per frame
  void LateUpdate()
  {
    if (pointClickInputs == null) pointClickInputs = PointAndClickInputs.Instance;
    if (pointClickInputs != null)
      UpdateMouseInteractible();
  }

  protected abstract void UpdateMouseInteractible();
}
