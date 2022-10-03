using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaEnemy : Enemy
{
    public DamageArea damageArea;
    public float shootFrequency = 3f;
    float timer = 0f;
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

    IEnumerator Shoot()
    {
        Vector3 playerPosition = GameMaster.Instance.curAbstractCharacter.transform.position;
        playerPosition.y = 0f;
        GameObject shootVFx = Instantiate(shootVFX, playerPosition, Quaternion.identity) as GameObject;

        float delay = 1.25f;
        while(delay > 0f)
        {
            if(GameMaster.Instance.gameLevel.playerModeHandler.currentMode == PlayerMode._FPS)
            {
                delay -= Time.deltaTime;

                playerPosition = GameMaster.Instance.curAbstractCharacter.transform.position;
                playerPosition.y = 0f;
            }
            yield return null;
        }

        GameObject projInst = Instantiate(damageArea.gameObject, playerPosition, Quaternion.identity);
        projInst.GetComponent<DamageArea>().owner = gameObject;
    }
}
