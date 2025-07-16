using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Managements
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        public int aliveEnemies = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // ??m b?o kh�ng c� 2 EnemyManager
            }
        }

        public void EnemySpawned()
        {
            aliveEnemies++;
            Debug.Log($"Enemy Spawned. Alive enemies: {aliveEnemies}");
        }

        public void EnemyDied()
        {
            aliveEnemies = Mathf.Max(0, aliveEnemies - 1); // Prevent negative
            Debug.Log($"Enemy Died. Alive enemies: {aliveEnemies}");

            // Check for victory immediately when enemy count reaches 0
            if (aliveEnemies <= 0)
            {
                Debug.Log("EnemyManager: ALL ENEMIES DEFEATED! Starting victory sequence...");
                StartCoroutine(TriggerVictoryAfterFrame());
            }
        }

        private IEnumerator TriggerVictoryAfterFrame()
        {
            // Wait a frame to ensure everything is properly updated
            yield return null;

            Debug.Log("EnemyManager: Triggering victory...");

            // Find and show victory object
            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject != null)
            {
                Debug.Log($"EnemyManager: Victory object found! Current active state: {victoryObject.activeInHierarchy}");

                victoryObject.SetActive(true);

                Debug.Log($"EnemyManager: Victory object activated! New active state: {victoryObject.activeInHierarchy}");
                Debug.Log($"EnemyManager: Victory position: {victoryObject.transform.position}");
                Debug.Log($"EnemyManager: Victory scale: {victoryObject.transform.localScale}");

                // Check if it has Image component and sprite
                UnityEngine.UI.Image imageComponent = victoryObject.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null)
                {
                    Debug.Log($"EnemyManager: Victory Image component found. Enabled: {imageComponent.enabled}");
                    Debug.Log($"EnemyManager: Victory Image color: {imageComponent.color}");
                    Debug.Log($"EnemyManager: Victory Image sprite: {(imageComponent.sprite != null ? imageComponent.sprite.name : "NULL")}");
                }
                else
                {
                    Debug.LogWarning("EnemyManager: Victory object has no Image component!");
                }

                // Check Canvas and fix if needed
                Canvas canvas = victoryObject.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    Debug.Log($"EnemyManager: Victory is under Canvas: {canvas.name}, enabled: {canvas.enabled}");
                    canvas.enabled = true;
                    canvas.sortingOrder = 1000; // Make sure it's on top
                }
                else
                {
                    Debug.LogError("EnemyManager: Victory object is NOT under a Canvas! Fixing this now...");
                    FixVictoryCanvas(victoryObject);
                }

                Debug.Log("EnemyManager: VICTORY! Victory banner should be shown. Returning to menu in 3 seconds...");
                StartCoroutine(ReturnToMenuAfterDelay());
            }
            else
            {
                Debug.Log("EnemyManager: Victory object NOT found! This means player can continue playing (not a final level).");
                // Do NOT return to menu if no Victory object is found
                // This allows the player to continue playing in intermediate levels
            }
        }

        private IEnumerator ReturnToMenuAfterDelay()
        {
            Debug.Log("EnemyManager: Waiting 3 seconds before returning to menu...");
            yield return new WaitForSeconds(3f);

            Debug.Log("EnemyManager: Time's up! Returning to main menu...");

            // Return to main menu
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Debug.Log("EnemyManager: Using GameManager to quit to main menu");
                gameManager.QuitToMainMenu();
            }
            else
            {
                Debug.Log("EnemyManager: GameManager not found, using SceneManager directly");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }

        private void FixVictoryCanvas(GameObject victoryObject)
        {
            Debug.Log("EnemyManager: Creating Canvas for Victory object...");

            // Create a new Canvas for Victory
            GameObject canvasObject = new GameObject("Victory Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000; // High sorting order to appear on top

            // Add CanvasScaler for proper scaling
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);

            // Add GraphicRaycaster for UI interactions
            canvasObject.AddComponent<GraphicRaycaster>();

            // Move Victory object under the new Canvas
            victoryObject.transform.SetParent(canvasObject.transform, false);

            // Make sure Victory has proper UI setup
            SetupVictoryUI(victoryObject);

            Debug.Log("EnemyManager: Victory Canvas created and Victory object moved under it!");
        }

        private void SetupVictoryUI(GameObject victoryObject)
        {
            // Make sure it has a RectTransform
            RectTransform rectTransform = victoryObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.Log("EnemyManager: Adding RectTransform to Victory object...");
                rectTransform = victoryObject.AddComponent<RectTransform>();
            }

            // Center it on screen
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;

            // Set a reasonable size
            rectTransform.sizeDelta = new Vector2(600, 300);

            // Make sure it has an Image component
            UnityEngine.UI.Image imageComponent = victoryObject.GetComponent<UnityEngine.UI.Image>();
            if (imageComponent == null)
            {
                Debug.Log("EnemyManager: Adding Image component to Victory object...");
                imageComponent = victoryObject.AddComponent<UnityEngine.UI.Image>();
            }

            // Make sure the image is visible
            imageComponent.enabled = true;
            imageComponent.color = Color.white; // Full opacity

            Debug.Log("EnemyManager: Victory UI setup complete!");
        }

        public bool AllEnemiesDefeated()
        {
            return aliveEnemies <= 0;
        }
    }
}
