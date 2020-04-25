﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLable;
    public UnityEngine.UI.Text healthLable;
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button menuButton;
    public UnityEngine.UI.Button restartButton;

    public GameObject player;

    public bool isStarted = false;
    
    
    int score = 0;
    //int health = 0;

    public static GameController instance;

    public void IncrementScore(int increment)
    {
        score += increment;
        scoreLable.text = "Score: " + score; 
    }

    public void ChangeHealth(int health) //этот метод лишь отображает здоровье. подсчет идет непосредственно в объекте игрока
    {
        //health += change;
        healthLable.text = "Health: " + health; 
    }

    public void ActiveMenuButton()
    {
        menuButton.gameObject.SetActive(true);
    }

    void RestartGame() //очищает сцену перед запуском новой игры
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid"); // поиск массива объектов
        foreach (GameObject item in asteroids)
        {
            Destroy(item);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // поиск массива объектов
        foreach (GameObject item in enemies)
        {
            Destroy(item);
        }
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("RedEnemyLaser"); // поиск массива объектов
        foreach (GameObject item in lasers)
        {
            Destroy(item);
        }

        
        Instantiate(player, player.transform.position, Quaternion.identity); //добавляем игрока только по нажатию на кнопку Start
        IncrementScore(-score);
    }

    // Start is called before the first frame update
    void Start()
    {
        menu.gameObject.SetActive(true);
        instance = this;

        startButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(false);
            isStarted = true;
            menuButton.gameObject.SetActive(false);
            RestartGame();
        });

        menuButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(true);
            isStarted = false;
            menuButton.gameObject.SetActive(false);
        });

        restartButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(false);
            isStarted = true;
            menuButton.gameObject.SetActive(false);
            RestartGame();
        });
    }

}
