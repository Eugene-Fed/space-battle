using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject asteroid;
    public float minDelay, maxDelay;

    float nextLaunchTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            float zPosition = transform.position.z;
            Vector3 asteroidPosition = new Vector3(xPosition, 0, zPosition);
            
            Instantiate(asteroid, asteroidPosition, Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
