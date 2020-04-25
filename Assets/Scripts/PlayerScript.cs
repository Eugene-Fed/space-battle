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
    public float gunAngle; // угол поворота орудий, для установки из интерфейса
    public int health; // количество урона, который может унести игрок на старте
    public int shieldHelth; //количество брони от полученного щита
    public int redEnemyLaserPower; //мощность вражеского лазера
    public int asteroidPower; //сила удара астероида

    //GameObject menuButton; //кнопка, которая появляется после смерти для выхода в главное меню.

    public GameObject laserShotYellow;
    public GameObject laserGunYellow;
    // объекты для боковых пушек
    public GameObject laserShotGreen;
    public GameObject laserGunGreenLeft;
    public GameObject laserGunGreenRight;
    public GameObject playerExplosion;

    Rigidbody playerShip;

    float nextYellowShotTime;
    float nextGreenShotTime;
    float leftGunAngle; //угол поворота левого бокового орудия
    float rightGunAngle; //угол поворота левого бокового орудия
    float startTime; //сохраняет время запуска игры, чтобы посчитать длительность
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Уровень брони = " + health);
        playerShip = GetComponent<Rigidbody>();
        startTime = Time.time; //получаем время активации игры
        leftGunAngle = playerShip.transform.rotation.y - gunAngle; // Задаем угол установки боковых пушек при старте, что бы снизить нагрузку на Update()
        rightGunAngle = playerShip.transform.rotation.y + gunAngle; // Если бы наш Звездолет вращался вокруг оси Y и стрелял по сторонам, выставляли бы по факту из Update()
        GameController.instance.ChangeHealth(health);
        //menuButton = GameObject.FindWithTag("OpenMenuButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isStarted == false)
        {
            return;
        }
        
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        playerShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        float xPosition = Mathf.Clamp(playerShip.position.x, xMin, xMax);
        float zPosition = Mathf.Clamp(playerShip.position.z, zMin, zMax);
        playerShip.position = new Vector3(xPosition, 0, zPosition);

        playerShip.rotation = Quaternion.Euler(playerShip.velocity.z * tiltPitch, 0, -playerShip.velocity.x * tiltRoll);

        if (Time.time > nextGreenShotTime && Input.GetButton("Fire1"))
        {
            Instantiate(laserShotGreen, laserGunGreenLeft.transform.position, Quaternion.Euler(0, leftGunAngle, 0));
            Instantiate(laserShotGreen, laserGunGreenRight.transform.position, Quaternion.Euler(0, rightGunAngle, 0));

            nextGreenShotTime = Time.time + greenShotDelay;
        }

        if (Time.time > nextYellowShotTime && Input.GetButton("Fire2"))
        {
            Instantiate(laserShotYellow, laserGunYellow.transform.position, Quaternion.identity);
            nextYellowShotTime = Time.time + yellowShotDelay;
            /* // пример получения списка объектов по тегу
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid"); // поиск массива объектов
            GameObject asteroid = GameObject.FindWithTag("Asteroid"); // поиск одного объекта
            foreach (GameObject item in asteroids)
            {
                Destroy(item);
            }
            */
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "RedEnemyLaser": //разбито на разные версии Лазера на случай, если разные лазеры будут забирать разное количество HP, а не уничтожать сразу
                Destroy(other.gameObject);
                health -= redEnemyLaserPower;
                Debug.Log("Прямое попадание вражеского лазера! Минус " + redEnemyLaserPower + " единиц брони! Защита = " + health);
                break;
            case "Asteroid": //разбито на разные версии Лазера на случай, если разные лазеры будут забирать разное количество HP, а не уничтожать сразу
                health -= asteroidPower;
                Debug.Log("Сокрушительное столкновение с астероидом! Минус " + asteroidPower + " брони! Защита = " + health);
                break;
            case "Enemy":
                Debug.Log("Капитан Ками Казе сделал свое дело!!! Корабль уничтожен!");
                //DestroySelf();
                health = 0;
                break;
            case "Shield":
                Destroy(other.gameObject); //заменить цветной щит на белый если он сработал
                health += shieldHelth;
                Debug.Log("Щит активирован. Плюс " + shieldHelth + " к броне. Броня усилена до  " + health + " единиц!!!");
                break;
            default:
                break;
        }
        
        if (health <= 0)
        {
            DestroySelf();
        }
        GameController.instance.ChangeHealth(health); //после каждой коллизии обновлять счетчик здоровья
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
        float finishTime = Time.time - startTime;
        Debug.Log("GAME OVER!!! Ты продержался " + finishTime + " секунд.");
        health = 0;
        GameController.instance.ActiveMenuButton(); //Активируем кнопку выхода в Меню
        //menuButton.gameObject.SetActive(true);
        //menuButton.SetActive(true);
    }  
}
