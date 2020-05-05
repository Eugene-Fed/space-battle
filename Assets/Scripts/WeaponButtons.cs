using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool weaponButtonDown;

    public bool WeaponButtonDown
    {
        get
        {
            return weaponButtonDown;
        }
    }

    //void OnMouseDown()
    public void OnPointerDown(PointerEventData eventData) //должен быть public, иначе не имплементируется IPointerDownHandler
    {
        weaponButtonDown = true;
        Debug.Log("weaponButtonsDown = " + weaponButtonDown);
    }

    //public void OnMouseUp()
    public void OnPointerUp(PointerEventData eventData)
    {
        weaponButtonDown = false;
        Debug.Log("weaponButtonsDown = " + weaponButtonDown);
    }


/* 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 */
}
