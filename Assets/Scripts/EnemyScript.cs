﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float minSpeedX, maxSpeedX; // для задания боковой скорости врагов
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
    public GameObject playerShip; // нужно для слежение самолетом противника за игроком

    Transform target; // сюда передаем объект Player, чтобы самолеты за ним следили во время движения

    float nextGreenShotTime;
    Rigidbody enemyShip;

    // Start is called before the first frame update
    void Start()
    {
        enemyShip = GetComponent<Rigidbody>();
        //asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        //target = playerShip.transform;

        float zSpeed = Random.Range(minSpeed, maxSpeed);
        float xSpeed = Random.Range(minSpeedX, maxSpeedX);
        enemyShip.velocity = new Vector3(xSpeed, 0, -zSpeed);
        enemyShip.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //enemyShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed
        //enemyShip.rotation = Quaternion.Euler(enemyShip.velocity.z * tiltPitch, 0, -enemyShip.velocity.x * tiltRoll);
        target = playerShip.transform;
        transform.LookAt(target);

        laserShotRedEnemy.transform.rotation = transform.rotation;

        if (Time.time > nextGreenShotTime)
        {
            // Так и не понял как направить на активные координаты игрока. Получилось только на стартовые :С
            
            Instantiate(laserShotRedEnemy, laserGunRedLeft.transform.position, laserShotRedEnemy.transform.rotation);
            Instantiate(laserShotRedEnemy, laserGunRedRight.transform.position, laserShotRedEnemy.transform.rotation);

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
