using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject up, down, left, right;

    public float maxOffset = 10f;

    GameObject visual;

    // Start is called before the first frame update
    void Start()
    {
        visual = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Instance.gameState == GameState.STARTED)
        {
            UpdateCrossHairItems();
            visual.SetActive(GameMaster.Instance.gameLevel.playerModeHandler.currentMode == PlayerMode._FPS);
        }
        else
        {
            visual.SetActive(false);
        }
    }

    void UpdateCrossHairItems ()
    {
        GamePlayer gp = GameMaster.Instance.curAbstractCharacter as GamePlayer;
        if (!gp) return;

        float ratio = gp.weapon.recoilFactor;

        float offset = Mathf.Lerp(0f, maxOffset, ratio);

        Vector2 leftOffset = new Vector2(-offset, 0f);
        Vector2 rightOffset = new Vector2(offset, 0f);
        Vector2 upOffset = new Vector2(0f, offset);
        Vector2 downOffset = new Vector2(0f, -offset);

        right.transform.localPosition = rightOffset;
        left.transform.localPosition = leftOffset;
        up.transform.localPosition = upOffset;
        down.transform.localPosition = downOffset;
    }
}
