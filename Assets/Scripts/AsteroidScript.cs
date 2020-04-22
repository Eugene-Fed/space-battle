using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public int health; //дает астероиду единицы здоровья
    public GameObject asteroidExplosion;
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
/*         
        if (other.tag == "GameBoundary" || other.tag == "Asteroid")
        {
            return;
        }
         */
        switch (other.tag)
        {
            case "Player":
                DestroyAsteroid();
                DestroyShip(other);
                break;
            case "Enemy":
                DestroyAsteroid();
                DestroyShip(other);
                break;
            case "YellowLaser":
                DestroyAsteroid();
                Destroy(other.gameObject); //лазер просто исчезает
                break;
            case "GreenLaser":
                DestroyAsteroid();
                Destroy(other.gameObject); //лазер просто исчезает
                break;
            case "RedEnemyLaser": //разбивка на разные лазеры для дальнейшей реализации разного урона в зависимости от типа лазера
                DestroyAsteroid();
                Destroy(other.gameObject); //лазер просто исчезает
                break;
            case "GameBoundary":
            case "Asteroid":
            default:
                return; //не делаем ничего. Астероиды проскальзывают сквозь друг друга, дабы не превратить игровое пространство в камнеломку
                break;
        }

        DestroyAsteroid();
        //Destroy(gameObject);
        Destroy(other.gameObject);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }

        //Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
    }

    private void DestroyAsteroid()
    {
        Destroy(gameObject);
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
    }

    private void DestroyShip(Collider other)
    {
        Destroy(other.gameObject);
        Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
    }

}
