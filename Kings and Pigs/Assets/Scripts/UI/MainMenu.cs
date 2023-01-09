using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay");
            SceneManager.LoadSceneAsync("Scene1", LoadSceneMode.Additive);
        }
    }
}