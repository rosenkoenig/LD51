using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : MonoBehaviour
{
    Enemy m_owner;

    public float m_maxRange;
    public int m_maxEnemies;

    public List<Enemy> m_protectedAllies;

    // Start is called before the first frame update
    void Start()
    {
        m_owner = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllies();
        ProtectAllies();
    }

    void UpdateAllies()
    {
        List<Enemy> allies = new List<Enemy>(GameMaster.Instance.gameLevel.levelEnemies);

        allies.Remove(m_owner);
        allies.RemoveAll(x => x.m_enemyClass == EnemyClass.Shielder);

        List<Enemy> sortedAllies = new List<Enemy>();

        while (allies.Count > 0)
        {
            Enemy closest = null;
            float minDist = float.MaxValue;
            foreach (Enemy ally in allies)
            {
                float dist = Vector3.Distance(transform.position, ally.transform.position);

                if (dist <= minDist)
                {
                    closest = ally;
                    minDist = dist;
                }
            }

            if (closest != null)
            {
                sortedAllies.Add(closest);
                allies.Remove(closest);
            }
        }

        m_protectedAllies = new List<Enemy>();

        foreach (Enemy sortedAlly in sortedAllies)
        {
            float dist = Vector3.Distance(transform.position, sortedAlly.transform.position);
            if (dist <= m_maxRange)
            {
                m_protectedAllies.Add(sortedAlly);
                if (m_protectedAllies.Count >= m_maxEnemies)
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void ProtectAllies()
    {
        List<Enemy> allies = new List<Enemy>(GameMaster.Instance.gameLevel.levelEnemies);

        foreach (Enemy ally in allies)
        {
            Damageable dmg = ally.GetComponent<Damageable>();
            if (m_protectedAllies.Contains(ally))
            {
                if (dmg.m_shielders.Contains(this) == false)
                    dmg.m_shielders.Add(this);
            }
            else
            {
                if (dmg.m_shielders.Contains(this))
                {
                    dmg.m_shielders.Remove(this);
                }
            }
        }
    }
}
