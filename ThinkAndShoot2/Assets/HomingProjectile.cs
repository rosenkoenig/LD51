using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform target;
    public float rotationRate = 30f;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            enabled = false;
            return;
        }

        Vector3 targetPos = target.position;
        targetPos.y = 1f;

        Vector3 dir = (targetPos - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, dir, rotationRate * Time.deltaTime);
    }
}
