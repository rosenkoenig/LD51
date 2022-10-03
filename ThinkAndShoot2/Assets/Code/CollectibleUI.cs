using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleUI : MonoBehaviour
{
    public SimpleCollectibleScript m_collectible;

    public TMPro.TextMeshProUGUI textType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_collectible && GameMaster.Instance.gameLevel.playerModeHandler.currentMode == PlayerMode._TOP)
        {
            Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(m_collectible.transform.position);

            Canvas copyOfMainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            float scaleFactor = copyOfMainCanvas.scaleFactor;
            Vector2 finalPosition = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);

            GetComponent<RectTransform>().anchoredPosition3D = finalPosition;

            textType.text = m_collectible.CollectibleType.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
