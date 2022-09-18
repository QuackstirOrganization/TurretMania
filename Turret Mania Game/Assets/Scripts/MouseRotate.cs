using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour
{
    public int rotationOffset = 90;
    public float rotZ;

    private SpriteRenderer spriteRenderer;

    public Sprite Up;
    public Sprite Down;

    public PlayerInput playerInput;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RotateTurret(InputAction.CallbackContext context)
    {
        Debug.Log(playerInput.currentControlScheme);

       

       
    }

    void ChangeTurret(float RotationFrom, float RotationTo, Sprite ChangeTo)
    {
        if (rotZ > RotationFrom && rotZ < RotationTo)
        {
            spriteRenderer.sprite = ChangeTo;
        }
    }
}
