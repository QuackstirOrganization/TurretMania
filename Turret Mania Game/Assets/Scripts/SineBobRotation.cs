using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class SineBobRotation : MonoBehaviour
    {
        [Header("Rotation")]
        public float rotZ;
        public int rotationOffset = 90;

        public SineBob BobbingVariables;
        public float BobRotation;

        public Rigidbody2D rb2D;
        private Transform Player;
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            // subtracting the position of the player from the mouse position
            Vector3 difference = Player.position - transform.position;
            difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees

            transform.localRotation = Quaternion.Euler(rb2D.velocity);
        }
    }
}
