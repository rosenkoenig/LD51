using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomRaycastHit
{
    public RaycastHit raycastHit3D;
    public RaycastHit2D raycastHit2D;
    public bool is2D = false;
    public string inputName = "";
    public string debug_colliderName = "";
}

public class PointAndClickInputs : MonoBehaviour
{
    public static PointAndClickInputs Instance = null;

    public Camera gameCamera = null;

    [SerializeField]
    bool _2D = true;
    [SerializeField]
    bool _3D = true;

    [SerializeField]
    bool mouse = false;

    [SerializeField]
    bool touch = false;

    [ConditionalHide("touch", hideInInspector = true)]
    [SerializeField]
    int maxTouchCount = 10;

    [HideInInspector]
    public bool hasToUpdate = true;

    //privates
    List<CustomRaycastHit> lastRaycastHits = null;

    public bool onlyFirstHit = false;

    public LayerMask layerMask;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasToUpdate)
            UpdateRaycasts();
    }

    void UpdateRaycasts()
    {
        lastRaycastHits = new List<CustomRaycastHit>();

        if (mouse)
        {
            FireRayCast(Input.mousePosition, "mouse");
        }

        if (touch)
        {
            int max = Mathf.Min(Input.touchCount, maxTouchCount);
            for (int i = 0; i < max; i++)
            {
                FireRayCast(Input.GetTouch(i).position, "touch" + i.ToString());
            }
        }
    }


    void FireRayCast(Vector3 inputPos, string inputName)
    {
        RaycastHit[] hits3D = null;
        RaycastHit2D[] hits2D = null;

        List<CustomRaycastHit> validRayHits = new List<CustomRaycastHit>();

        Ray ray = Camera.main.ScreenPointToRay(inputPos);
        if (_3D)
        {
            hits3D = Physics.RaycastAll(ray, 100f, layerMask);

            System.Array.Sort(hits3D, (x, y) => x.distance.CompareTo(y.distance));

            for (int i = 0; i < hits3D.Length; i++)
            {
                if (hits3D[i].collider.GetComponent<AbstractMouseInteractible>())
                {
                    CustomRaycastHit newHit = new CustomRaycastHit();
                    newHit.raycastHit3D = hits3D[i];
                    newHit.inputName = inputName;
                    newHit.debug_colliderName = hits3D[i].collider.gameObject.name;

                    validRayHits.Add(newHit);
                    if (onlyFirstHit) break;
                }
            }

        }

        if (_2D && !(validRayHits.Count > 0 && onlyFirstHit))
        {
            hits2D = Physics2D.RaycastAll(ray.origin, ray.direction, layerMask);

            for (int i = 0; i < hits2D.Length; i++)
            {
                if (hits2D[i].collider.GetComponent<AbstractMouseInteractible>())
                {
                    CustomRaycastHit newHit = new CustomRaycastHit();
                    newHit.raycastHit2D = hits2D[i];
                    newHit.inputName = inputName;
                    newHit.is2D = true;

                    validRayHits.Add(newHit);
                    if (onlyFirstHit) break;
                }
            }
        }

        if (lastRaycastHits == null)
            lastRaycastHits = new List<CustomRaycastHit>(validRayHits);
        else
        {
            foreach (CustomRaycastHit hit in validRayHits)
            {
                lastRaycastHits.Add(hit);
            }
        }
    }

    public Vector3 GetHitPoint(string tag)
    {
        for (int i = 0; i < lastRaycastHits.Count; i++)
        {
            if (lastRaycastHits[i].is2D)
            {
                if (lastRaycastHits[i].raycastHit2D.collider.tag == tag) return lastRaycastHits[i].raycastHit2D.point;
            }
            else
            {
                if (lastRaycastHits[i].raycastHit3D.collider.tag == tag) return lastRaycastHits[i].raycastHit3D.point;
            }
        }

        return Vector3.negativeInfinity;
    }

    public bool GetGameObjectIsTouched(GameObject go)
    {
        return GetGameObjectIsTouched(go, "");
    }
    public bool GetGameObjectIsTouched(GameObject go, string inputName)
    {
        if (lastRaycastHits == null) return go == null;

        for (int i = 0; i < lastRaycastHits.Count; i++)
        {
            bool inputNameMatches = lastRaycastHits[i].inputName == inputName || inputName == "";

            if (lastRaycastHits[i].is2D)
            {
                if (lastRaycastHits[i].raycastHit2D.collider.gameObject == go && inputNameMatches) return true;
            }
            else
            {
                if (lastRaycastHits[i].raycastHit3D.collider.gameObject == go && inputNameMatches) return true;
            }
        }

        return false;
    }
}
