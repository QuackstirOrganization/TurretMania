using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Deprecated

    public int rotationOffset = 90;
    public float rotZ;

    public float MoveSpeed;
    public float minMoveSpeed;
    public float maxMoveSpeed;

    [Header("Bobbing Influence"), Space(30)]
    public float BobInfluence;
    public float maxBobInfluence;
    public float minBobInfluence;
    public bool isBobInfluenced;
    private float initialBobInfluence;

    [Header("Bobbing Variables")]
    public float BobSpeed;
    public float BobAmp;
    public float BobRotation;

    private Transform Player;
    private Rigidbody2D Rb2D;

    private float BobSeed;
    private void Start()
    {
        BobInfluence = isBobInfluenced ?
            Random.Range(minBobInfluence, maxBobInfluence) : 1;

        initialBobInfluence = BobInfluence;

        BobSeed = Random.Range(-5.0f, 5.0f);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void Update()
    {
        // subtracting the position of the player from the mouse position
        Vector3 difference = Player.position - transform.position;
        difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + (rotationOffset + (Bobbing() * -BobRotation)));
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position + (transform.up * Bobbing()), Time.deltaTime * MoveSpeed);

        //Debug.Log("Enemy Movement: " + Vector3.Distance(transform.position, Player.position));

        if (Vector3.Distance(transform.position, Player.position) < 2)
        {

            BobInfluence = Mathf.Lerp(BobInfluence, 0.2f, Time.deltaTime);

            //MoveSpeed = 4;

            //initialBobInfluence - Vector3.Distance(transform.position, Player.position);
            //BobInfluence = Mathf.Clamp(BobInfluence, 0, maxBobInfluence);

        }

    }

    float Bobbing()
    {
        //Debug.Log(BobAmp * Mathf.Sin(Time.time * BobSpeed));
        return (BobAmp * BobInfluence) * Mathf.Sin((Time.time + BobSeed) * (BobSpeed * BobInfluence));
    }
    #endregion
}
