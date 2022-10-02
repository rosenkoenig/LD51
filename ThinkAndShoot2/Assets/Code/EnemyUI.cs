using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Enemy m_myEnemy;

    public TMPro.TextMeshProUGUI textHP;
    public TMPro.TextMeshProUGUI textType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_myEnemy && GameMaster.Instance.gameLevel.playerModeHandler.currentMode == PlayerMode._TOP)
        {
            Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(m_myEnemy.transform.position);

            Canvas copyOfMainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            float scaleFactor = copyOfMainCanvas.scaleFactor;
            Vector2 finalPosition = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);

            GetComponent<RectTransform>().anchoredPosition3D = finalPosition;

            textHP.text = m_myEnemy.m_Health.CurrentHealth.ToString("F0") + "HP";
            textType.text = m_myEnemy.m_enemyClass.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
