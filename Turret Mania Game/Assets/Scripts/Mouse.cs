using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Vector3 mousePosition, targetPosition;
    public Transform targetObject;
    float distance = 10;

    void Update()
    {
        //Cursor.visible = false;
        //To get the current mouse position
        mousePosition = Input.mousePosition;
        //Convert the mousePosition according to World position
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distance));
        //Set the position of targetObject
        targetObject.position = targetPosition;
        //Debug.Log(mousePosition+" "+targetPosition);
        //If Left Button is clicked

    }
}

