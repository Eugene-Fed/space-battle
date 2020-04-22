using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float greenShotDelay;
    public int health; //дает врагу единицы здоровья
    public int shieldHelth; //количество брони от полученного щита
    public int yellowLaserPower; //мощность вражеского лазера
    public int greenLaserPower; //мощность вражеского лазера
    public int asteroidPower; //сила удара астероида

    public GameObject laserShotRedEnemy;
    public GameObject laserGunRedLeft;
    public GameObject laserGunRedRight;
    public GameObject playerExplosion;

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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GreenLaser": //разбито на разные версии Лазера на случай, если разные лазеры будут забирать разное количество HP, а не уничтожать сразу
                Destroy(other.gameObject); //убираем лазер
                health -= greenLaserPower;
                break;
            case "YellowLaser": //разбито на разные версии Лазера на случай, если разные лазеры будут забирать разное количество HP, а не уничтожать сразу
                Destroy(other.gameObject); //убираем лазер
                health -= yellowLaserPower;
                break;
            case "Asteroid":
                health -= asteroidPower;
                Debug.Log("Сокрушительное столкновение с астероидом! Минус " + asteroidPower + " брони! Защита = " + health);
                break;
            case "Enemy":
                DestroySelf();
                break;
            case "Player":
                DestroySelf();
                break;
            case "Shield":
                Destroy(other.gameObject); //заменить цветной щит на белый если он сработал
                health += shieldHelth;
                Debug.Log("УПС! Враг своровал броню! Плюс " + shieldHelth + " к его броне. Вражеская броня усилена до " + health + " единиц!!!");
                //Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        if (health <= 0)
        {
            DestroySelf();
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
    }
    
}
