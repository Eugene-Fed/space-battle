using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLable;
    public UnityEngine.UI.Text healthLable;
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button exitGameButton;
    public UnityEngine.UI.Button restartButton;

    public GameObject player;
    public GameObject gameMenu;

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
        gameMenu.gameObject.SetActive(true);
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
        instance = this;

        startButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(false);
            isStarted = true;
            gameMenu.gameObject.SetActive(false);
            RestartGame();
        });

        exitGameButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(true);
            isStarted = false;
            gameMenu.gameObject.SetActive(false);
            RestartGame(); // делаем рестарт, чтобы уничтожить всех оставшихся врагов и астероидов
            Destroy(player); // после чего удаляем игрока, т.к. он создается в методе RestartGame()
        });

        restartButton.onClick.AddListener(delegate {
            menu.gameObject.SetActive(false);
            isStarted = true;
            gameMenu.gameObject.SetActive(false);
            RestartGame();
        });
    }

}
