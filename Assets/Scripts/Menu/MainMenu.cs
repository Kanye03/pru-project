using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Play()
        {
            SceneManager.LoadScene("Scene1");
        }

        // Update is called once per frame
        public void Quit()
        {
            Debug.Log("Quit button pressed!");
            
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
