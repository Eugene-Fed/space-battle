using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    public float speed;
    public float tiltRoll; // наклон вбок
    public float tiltPitch; // наклон вперед-назад
    public float xMin, xMax, zMin, zMax;
    public float yellowShotDelay;
    public float greenShotDelay;

    public GameObject laserShotYellow;
    public GameObject laserGunYellow;
    // объекты для боковых пушек
    public GameObject laserShotGreen;
    public GameObject laserGunGreenLeft;
    public GameObject laserGunGreenRight;

    Rigidbody playerShip;

    float nextYellowShotTime;
    float nextGreenShotTime;
    
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GetComponent<Rigidbody>();
                
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        playerShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        float xPosition = Mathf.Clamp(playerShip.position.x, xMin, xMax);
        float zPosition = Mathf.Clamp(playerShip.position.z, zMin, zMax);
        playerShip.position = new Vector3(xPosition, 0, zPosition);

        playerShip.rotation = Quaternion.Euler(playerShip.velocity.z * tiltPitch, 0, -playerShip.velocity.x * tiltRoll);

        if (Time.time > nextYellowShotTime && Input.GetButton("Fire1"))
        {
            Instantiate(laserShotYellow, laserGunYellow.transform.position, Quaternion.identity);
            nextYellowShotTime = Time.time + yellowShotDelay;
        }
        if (Time.time > nextGreenShotTime && Input.GetButton("Fire2"))
        {
            Instantiate(laserShotGreen, laserGunGreenLeft.transform.position, Quaternion.identity);
            Instantiate(laserShotGreen, laserGunGreenRight.transform.position, Quaternion.identity);
            nextGreenShotTime = Time.time + greenShotDelay;
        }

        
    }
}
