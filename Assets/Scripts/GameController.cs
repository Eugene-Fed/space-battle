using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLable;
    public UnityEngine.UI.Text healthLable;
    public UnityEngine.UI.Text scoreRecordLable;
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button exitGameButton;
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button closeCredits;
    public UnityEngine.UI.Button openCredits;

    public GameObject player;
    public GameObject gameMenu;
    public GameObject creditsGrid;
    public GameObject handlers; //папка элементов управления персонажем
    public GameObject leftJoystick; //левый джойстик для перемещений


    public bool isStarted = false;
    
    
    int score = 0;
    int scoreRecord = 0; //записывается последний лучший результат
    //int health = 0;

    public static GameController instance;

    public void IncrementScore(int increment)
    {
        score += increment;
        scoreLable.text = "Score: " + score;
        if (score > scoreRecord)
        {
            scoreRecord = score;
            scoreRecordLable.text = "Best Score: " + scoreRecord;
        } 
    }

    public void ChangeHealth(int health) //этот метод лишь отображает здоровье. подсчет идет непосредственно в объекте игрока
    {
        //health += change;
        healthLable.text = "Health: " + health; 
    }

    public void ActiveMenuButton()
    {
        gameMenu.gameObject.SetActive(true);
        handlers.gameObject.SetActive(false);
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
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield"); // поиск массива объектов
        foreach (GameObject item in shields)
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
        creditsGrid.gameObject.SetActive(false);
        handlers.gameObject.SetActive(false);
        instance = this;

        startButton.onClick.AddListener(delegate { //запуск игры из главного меню
            menu.gameObject.SetActive(false);
            isStarted = true;
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(true); //активируем джойстик ПЕРЕД созданием игрока (игрок создается в RestartGame())
            RestartGame();
        });

        openCredits.onClick.AddListener(delegate { //открыть Кредитс
            creditsGrid.gameObject.SetActive(true);
        });

        closeCredits.onClick.AddListener(delegate { //закрыть Кредитс
            creditsGrid.gameObject.SetActive(false);
        });

        exitGameButton.onClick.AddListener(delegate { //выход в главное меню
            menu.gameObject.SetActive(true);
            isStarted = false;
            RestartGame(); // делаем рестарт, чтобы уничтожить всех оставшихся врагов и астероидов
            Destroy(player); // после чего удаляем игрока, т.к. он создается в методе RestartGame()
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(false); // отключаем джойстик ПОСЛЕ игрока, иначе возникнет ошибка при поиске объекта джойстика
        });

        restartButton.onClick.AddListener(delegate { //перезапуск игры
            menu.gameObject.SetActive(false);
            isStarted = true;
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(true); //активируем Джойстик ДО создания игрока
            RestartGame();
        });
    }

}
