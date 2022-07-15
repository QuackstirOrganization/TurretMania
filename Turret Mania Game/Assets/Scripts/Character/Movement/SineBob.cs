using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class SineBob : MonoBehaviour
    {
        private Rigidbody2D rb2D;

        [Header("Bobbing Influence")]
        public float BobInfluence;
        public float maxBobInfluence;
        public float minBobInfluence;
        public bool isBobInfluenced;
        private float initialBobInfluence;

        [Header("Bobbing Variables")]
        public float BobSpeed;
        public float BobAmp;

        private float BobSeed;

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();

            BobInfluence = isBobInfluenced ?
                Random.Range(minBobInfluence, maxBobInfluence) : 1;

            initialBobInfluence = BobInfluence;

            BobSeed = Random.Range(-5.0f, 5.0f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + (transform.right * Bobbing()), Time.deltaTime);
        }

        public float Bobbing()
        {
            return (BobAmp * BobInfluence) * Mathf.Sin((Time.time + BobSeed) * (BobSpeed * BobInfluence));
        }
    }
}
