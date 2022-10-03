using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentScore = 0;
    int bestScore = 0;

    [HideInInspector]
    public bool lastScoreWasBest = false;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("score");
        GameMaster.Instance.onGameStarts += OnGameStarts;
        GameMaster.Instance.onGameEnds += OnGameEnds;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Instance.gameState == GameState.STARTED)
        {
            timer += Time.deltaTime;

            if(timer>= 1f)
            {
                timer -= 1f;
                AddToScore(1);
            }
        }
    }

    void OnGameStarts ()
    {
        lastScoreWasBest = false;
        currentScore = 0;
    }

    public void AddToScore (int score)
    {
        currentScore += score;
    }

    public string GetScore()
    {
        return currentScore.ToString();
    }

    public string GetBestScore ()
    {
        return bestScore.ToString();
    }

    public void OnGameEnds ()
    {
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            lastScoreWasBest = true;
            PlayerPrefs.SetInt("score", bestScore);
            PlayerPrefs.Save();
        }
    }
}
