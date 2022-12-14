using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoverer : AbsstractButtonMouseInteractible
{
    UIMaster ui;
    public GameObject enemy;

    private void Start()
    {
        ui = GameMaster.Instance.uiMaster as UIMaster;
    }

    protected override void OnMouseIn()
    {
        base.OnMouseIn();

        if(!enemy)
        {
            Debug.LogError("Enemy is null", this);
            return;
        }

        ui.DisplayEnemyUI(enemy.GetComponent<Enemy>());
    }

    protected override void OnMouseOut()
    {
        base.OnMouseOut();

        ui.HideEnemyUI();
    }
}
