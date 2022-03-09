using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooter : MonoBehaviour
{
    private Vector3 horizontalMovement;
    private float rotationSpeed = 20f;


    // Update is called once per frame
    void Update()
    {
        //Store the Z attribute of the vector3 and it is set negative to ensure it is split appropriatly to the screen
        //The negative z value to rotate clock wise
        horizontalMovement = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(horizontalMovement * rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);
            //the 3rd input is for the raycast distance, for testing purposes 10f
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 10f);

            if(hit)
            {
                Debug.Log("Hit Something : " + hit.collider.name);
                hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
                Debug.Log("Nothing Hit!");
        }
    }
}
