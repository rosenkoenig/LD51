using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShootVFX : MonoBehaviour
{
    LineRenderer line;

    private void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 offset = new Vector3(0f, 0.9f, 0f);
        line.SetPosition(0, transform.position + offset);
        line.SetPosition(1, GameMaster.Instance.curAbstractCharacter.transform.position + offset);
    }
}
