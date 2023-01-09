using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private GameObject king;

    private void Start()
    {
        king = GameObject.Find("King");
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetSceneAt(1).buildIndex;
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    private IEnumerator LoadScene(int currentSceneIndex)
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(currentSceneIndex);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(currentSceneIndex + 1, LoadSceneMode.Additive);

        Animator animator = king.GetComponent<Animator>();
        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        while (!unloadScene.isDone && !loadScene.isDone)
            yield return null;

        //Scene loadedScene = SceneManager.GetSceneByBuildIndex(currentSceneIndex + 1);
        //SceneManager.SetActiveScene(loadedScene);
    }
}
