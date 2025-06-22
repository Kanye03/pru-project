using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managements;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;

        private GameManager gameManager;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            // Tìm GameManager
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                // Tạo GameManager nếu không có
                GameObject gameManagerObj = new GameObject("GameManager");
                gameManager = gameManagerObj.AddComponent<GameManager>();
            }
        }

        private void Start()
        {
            if (playAgainButton == null)
            {
                Debug.LogError("DeathScreen: playAgainButton is null! Please assign it in inspector.");
                return;
            }
            
            if (quitButton == null)
            {
                Debug.LogError("DeathScreen: quitButton is null! Please assign it in inspector.");
                return;
            }
            
            ResetDeathScreen();
            
            playAgainButton.onClick.AddListener(PlayAgain);
            quitButton.onClick.AddListener(Quit);
            Debug.Log("DeathScreen: Button events assigned");
        }

        public void ResetDeathScreen()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            Time.timeScale = 1f; 
            Debug.Log("DeathScreen: Panel hidden via CanvasGroup and time scale reset");
        }

        public void ShowDeathScreen()
        {
            Debug.Log("DeathScreen: ShowDeathScreen() called");
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0f;
            Debug.Log("DeathScreen: Panel shown via CanvasGroup and game paused");
        }

        public void PlayAgain()
        {
            if (gameManager != null)
            {
                gameManager.RestartGame();
            }
            else
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("Scene1");
            }
        }

        public void Quit()
        {
            if (gameManager != null)
            {
                gameManager.QuitToMainMenu();
            }
            else
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
} 