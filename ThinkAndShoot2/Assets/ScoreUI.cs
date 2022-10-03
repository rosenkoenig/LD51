using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Instance.gameState == GameState.STARTED)
        {
            scoreText.text = GameMaster.Instance.ScoreManager.GetScore();
        }
    }
}
