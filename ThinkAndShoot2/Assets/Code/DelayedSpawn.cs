using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSpawn : MonoBehaviour
{
    public float delay = 0f;
    public float randomness = 0f;
    float startTime = 0f;
    float random = 0f;

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        random = Random.Range(-randomness, randomness);
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= startTime + delay + random)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(this);
        }
    }
}
