using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TurretGame
{
    public class EventCollision : MonoBehaviour
    {
        public UnityEvent Events_Enter;
        public GameObject Menu;

        private Transform player;
        private Rigidbody2D playerRB2D;
        private PlayerInputManager playerInputManager;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Events_Enter.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Events_Enter.Invoke();
        }

        public void CustomizationTrigger()
        {
            Menu.SetActive(true);
            player.position = this.transform.position;
            playerInputManager.enabled = false;
            playerRB2D.velocity = Vector2.zero;
        }

        public void disableTrigger()
        {
            playerInputManager.enabled = true;
            Menu.SetActive(false);
        }
    }
}
