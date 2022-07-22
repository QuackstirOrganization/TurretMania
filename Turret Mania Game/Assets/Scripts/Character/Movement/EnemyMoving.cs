using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [Header("Rotation Variables")]
    public int rotationOffset = 90;
    private float rotZ;

    [Header("Movement Variables")]
    public float MoveSpeed;

    [Header("Other References")]
    private Transform Player;
    private Rigidbody2D Parent_Rb2D;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Parent_Rb2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // subtracting the position of the player from the mouse position
        Vector3 difference = Player.position - transform.position;
        difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }

    private void FixedUpdate()
    {
        Parent_Rb2D.velocity = transform.up * MoveSpeed;
    }
}
