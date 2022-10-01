using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXGameObject : MonoBehaviour
{
    public float duration = -1;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.Instance.gameState == GameState.STARTED)
        {
            timer += Time.deltaTime;
            if (duration > 0f && timer >= duration)
            {
                Destroy(gameObject);
            }
        }
        else if (GameMaster.Instance.gameState == GameState.ENDED)
        {
            Destroy(gameObject);
        }
    }
}
