using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image gauge;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth (float ratio)
    {
        gauge.transform.localScale = new Vector3(ratio, 1f, 1f);
    }
}
