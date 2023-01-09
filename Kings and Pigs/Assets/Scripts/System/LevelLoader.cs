using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    private Canvas levelLoader;
    private GameObject king;
    private void Start()
    {
        instance = this;
        levelLoader = FindObjectOfType<Canvas>();
        king = GameObject.Find("King");
        Debug.Log(king.name);
    }

    public void LoadNextScene()
    {
        GameObject king = GameObject.Find("King");
        Animator animator = king.GetComponent<Animator>();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(PlayAnimation(animator, currentSceneIndex));
    }

    private IEnumerator PlayAnimation(Animator animator, int currentSceneIndex)
    {
        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Additive);

        //levelLoader.GetComponentInChildren<CanvasGroup>().alpha = Mathf.MoveTowards(0f, 1f, Time.deltaTime);
    }
}
