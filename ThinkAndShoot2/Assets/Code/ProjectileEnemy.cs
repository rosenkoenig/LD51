using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    public Projectile projectile;
    public float shootFrequency = 3f;
    public float warningTime = 1f;
    float timer = 0f;
    public Transform shootHotspot;
    public GameObject shootVFX;
    public LineRenderer warningVFX;

    public bool isInWarningTime = false;
    public bool isUpdatingPlayerPos = false;
    bool wasUpdatingPlayerPos = false;

    Vector3 playerPosition;

    protected override void UpdateAlive()
    {
        base.UpdateAlive();

        UpdateShoot();
    }

    void UpdateShoot()
    {
        timer += Time.deltaTime;

        isInWarningTime = timer >= shootFrequency - warningTime;
        isUpdatingPlayerPos = isInWarningTime && timer <= shootFrequency - 0.3f;

        if(isUpdatingPlayerPos)
        {
            playerPosition = GameMaster.Instance.curAbstractCharacter.transform.position;
            playerPosition += Vector3.up * 0.5f;
        }

        if(wasUpdatingPlayerPos != isUpdatingPlayerPos)
        {
            wasUpdatingPlayerPos = isUpdatingPlayerPos;

            if(isUpdatingPlayerPos)
            {

                GameObject shootVFx = Instantiate(shootVFX, shootHotspot.position, shootHotspot.rotation) as GameObject;
            }
        }

        UpdateWarning();

        if (timer >= shootFrequency)
        {
            Shoot();
            timer = 0f;
        } 
    }

    void UpdateWarning ()
    {
        if(warningVFX)
        {
            warningVFX.gameObject.SetActive(isInWarningTime);

            if (isUpdatingPlayerPos)
            {
                Vector3 offset = new Vector3(0f, 0.9f, 0f);
                warningVFX.SetPosition(0, transform.position + offset);
                warningVFX.SetPosition(1, GameMaster.Instance.curAbstractCharacter.transform.position + new Vector3(0f, 1f, 0f));
            }
        }
    }

    void Shoot ()
    {
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
