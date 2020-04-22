using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    GameObject asteroid;
    
    //создаю список, по индексам которого проходит Random.Range(). 
    List<GameObject> enemyList;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    
    public float minDelay, maxDelay;

    float nextLaunchTime;

    void Start()
    {
        //Исходя из списка, на каждый большой астероид получаем 2 средних и 3 мелких
        enemyList = new List<GameObject>(){asteroid1, asteroid1, asteroid1, asteroid2, asteroid2, asteroid3, enemy1, enemy2, enemy3};
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            int asteroidIndex = Random.Range(0, enemyList.Count); // получаем рандомный индекс астероида
            asteroid = enemyList[asteroidIndex]; //получаем астероид в соответствии с рандомным индексом
            
            float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            float zPosition = transform.position.z;
            Vector3 asteroidPosition = new Vector3(xPosition, 0, zPosition);
            
            Instantiate(asteroid, asteroidPosition, Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
