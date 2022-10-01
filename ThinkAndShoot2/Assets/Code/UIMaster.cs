using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : AbstractUIMaster
{
    public GameObject gameOverMenu;

    public HealthBar healthBar;

    public EnemyUI enemyUI;

    public void DisplayGameOver ()
    {
        gameOverMenu.SetActive(true);
    }

    public void DisplayEnemyUI(Enemy enemy)
    {
        enemyUI.m_myEnemy = enemy;
        enemyUI.gameObject.SetActive(true);
    }

    public void HideEnemyUI()
    {
        enemyUI.gameObject.SetActive(false);
    }
}
