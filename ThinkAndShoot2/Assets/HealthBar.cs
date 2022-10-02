using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image m_gauge;
    public Animator m_animator;

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
        m_animator.Play("HEALTHBAR_Hit");
        m_gauge.transform.localScale = new Vector3(ratio, 1f, 1f);
    }
}
