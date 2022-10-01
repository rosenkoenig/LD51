using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("BasixAnimates/AnimateScale", 10)]
public class AnimateScale : BasixAnimate<Vector3>
{

  public override void ApplyAnimate(float factor)
  {
    transform.localScale = Vector3.Lerp(startState, endState, factor);

    if (factor == 1f) applyBounce();
  }

  void applyBounce()
  {
    if (bounce == false && style != BasixAnimate<Vector3>.BasixAnimateStyle.Once) return;
    StartCoroutine(applyBounceCoroutine());
  }

  [SerializeField]
  [ConditionalHide("style", wantedValue = "Once", hideInInspector = true)]
  bool bounce = false;

  [ConditionalHide("bounce", hideInInspector = true)]
  public float maxAmplitude;

  [ConditionalHide("bounce", hideInInspector = true)]
  public float bounceDuration;

  [ConditionalHide("bounce", hideInInspector = true)]
  public float bounceQuantity;

  IEnumerator applyBounceCoroutine()
  {
    float factor, sinFactor, sin, bounceFactor;

    float bounceTime = bounceDuration;
    while (bounceTime >= 0f)
    {
      factor = Mathf.InverseLerp(0f, bounceDuration, bounceTime);

      sinFactor = Mathf.Lerp(0f, Mathf.PI * 2f, 1f - factor);

      sin = Mathf.Sin(sinFactor * bounceQuantity);

      bounceFactor = sin * Mathf.Lerp(0f, maxAmplitude, factor);

      transform.localScale = Vector3.one * (1f + bounceFactor);

      bounceTime -= Time.deltaTime;
      yield return null;
    }
  }

  [ContextMenu("Use Current As Start")]
  void UseCurrentAsStart()
  {
    startState = transform.localScale;
  }

  [ContextMenu("Use Current As Target")]
  void UseCurrentAsTarget()
  {
    endState = transform.localScale;
  }

}
