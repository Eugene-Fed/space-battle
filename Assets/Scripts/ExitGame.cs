using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour
{
    //public GameObject btnMnuExit; //delete this variable declatation
    public GameObject MnuTop;

    void OnMouseDown ()
    {
        //btnMnuExit.SetActive (false);
        Debug.Log("Нажата кнопка Exit");
        MnuTop.SetActive (false);
        Application.Quit();
    }
    
}
