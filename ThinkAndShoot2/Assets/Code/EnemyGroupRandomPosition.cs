using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupRandomPosition : EnemyGroup
{
    public Vector2 m_xPosRange;
    public Vector2 m_zPosRange;

    public Vector2 m_rotRange;

    public override void OnSpawned()
    {
        base.OnSpawned();

        if(m_xPosRange.magnitude != 0f || m_zPosRange.magnitude != 0f)
        {
            float x = Mathf.Lerp(m_xPosRange.x, m_xPosRange.y, Random.Range(0f, 1f));
            float z = Mathf.Lerp(m_zPosRange.x, m_zPosRange.y, Random.Range(0f, 1f));
            transform.position = new Vector3(x, 0f, z);
        }

        if(m_rotRange.magnitude != 0f)
        {
            float yRot = Mathf.Lerp(m_rotRange.x, m_rotRange.y, Random.Range(0f, 1f));
            transform.rotation = Quaternion.Euler(0f, yRot, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //left
        Debug.DrawLine(new Vector3(m_xPosRange.x, 0.1f, m_zPosRange.x), new Vector3(m_xPosRange.x, 0.1f, m_zPosRange.y));
        //top
        Debug.DrawLine(new Vector3(m_xPosRange.x, 0.1f, m_zPosRange.y), new Vector3(m_xPosRange.y, 0.1f, m_zPosRange.y));
        //right
        Debug.DrawLine(new Vector3(m_xPosRange.y, 0.1f, m_zPosRange.x), new Vector3(m_xPosRange.y, 0.1f, m_zPosRange.y));
        //bottom
        Debug.DrawLine(new Vector3(m_xPosRange.x, 0.1f, m_zPosRange.x), new Vector3(m_xPosRange.y, 0.1f, m_zPosRange.x));
    }

    [ContextMenu("Set max values")]
    void setMaxValues ()
    {
        m_xPosRange.x = -80f;
        m_xPosRange.y = 80f;
        m_zPosRange.x = -40f;
        m_zPosRange.y = 40f;
    }

    [ContextMenu("Recenter")]
    void recenter ()
    {
        Vector3 avgPos = Vector3.zero;

        for (int i = 0; i < transform.childCount; i++)
        {
            avgPos += transform.GetChild(i).position;
        }

        avgPos /= transform.childCount;



        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = transform.GetChild(i).position - avgPos;
        }
    }
}
