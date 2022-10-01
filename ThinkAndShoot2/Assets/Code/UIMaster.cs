using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : AbstractUIMaster
{
    public GameObject gameOverMenu;

    public void DisplayGameOver ()
    {
        gameOverMenu.SetActive(true);
    }
}
