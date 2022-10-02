using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyClass
{
    Beamer,
    Missiler,
    Shielder,
    Digger
}

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Health m_Health;
    public bool IsDead { get; private set; }

    public Animator m_animator;

    public GameObject m_deathVfx;
    public GameObject m_hitVfx;

    public EnemyClass m_enemyClass;

    protected virtual void Start()
    {
        m_Health = GetComponent<Health>();

        m_Health.OnDie += OnDie;
        m_Health.OnDamaged += OnDamaged;

        GameMaster.Instance.gameLevel.levelEnemies.Add(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(IsDead)
        {
            Destroy(gameObject);
        }
        else if (GameMaster.Instance.gameLevel.playerModeHandler.currentMode == PlayerMode._FPS)
        {
            UpdateAlive();
        }
    }

    protected virtual void UpdateAlive()
    {

    }

    protected virtual void OnDie ()
    {
        IsDead = true;
        if(m_deathVfx)
        {
            Instantiate(m_deathVfx, transform.position + Vector3.up, transform.rotation);
        }        
    }

    protected virtual void OnDamaged (float damageTaken, GameObject damageSource)
    {
        m_animator.Play("ENEMY_Hit");
        if (m_hitVfx)
        {
            Instantiate(m_hitVfx, transform.position + Vector3.up, transform.rotation);
        }
    }


}
