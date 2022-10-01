using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
