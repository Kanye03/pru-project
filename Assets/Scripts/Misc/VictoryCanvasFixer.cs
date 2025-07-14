using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class VictoryCanvasFixer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("VictoryCanvasFixer: Starting to fix Victory object setup...");
            FixVictorySetup();
        }
        
        private void Update()
        {
            // Press J to manually fix and show victory
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("VictoryCanvasFixer: J key pressed! Fixing and showing victory...");
                FixVictorySetup();
                ShowVictory();
            }
        }
        
        private void FixVictorySetup()
        {
            // Find the Victory object
            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject == null)
            {
                Debug.LogError("VictoryCanvasFixer: Victory object not found!");
                return;
            }
            
            Debug.Log("VictoryCanvasFixer: Victory object found, checking setup...");
            
            // Check if it's already under a Canvas
            Canvas parentCanvas = victoryObject.GetComponentInParent<Canvas>();
            if (parentCanvas != null)
            {
                Debug.Log($"VictoryCanvasFixer: Victory is already under Canvas: {parentCanvas.name}");
                // Make sure the canvas is properly configured
                parentCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                parentCanvas.sortingOrder = 1000; // High priority to be on top
                return;
            }
            
            Debug.Log("VictoryCanvasFixer: Victory is NOT under a Canvas. Creating one...");
            
            // Create a new Canvas for Victory
            GameObject canvasObject = new GameObject("Victory Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000; // High sorting order to appear on top
            
            // Add CanvasScaler for proper scaling
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
            
            // Add GraphicRaycaster for UI interactions
            canvasObject.AddComponent<GraphicRaycaster>();
            
            // Move Victory object under the new Canvas
            victoryObject.transform.SetParent(canvasObject.transform, false);
            
            // Make sure Victory has proper UI components
            SetupVictoryUI(victoryObject);
            
            Debug.Log("VictoryCanvasFixer: Victory Canvas created and Victory object moved under it!");
        }
        
        private void SetupVictoryUI(GameObject victoryObject)
        {
            // Make sure it has a RectTransform
            RectTransform rectTransform = victoryObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.Log("VictoryCanvasFixer: Adding RectTransform to Victory object...");
                rectTransform = victoryObject.AddComponent<RectTransform>();
            }
            
            // Center it on screen
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            
            // Set a reasonable size if it's too small
            if (rectTransform.sizeDelta.x < 100 || rectTransform.sizeDelta.y < 100)
            {
                rectTransform.sizeDelta = new Vector2(600, 300); // Good size for victory banner
                Debug.Log("VictoryCanvasFixer: Set Victory size to 600x300");
            }
            
            // Make sure it has an Image component
            Image imageComponent = victoryObject.GetComponent<Image>();
            if (imageComponent == null)
            {
                Debug.Log("VictoryCanvasFixer: Adding Image component to Victory object...");
                imageComponent = victoryObject.AddComponent<Image>();
            }
            
            // Make sure the image is visible
            imageComponent.enabled = true;
            imageComponent.color = Color.white; // Full opacity
            
            // Check if sprite is assigned
            if (imageComponent.sprite == null)
            {
                Debug.LogWarning("VictoryCanvasFixer: Victory Image has no sprite assigned. It will show as white rectangle.");
                Debug.LogWarning("VictoryCanvasFixer: Please assign the win sprite manually in the Inspector.");
            }
            else
            {
                Debug.Log($"VictoryCanvasFixer: Victory Image has sprite: {imageComponent.sprite.name}");
            }
            
            Debug.Log("VictoryCanvasFixer: Victory UI setup complete!");
        }
        
        private void ShowVictory()
        {
            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject != null)
            {
                victoryObject.SetActive(true);
                Debug.Log("VictoryCanvasFixer: Victory object activated and should be visible!");
            }
        }
    }
}
