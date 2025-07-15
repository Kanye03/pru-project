using UnityEngine;

namespace Misc
{
    public class SimpleVictoryTest : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("SimpleVictoryTest: Script started! Press H to test victory.");
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("SimpleVictoryTest: H key pressed! Testing victory...");
                TestVictory();
            }
        }
        
        private void TestVictory()
        {
            // Find Victory object
            GameObject victoryObject = GameObject.Find("Victory");
            
            if (victoryObject == null)
            {
                Debug.LogError("SimpleVictoryTest: Victory object NOT FOUND!");
                
                // List all GameObjects to see what's available
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                Debug.Log($"SimpleVictoryTest: Found {allObjects.Length} GameObjects in scene:");
                
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name.ToLower().Contains("victory") || obj.name.ToLower().Contains("win"))
                    {
                        Debug.Log($"SimpleVictoryTest: Found potential victory object: {obj.name}");
                    }
                }
                return;
            }
            
            Debug.Log($"SimpleVictoryTest: Victory object found! Name: {victoryObject.name}");
            Debug.Log($"SimpleVictoryTest: Victory active before: {victoryObject.activeInHierarchy}");
            Debug.Log($"SimpleVictoryTest: Victory position: {victoryObject.transform.position}");
            
            // Simply activate it
            victoryObject.SetActive(true);
            
            Debug.Log($"SimpleVictoryTest: Victory active after: {victoryObject.activeInHierarchy}");
            Debug.Log("SimpleVictoryTest: Victory should now be visible!");
        }
    }
}
