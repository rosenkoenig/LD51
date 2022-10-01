using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    public Projectile projectile;
    public float shootFrequency = 3f;
    float timer = 0f;
    public Transform shootHotspot;
    public GameObject shootVFX;

    protected override void UpdateAlive()
    {
        base.UpdateAlive();

        UpdateShoot();
    }

    void UpdateShoot()
    {
        timer += Time.deltaTime;
        if (timer >= shootFrequency)
        {
            StartCoroutine(Shoot());
            timer = 0f;
        } 
    }

    IEnumerator Shoot ()
    {
        GameObject shootVFx = Instantiate(shootVFX, shootHotspot.position, shootHotspot.rotation) as GameObject;
        yield return new WaitForSeconds(0.3f);

        Vector3 playerPosition = GameMaster.Instance.curAbstractCharacter.transform.position;
        playerPosition += Vector3.up * 0.5f;
        Vector3 dir = playerPosition - shootHotspot.position;
        GameObject projInst = Instantiate(projectile.gameObject, shootHotspot.position, Quaternion.identity);
        projInst.transform.forward = dir;
        projInst.GetComponent<Projectile>().owner = gameObject;

        HomingProjectile homingProjectile = projInst.GetComponent<HomingProjectile>();
        if (homingProjectile)
        {
            homingProjectile.target = GameMaster.Instance.curAbstractCharacter.transform;
        }
    }
}
