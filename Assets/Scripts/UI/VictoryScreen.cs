using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managements;

namespace UI
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private GameObject victoryObject; // Reference to the Victory GameObject in scene
        [SerializeField] private float victoryDisplayTime = 3f; // How long to show victory before returning to menu
        
        private GameManager gameManager;
        private bool victoryShown = false;

        private void Awake()
        {
            // Find GameManager
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                // Create GameManager if none exists
                GameObject gameManagerObj = new GameObject("GameManager");
                gameManager = gameManagerObj.AddComponent<GameManager>();
            }
        }

        private void Start()
        {
            // Find the Victory object if not assigned
            if (victoryObject == null)
            {
                victoryObject = GameObject.Find("Victory");
                if (victoryObject == null)
                {
                    Debug.LogError("VictoryScreen: Victory object not found! Please assign it in inspector or ensure it exists in scene.");
                }
            }

            // Initially hide the victory object
            if (victoryObject != null)
            {
                victoryObject.SetActive(false);
                Debug.Log("VictoryScreen: Victory object hidden at start.");
            }
        }



        public void ShowVictory()
        {
            if (victoryShown) return; // Prevent multiple calls
            
            victoryShown = true;
            Debug.Log("VictoryScreen: All enemies defeated! Showing victory screen.");
            
            // Show the victory object
            if (victoryObject != null)
            {
                victoryObject.SetActive(true);
                Debug.Log("VictoryScreen: Victory banner displayed.");
            }
            
            // Start coroutine to return to menu after delay
            StartCoroutine(ReturnToMenuAfterDelay());
        }

        private IEnumerator ReturnToMenuAfterDelay()
        {
            // Wait for the specified time
            yield return new WaitForSeconds(victoryDisplayTime);
            
            Debug.Log("VictoryScreen: Returning to main menu.");
            
            // Return to main menu
            if (gameManager != null)
            {
                gameManager.QuitToMainMenu();
            }
            else
            {
                // Fallback
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }

        // Public method to manually trigger victory (for testing or other systems)
        public void TriggerVictory()
        {
            ShowVictory();
        }
    }
}
