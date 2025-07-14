using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class VictoryForcer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("VictoryForcer: Script started! Press G to force victory visible.");
        }

        private void Update()
        {
            // Press G to force victory to be visible (F key conflicts with DeathScreenDebugger)
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("VictoryForcer: G key pressed!");
                ForceVictoryVisible();
            }
        }
        
        public void ForceVictoryVisible()
        {
            Debug.Log("VictoryForcer: Forcing victory to be visible... (Press G key)");

            GameObject victoryObject = GameObject.Find("Victory");
            if (victoryObject == null)
            {
                Debug.LogError("VictoryForcer: Victory object not found!");
                return;
            }
            
            // Activate the object
            victoryObject.SetActive(true);
            
            // Make sure it has proper UI setup
            Image imageComponent = victoryObject.GetComponent<Image>();
            if (imageComponent != null)
            {
                // Make sure image is enabled and visible
                imageComponent.enabled = true;
                imageComponent.color = new Color(1f, 1f, 1f, 1f); // Full opacity white
                Debug.Log("VictoryForcer: Image component enabled and made opaque");
            }
            
            // Make sure it's under a Canvas
            Canvas canvas = victoryObject.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("VictoryForcer: Victory is not under a Canvas! Adding one...");
                
                // Create a Canvas if none exists
                GameObject canvasObj = new GameObject("Victory Canvas");
                Canvas newCanvas = canvasObj.AddComponent<Canvas>();
                newCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                newCanvas.sortingOrder = 100; // High sorting order to be on top
                
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
                
                // Move victory under this canvas
                victoryObject.transform.SetParent(canvasObj.transform, false);
                
                Debug.Log("VictoryForcer: Created new Canvas for Victory object");
            }
            else
            {
                // Make sure canvas is enabled and has high sorting order
                canvas.enabled = true;
                canvas.sortingOrder = 100;
                Debug.Log($"VictoryForcer: Canvas found and enabled. Sorting order: {canvas.sortingOrder}");
            }
            
            // Set proper position and scale
            RectTransform rectTransform = victoryObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Center it on screen
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.localScale = Vector3.one;
                
                // Make sure it has a reasonable size
                if (rectTransform.sizeDelta.x < 10 || rectTransform.sizeDelta.y < 10)
                {
                    rectTransform.sizeDelta = new Vector2(400, 200); // Reasonable size
                }
                
                Debug.Log($"VictoryForcer: RectTransform positioned at center. Size: {rectTransform.sizeDelta}");
            }
            
            Debug.Log("VictoryForcer: Victory should now be visible!");
        }
    }
}
