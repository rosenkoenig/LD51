using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 10.0f;

    public float damage = 1f;

    Collider hitCollider;
    Vector3 lastHitNormal;

    public GameObject hitShitVfx;
    public GameObject hitEnemyVfx;

    public GameObject owner;

    public LayerMask physicsMasks;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameMaster.Instance.gameState != GameState.STARTED)
        {
            Destroy(gameObject);
            return;
        }

        if (GameMaster.Instance.gameLevel.playerModeHandler.currentMode != PlayerMode._FPS)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        rb.velocity = transform.forward * speed;

        if(hitCollider != null)
        {
            OnCollide(hitCollider, lastHitNormal);
        }

        Ray ray = new Ray(transform.position, rb.velocity.normalized);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, speed * Time.deltaTime, physicsMasks))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if ((enemy == null || enemy.gameObject != owner) && hit.collider.gameObject != owner)
            {
                string debug = "";
                if(enemy != null)
                {
                    debug = ""+ enemy.gameObject;
                }
                else
                {
                    debug = "" + hit.collider.gameObject;
                }
                Debug.Log(debug + " hit by " + owner);
                hitCollider = hit.collider;
                lastHitNormal = hit.normal;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if ((enemy == null || enemy.gameObject != owner) && other.gameObject != owner)
        {
            string debug = "";
            if (enemy != null)
            {
                debug = "" + enemy.gameObject;
            }
            else
            {
                debug = "" + other.gameObject;
            }
            Debug.Log(debug + " hit by " + owner);
            OnCollide(other, -transform.forward);
        }
    }


    void OnCollide (Collider other, Vector3 normal)
    {
        Damageable dmg = other.gameObject.GetComponentInParent<Damageable>();
        if (dmg && dmg.CanTakeDamage())
        {
            dmg.InflictDamage(damage, false, owner);
            Instantiate(hitEnemyVfx, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(hitShitVfx, transform.position, Quaternion.Euler(normal));
        }

        Destroy(gameObject);
    }
}
