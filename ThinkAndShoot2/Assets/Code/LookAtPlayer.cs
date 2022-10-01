using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    AbstractCharacter character;

    public bool ignoreY = true;
    // Start is called before the first frame update
    void Start()
    {
        character = GameMaster.Instance.curAbstractCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        if (character)
        {
            Vector3 chPos = character.transform.position;
            if(ignoreY)
            {
                chPos.y = transform.position.y;
            }
            transform.forward = (chPos - transform.position).normalized;
        }
    }
}
