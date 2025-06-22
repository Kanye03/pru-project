using UnityEngine;
using UI;
using Player;

namespace Misc
{
    public class DeathScreenDebugger : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("=== DeathScreen Debugger ===");
            
            // Tìm tất cả DeathScreen components
            DeathScreen[] deathScreens = FindObjectsOfType<DeathScreen>();
            
            Debug.Log($"Found {deathScreens.Length} DeathScreen component(s) in scene");
            
            if (deathScreens.Length == 0)
            {
                Debug.LogError("No DeathScreen components found! Please add DeathScreen component to a GameObject.");
            }
            else
            {
                for (int i = 0; i < deathScreens.Length; i++)
                {
                    DeathScreen ds = deathScreens[i];
                    Debug.Log($"DeathScreen {i + 1}: {ds.name} (GameObject: {ds.gameObject.name})");
                    
                    // Kiểm tra xem component có được enable không
                    if (ds.enabled)
                    {
                        Debug.Log($"  - Component is enabled");
                    }
                    else
                    {
                        Debug.LogWarning($"  - Component is DISABLED!");
                    }
                    
                    // Kiểm tra xem GameObject có active không
                    if (ds.gameObject.activeInHierarchy)
                    {
                        Debug.Log($"  - GameObject is active in hierarchy");
                    }
                    else
                    {
                        Debug.LogWarning($"  - GameObject is INACTIVE in hierarchy!");
                    }
                }
            }
            
            // Tìm PlayerHealth
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log($"PlayerHealth found: {playerHealth.name}");
                Debug.Log($"Player isDead: {playerHealth.isDead}");
            }
            else
            {
                Debug.LogWarning("No PlayerHealth found in scene!");
            }
            
            // Tìm tất cả GameObjects có tên chứa "Death"
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            int deathObjects = 0;
            
            foreach (GameObject obj in allObjects)
            {
                if (obj.name.ToLower().Contains("death"))
                {
                    Debug.Log($"Found GameObject with 'death' in name: {obj.name}");
                    deathObjects++;
                }
            }
            
            Debug.Log($"Found {deathObjects} GameObject(s) with 'death' in name");
            Debug.Log("=== End Debug ===");
        }
        
        private void Update()
        {
            // Nhấn phím D để debug death screen
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("=== Manual DeathScreen Debug ===");
                
                DeathScreen deathScreen = FindObjectOfType<DeathScreen>();
                if (deathScreen != null)
                {
                    Debug.Log("DeathScreen found, testing ShowDeathScreen()");
                    deathScreen.ShowDeathScreen();
                }
                else
                {
                    Debug.LogError("No DeathScreen found!");
                }
                
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    Debug.Log($"PlayerHealth isDead: {playerHealth.isDead}");
                }
                
                Debug.Log("=== End Manual Debug ===");
            }
        }
    }
} 