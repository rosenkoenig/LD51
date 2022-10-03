using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : AbstractUIMaster
{
    public GameObject gameOverMenu;

    public HealthBar healthBar;

    public EnemyUI enemyUI;
    public CollectibleUI collectibleUI;

    public StartMenu startMenu;

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

    public void DisplayCollectibleUI(SimpleCollectibleScript collectible)
    {
        collectibleUI.m_collectible = collectible;
        collectibleUI.gameObject.SetActive(true);
    }

    public void HideCollectibleUI()
    {
        collectibleUI.gameObject.SetActive(false);
    }

    protected override void OnGameLaunched()
    {
        base.OnGameLaunched();

        startMenu.gameObject.SetActive(true);
    }

    protected override void OnGameStart()
    {
        base.OnGameStart();
        startMenu.gameObject.SetActive(false);
    }
}
