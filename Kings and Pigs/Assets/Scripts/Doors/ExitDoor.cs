using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doors
{
    public class ExitDoor : Door
    {
        protected override void OnStart()
        {
            if (data.previousLevel > currentLevel)
                king.transform.position = transform.position;

            kingAnimator.SetTrigger("DoorOut");

            animator.SetTrigger("Open");

            StartCoroutine(CLoseDoor(kingAnimator));
        }

        protected override void LoadLevel()
        {
            int currentSceneIndex = SceneManager.GetSceneAt(1).buildIndex;
            StartCoroutine(LoadNextScene(currentSceneIndex));
        }

        private IEnumerator CLoseDoor(Animator playerAnimator)
        {
            float animlenth = playerAnimator.GetCurrentAnimatorClipInfo(0).Length;
            yield return new WaitForSeconds(animlenth);

            animator.SetTrigger("Close");
            yield return null;
        }

        private IEnumerator LoadNextScene(int currentSceneIndex)
        {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(currentSceneIndex + 1, LoadSceneMode.Additive);
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
    }
}