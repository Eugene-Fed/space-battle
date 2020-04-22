using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public int health; //дает астероиду единицы здоровья
    public GameObject asteroidExposion;
    public GameObject playerExplosion;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;

        float zSpeed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -zSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Для того, чтобы поток астероидов был "плотнее" и они не уничтожали друг друга исключил их взаимоодействие между собой
        if (other.tag == "GameBoundary" || other.tag == "Asteroid")
        {
            return;
        }
        
        Destroy(gameObject);
        Destroy(other.gameObject);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }

        Instantiate(asteroidExposion, transform.position, Quaternion.identity);
    }

    private void DestroyAsteroid()
    {
        //
    }
}
