using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public int rotationOffset = 90;
    public float rotZ;

    private SpriteRenderer spriteRenderer;

    public Sprite Up;
    public Sprite Right;
    public Sprite Left;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // subtracting the position of the player from the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        ChangeTurret(0, 180, Up);
        ChangeTurret(-180, -90, Right);
        ChangeTurret(-90, 0, Left);

    }

    void ChangeTurret(float RotationFrom, float RotationTo, Sprite ChangeTo)
    {
        if (rotZ > RotationFrom && rotZ < RotationTo)
        {
            spriteRenderer.sprite = ChangeTo;
        }
    }
}
