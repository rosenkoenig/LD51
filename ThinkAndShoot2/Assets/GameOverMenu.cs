using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentScore, bestScore;

    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = GameMaster.Instance.ScoreManager.GetScore();
        bestScore.text = GameMaster.Instance.ScoreManager.GetBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartButtonpressed ()
    {
        gameObject.SetActive(false);
        GameMaster.Instance.RestartGame();
    }

    public void OnQuitButtonPressed ()
    {
        Application.Quit();
    }
}
