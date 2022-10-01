using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateMaterialFloatParameter : BasixAnimate<float>
{

  public string parameterName = "";

  Material material;

  public override void ApplyAnimate(float factor)
  {
    if (material == null && GetComponent<Renderer>()) material = GetComponent<Renderer>().material;
    if (material == null && GetComponent<Graphic>()) material = GetComponent<Graphic>().material;

    material.SetFloat(parameterName, Mathf.Lerp(startState, endState, factor));
  }
}
