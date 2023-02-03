using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public float blinkTime;
        private GameObject text;

        private void Start()
        {
            text = transform.GetChild(2).gameObject;
            StartCoroutine(BlinkText());
        }

        private void Update()
        {
            if (Input.anyKey)
                StartGame();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay");
            SceneManager.LoadSceneAsync("Scene1", LoadSceneMode.Additive);
        }

        private IEnumerator BlinkText()
        {
            while (true)
            {
                text.SetActive(!text.activeSelf);
                yield return new WaitForSeconds(blinkTime);
            }
        }
    }
}