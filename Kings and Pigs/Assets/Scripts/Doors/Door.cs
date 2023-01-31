using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doors
{
    public abstract class Door : MonoBehaviour
    {
        public SceneData data;

        protected Animator animator;
        protected GameObject king;
        protected Animator kingAnimator;

        protected bool playerInTrigger;
        protected int currentLevel;

        protected void Awake()
        {
            animator = GetComponentInParent<Animator>();
            playerInTrigger = false;
        }

        private void Start()
        {
            currentLevel = SceneManager.GetSceneAt(1).buildIndex;
            if (data.previousLevel < 0)
                data.previousLevel = currentLevel;


            king = GameObject.Find("King");
            if (king != null)
            {
                kingAnimator = king.GetComponent<Animator>();
                OnStart();
            }
        }

        private void Update()
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
            {
                data.previousLevel = SceneManager.GetSceneAt(1).buildIndex;
                kingAnimator.SetTrigger("DoorIn");
                animator.SetTrigger("Open");
                LoadLevel();
            }
        }

        protected abstract void OnStart();
        protected abstract void LoadLevel();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerInTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerInTrigger = false;
        }
    }
}