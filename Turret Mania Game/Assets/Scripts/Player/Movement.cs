using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D Rb2D;
    public float Speed;
    public Transform eye;

    private Vector2 MoveVector;
    // Start is called before the first frame update
    void Start()
    {
        Rb2D = this.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 newMoveVector)
    {
        if (newMoveVector != null)
        {
            MoveVector = newMoveVector;

        }
    }

    private void FixedUpdate()
    {
        Vector2 MovementVector = MoveVector;

        Rb2D.velocity = MovementVector * Speed;

        eye.localPosition = MovementVector * 0.3f;
    }
}
