using UnityEngine;
using UI;

namespace Misc
{
    public class DeathScreenTester : MonoBehaviour
    {
        private DeathScreen deathScreen;
        private int testCount = 0;

        private void Start()
        {
            deathScreen = FindObjectOfType<DeathScreen>();
            
            if (deathScreen != null)
            {
                Debug.Log("DeathScreenTester: Found DeathScreen component");
            }
            else
            {
                Debug.LogWarning("DeathScreenTester: No DeathScreen found in scene");
            }
        }

        private void Update()
        {
            // Nhấn phím T để test death screen
            if (Input.GetKeyDown(KeyCode.T))
            {
                testCount++;
                Debug.Log($"DeathScreenTester: T key pressed - testing death screen (Test #{testCount})");
                
                if (deathScreen != null)
                {
                    deathScreen.ShowDeathScreen();
                }
                else
                {
                    Debug.LogError("DeathScreenTester: Cannot test - DeathScreen is null!");
                }
            }
            
            // Nhấn phím R để reset death screen
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("DeathScreenTester: R key pressed - resetting death screen");
                
                if (deathScreen != null)
                {
                    deathScreen.ResetDeathScreen();
                }
                else
                {
                    Debug.LogError("DeathScreenTester: Cannot reset - DeathScreen is null!");
                }
            }
        }
    }
} 