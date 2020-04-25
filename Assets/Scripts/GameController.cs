using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLable;
    public UnityEngine.UI.Text healthLable;
    
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


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

}
