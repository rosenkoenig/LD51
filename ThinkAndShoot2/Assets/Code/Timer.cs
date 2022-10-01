using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Instance.gameState == GameState.STARTED)
        {
            text.enabled = true;

            float timer = 10f - GameMaster.Instance.gameLevel.playerModeHandler.timer;

            if (timer < 1f)
            {
                timer *= 100f;
            }

            float textTimer = Mathf.Ceil(timer);
            string timerTxt = textTimer.ToString();

            if (textTimer < 10f)
            {
                timerTxt = "0" + timerTxt;
            }

            text.text = timerTxt;
        }
        else
        {
            text.enabled = false;
        }
    }
}
