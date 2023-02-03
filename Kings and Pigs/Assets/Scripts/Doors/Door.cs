using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doors
{
    public class Door : MonoBehaviour
    {
        public SceneData data;

        private Animator animator;
        private GameObject king;
        private Animator kingAnimator;

        private bool playerInTrigger;
        private string doorName;
        private int currentLevel;

        protected void Awake()
        {
            animator = GetComponentInParent<Animator>();
            playerInTrigger = false;
            doorName = transform.parent.name;
        }

        private void Start()
        {
            currentLevel = gameObject.scene.buildIndex;
            if (data.previousLevel < 0)
                data.previousLevel = currentLevel;

            king = GameObject.Find("King");
            if (king != null)
            {
                kingAnimator = king.GetComponent<Animator>();
                MovePlayerToCorrectDoor();
            }
        }

        private void Update()
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
            {
                data.previousLevel = currentLevel;
                kingAnimator.SetTrigger("DoorIn");
                animator.SetTrigger("Open");
                LoadLevel();
            }
        }

        private void LoadLevel()
        {
            if (doorName == "EnterDoor")
            {
                int sceneToLoad = currentLevel - 1;
                sceneToLoad = Mathf.Max(sceneToLoad, 2);
                StartCoroutine(LoadScene(currentLevel, sceneToLoad));
            }
            else if (doorName == "ExitDoor")
            {
                int sceneToLoad = currentLevel + 1;
                sceneToLoad = Mathf.Min(sceneToLoad, SceneManager.sceneCountInBuildSettings - 1);
                StartCoroutine(LoadScene(currentLevel, sceneToLoad));
            }
        }

        private void MovePlayerToCorrectDoor()
        {
            if (doorName == "EnterDoor" && data.previousLevel <= currentLevel
                ||
                (doorName == "ExitDoor" && data.previousLevel > currentLevel))
            {
                king.transform.position = transform.position;

                kingAnimator.SetTrigger("DoorOut");
                animator.SetTrigger("Open");

                StartCoroutine(CLoseDoor());
            }
        }

        private IEnumerator LoadScene(int currentSceneIndex, int sceneToLoadIndex)
        {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoadIndex, LoadSceneMode.Additive);
            loadScene.allowSceneActivation = false;

            float animLength = GetCurrentAnimationLength(kingAnimator);
            yield return new WaitForSeconds(animLength - 0.5f);

            SceneManager.UnloadSceneAsync(currentSceneIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

            loadScene.allowSceneActivation = true;
            yield return null;
        }

        private float GetCurrentAnimationLength(Animator animator)
        {
            return animator.GetCurrentAnimatorClipInfo(0).Length;
        }

        private IEnumerator CLoseDoor()
        {
            float animlenth = GetCurrentAnimationLength(kingAnimator);
            yield return new WaitForSeconds(animlenth);

            animator.SetTrigger("Close");
            yield return null;
        }

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