using UnityEngine;
using System.Collections.Generic;

public class Damageable : MonoBehaviour
{
    [Tooltip("Multiplier to apply to the received damage")]
    public float DamageMultiplier = 1f;

    [Range(0, 1)]
    [Tooltip("Multiplier to apply to self damage")]
    public float SensibilityToSelfdamage = 0.5f;

    public Health Health { get; private set; }

    public List<Shielder> m_shielders = new List<Shielder>();
    public List<LineRenderer> m_shieldersLines = new List<LineRenderer>();

    public GameObject m_shieldSign;
    GameObject m_curSign;
    public GameObject m_shielderLinePrefab;

    void Awake()
    {
        // find the health component either at the same level, or higher in the hierarchy
        Health = GetComponent<Health>();
        if (!Health)
        {
            Health = GetComponentInParent<Health>();
        }

        m_shielders = new List<Shielder>();
    }

    void UpdateShield ()
    {
        for (int i = m_shielders.Count - 1; i >= 0; i--)
        {
            if(m_shielders[i] == null)
            {
                m_shielders.RemoveAt(i);
            }
        }

        int diffIdx = m_shielders.Count - m_shieldersLines.Count;

        if(diffIdx > 0)
        {
            //add lines
            for (int i = 0; i < diffIdx; i++)
            {
                GameObject inst = Instantiate(m_shielderLinePrefab);
                LineRenderer lineRenderer = inst.GetComponent<LineRenderer>();
                m_shieldersLines.Add(lineRenderer);
            }
        }
        else if (diffIdx < 0)
        {
            //remove lines
            for (int i = 0; i < -diffIdx; i++)
            {
                LineRenderer lineRenderer = m_shieldersLines[i];
                m_shieldersLines.Remove(lineRenderer);
                Destroy(lineRenderer);
            }
        }

        for (int i = 0; i < m_shieldersLines.Count; i++)
        {
            //set targetPos
            LineRenderer line = m_shieldersLines[i];
            Vector3 shielderPos = m_shielders[i].transform.position;
            Vector3 offset = new Vector3(0f, 0.6f, 0f);
            line.SetPosition(0, shielderPos + offset); ;
            line.SetPosition(1, transform.position + offset);
        }

        if(!CanTakeDamage())
        {
            if (m_curSign != null)
                return;

            m_curSign = Instantiate(m_shieldSign, transform);
            m_curSign.transform.localPosition = Vector3.zero;
        }
        else
        {
            if (m_curSign == null)
                return;

            Destroy(m_curSign);
        }
    }

    void OnDestroy ()
    {
        foreach(LineRenderer line in m_shieldersLines)
        {
            if(line)
            {
                Destroy(line.gameObject);
            }
        }
    }

    private void Update()
    {
        UpdateShield();
    }

    public bool CanTakeDamage ()
    {
        return m_shielders.Count == 0;
    }


    public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
    {
        if(!CanTakeDamage())
        {
            return;
        }

        if (Health)
        {
            var totalDamage = damage;

            // skip the crit multiplier if it's from an explosion
            if (!isExplosionDamage)
            {
                totalDamage *= DamageMultiplier;
            }

            // potentially reduce damages if inflicted by self
            if (Health.gameObject == damageSource)
            {
                totalDamage *= SensibilityToSelfdamage;
            }

            // apply the damages
            Health.TakeDamage(totalDamage, damageSource);
        }
    }
}
