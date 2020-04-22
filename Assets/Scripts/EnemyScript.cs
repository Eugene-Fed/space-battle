using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float greenShotDelay;
    public int health; //дает врагу единицы здоровья

    public GameObject laserShotRedEnemy;
    public GameObject laserGunRedLeft;
    public GameObject laserGunRedRight;

    float nextGreenShotTime;
    Rigidbody enemyShip;

    // Start is called before the first frame update
    void Start()
    {
        enemyShip = GetComponent<Rigidbody>();
        //asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;

        float zSpeed = Random.Range(minSpeed, maxSpeed);
        enemyShip.velocity = new Vector3(0, 0, -zSpeed);
        enemyShip.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //enemyShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed
        //enemyShip.rotation = Quaternion.Euler(enemyShip.velocity.z * tiltPitch, 0, -enemyShip.velocity.x * tiltRoll);

        if (Time.time > nextGreenShotTime)
        {
            //Instantiate(laserShotRedEnemy, laserGunRedLeft.transform.position, Quaternion.identity);
            Instantiate(laserShotRedEnemy, laserGunRedLeft.transform.position, Quaternion.Euler(0, 180, 0));
            Instantiate(laserShotRedEnemy, laserGunRedRight.transform.position, Quaternion.Euler(0, 180, 0));

            nextGreenShotTime = Time.time + greenShotDelay;
        }

        
    }
}
