using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHover : AbsstractButtonMouseInteractible
{
    UIMaster ui;
    public SimpleCollectibleScript collectible;

    private void Start()
    {
        ui = GameMaster.Instance.uiMaster as UIMaster;
    }

    protected override void OnMouseIn()
    {
        base.OnMouseIn();

        if(!collectible)
        {
            Debug.LogError("Collectible is null", this);
            return;
        }

        ui.DisplayCollectibleUI(collectible);
    }

    protected override void OnMouseOut()
    {
        base.OnMouseOut();

        ui.HideCollectibleUI();
    }
}
