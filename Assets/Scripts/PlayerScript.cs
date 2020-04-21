using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    public float speed;
    public float tiltRoll; // наклон вбок
    public float tiltPitch; // наклон вперед-назад
    public float xMin, xMax, zMin, zMax;
    Rigidbody playerShip;
    
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GetComponent<Rigidbody>();
                
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        playerShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        float xPosition = Mathf.Clamp(playerShip.position.x, xMin, xMax);
        float zPosition = Mathf.Clamp(playerShip.position.z, zMin, zMax);
        playerShip.position = new Vector3(xPosition, 0, zPosition);

        playerShip.rotation = Quaternion.Euler(playerShip.velocity.z * tiltPitch, 0, -playerShip.velocity.x * tiltRoll);
    }
}
