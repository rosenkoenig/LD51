using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    public float damage = 1f;

    public GameObject hitEnemyVfx;

    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.Instance.gameState != GameState.STARTED)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("contact with "+ other.gameObject.name);
        if (other.tag == "Player")
        {
            Debug.Log(other.gameObject + " hit by " + owner);
            OnCollide(other, Vector3.up);
        }
    }

    void OnCollide(Collider other, Vector3 normal)
    {
        Damageable dmg = other.gameObject.GetComponentInParent<Damageable>();
        if (dmg && dmg.CanTakeDamage())
        {
            dmg.InflictDamage(damage, false, owner);
            Instantiate(hitEnemyVfx, other.transform.position, Quaternion.Euler(normal));
        }

        Destroy(gameObject);
    }
}
