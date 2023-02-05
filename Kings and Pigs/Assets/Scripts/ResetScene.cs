using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public string sceneName = "";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sceneName != "")
                SceneManager.LoadScene(sceneName);
            else
            {
                Scene currentScene = SceneManager.GetSceneAt(1);

                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
                SceneManager.LoadScene(currentScene.name, LoadSceneMode.Additive);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
            LoadLevel1();
    }

    private void LoadLevel1()
    {
        SceneManager.LoadScene("Gameplay");
        SceneManager.LoadSceneAsync("Scene1", LoadSceneMode.Additive);
    }
}
