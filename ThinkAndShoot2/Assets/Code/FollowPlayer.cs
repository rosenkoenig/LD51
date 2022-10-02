using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smooth = 1f;

    public Vector3 axisFactor = Vector3.one;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameMaster.Instance.curAbstractCharacter.transform.position;
        playerPos.x *= axisFactor.x;
        playerPos.y *= axisFactor.y;
        playerPos.z *= axisFactor.z;
        transform.position = Vector3.Lerp(transform.position, playerPos, smooth);
    }
}
