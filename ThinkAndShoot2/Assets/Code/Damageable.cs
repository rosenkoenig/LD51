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

    public GameObject m_shieldSign;
    GameObject m_curSign;

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
        for (int i = 0; i < m_shielders.Count; i++)
        {
            if(m_shielders[i] == null)
            {
                m_shielders.RemoveAt(i);
            }
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
