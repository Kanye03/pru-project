using UnityEngine;
using UI;
using Managements;

namespace Misc
{
    public class VictoryDebugger : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("=== Victory System Debugger ===");
            
            // Check for EnemyManager
            if (EnemyManager.Instance != null)
            {
                Debug.Log($"EnemyManager found! Alive enemies: {EnemyManager.Instance.aliveEnemies}");
            }
            else
            {
                Debug.LogError("EnemyManager.Instance is NULL!");
            }
            
            // Check for Victory object
            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject != null)
            {
                Debug.Log($"Victory object found! Active: {victoryObject.activeInHierarchy}");
                Debug.Log($"Victory object position: {victoryObject.transform.position}");
                Debug.Log($"Victory object scale: {victoryObject.transform.localScale}");

                // Check if it has UI components
                UnityEngine.UI.Image imageComponent = victoryObject.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null)
                {
                    Debug.Log($"Victory has Image component. Color: {imageComponent.color}, Sprite: {imageComponent.sprite?.name}");
                    Debug.Log($"Image enabled: {imageComponent.enabled}");
                }

                // Check Canvas
                Canvas canvas = victoryObject.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    Debug.Log($"Victory is under Canvas: {canvas.name}, Canvas enabled: {canvas.enabled}");
                    Debug.Log($"Canvas render mode: {canvas.renderMode}");
                    Debug.Log($"Canvas sorting order: {canvas.sortingOrder}");
                }
                else
                {
                    Debug.LogWarning("Victory object is NOT under a Canvas! This might be why it's not visible.");
                }

                // Check RectTransform
                RectTransform rectTransform = victoryObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Debug.Log($"Victory RectTransform size: {rectTransform.sizeDelta}");
                    Debug.Log($"Victory anchored position: {rectTransform.anchoredPosition}");
                }
            }
            else
            {
                Debug.LogError("Victory object NOT found!");
            }
            
            // Check for VictoryScreen component
            VictoryScreen victoryScreen = FindObjectOfType<VictoryScreen>();
            if (victoryScreen != null)
            {
                Debug.Log("VictoryScreen component found!");
            }
            else
            {
                Debug.LogWarning("VictoryScreen component NOT found! You need to add it to a GameObject in the scene.");
            }
            
            // Check for GameManager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Debug.Log("GameManager found!");
            }
            else
            {
                Debug.LogWarning("GameManager NOT found!");
            }
            
            Debug.Log("=== End Victory Debug ===");
        }
        
        private void Update()
        {
            // Press V to manually test victory
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("Manual victory test triggered!");
                TestVictory();
            }
            
            // Press E to check enemy count
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EnemyManager.Instance != null)
                {
                    Debug.Log($"Current alive enemies: {EnemyManager.Instance.aliveEnemies}");
                    Debug.Log($"All enemies defeated: {EnemyManager.Instance.AllEnemiesDefeated()}");
                }
            }
        }
        
        private void TestVictory()
        {
            // Try to show victory directly
            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject != null)
            {
                victoryObject.SetActive(true);
                Debug.Log("Victory object activated manually!");
                
                // Try to return to menu after 3 seconds
                StartCoroutine(TestReturnToMenu());
            }
            else
            {
                Debug.LogError("Cannot test victory - Victory object not found!");
            }
        }
        
        private System.Collections.IEnumerator TestReturnToMenu()
        {
            yield return new WaitForSeconds(3f);
            
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Debug.Log("Returning to main menu via GameManager...");
                gameManager.QuitToMainMenu();
            }
            else
            {
                Debug.Log("Returning to main menu via SceneManager...");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
