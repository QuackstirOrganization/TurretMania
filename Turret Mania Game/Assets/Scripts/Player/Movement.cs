using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D Rb2D;
    public float Speed;
    public float Acceleration;

    // Start is called before the first frame update
    void Start()
    {
        Rb2D = this.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 newMoveVector)
    {
        //Rb2D.velocity = newMoveVector * Speed;

        Rb2D.velocity = Vector2.Lerp(Rb2D.velocity, newMoveVector * Speed, Acceleration);
    }
}
