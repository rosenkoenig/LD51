using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
  [SerializeField]
  Image gaugeForeground = default;

  [Range(0f,1f)]
  public float gaugeDampen = 1f;

  [ReadOnly][SerializeField]
  float value = 0f;

  public System.Action<float> onNewValue = null;

  public float Value { get => value; }

  public void setValue (float newValue)
  {
    if (newValue == value) return;

    value = newValue;
    onNewValue?.Invoke(value);
  }

  private void Update()
  {
    gaugeForeground.fillAmount = Mathf.Lerp(gaugeForeground.fillAmount, value, gaugeDampen);
  }
}
