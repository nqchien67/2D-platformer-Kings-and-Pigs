using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public string sceneName = "";
    private List<Scene> activeScenes = new List<Scene>();

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            activeScenes.Add(SceneManager.GetSceneAt(i));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sceneName != "")
                SceneManager.LoadScene(sceneName);

            SceneManager.LoadScene(activeScenes[0].name);
            for (int i = 1; i < activeScenes.Count; ++i)
                SceneManager.LoadScene(activeScenes[i].name, LoadSceneMode.Additive);
        }
    }
}
