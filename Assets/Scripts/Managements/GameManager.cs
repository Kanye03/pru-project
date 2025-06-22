using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

namespace Managements
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            // Reset player khi scene mới load
            ResetGameState();
        }

        public void ResetGameState()
        {
            // Tìm và reset player
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetPlayer();
                Debug.Log("GameManager: Player reset successfully");
            }
            else
            {
                Debug.LogWarning("GameManager: No PlayerHealth found in scene");
            }
            
            // Kiểm tra và tạo lại ActiveWeapon nếu cần
            if (Player.ActiveWeapon.Instance == null)
            {
                Debug.Log("GameManager: ActiveWeapon.Instance is null, creating new one");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    // Tìm hoặc tạo mới ActiveWeapon component
                    Player.ActiveWeapon activeWeapon = player.GetComponent<Player.ActiveWeapon>();
                    if (activeWeapon == null)
                    {
                        activeWeapon = player.AddComponent<Player.ActiveWeapon>();
                        Debug.Log("GameManager: Added ActiveWeapon component to player");
                    }
                }
            }
            
            // Tìm và reset inventory
            UI.ActiveInventory activeInventory = FindObjectOfType<UI.ActiveInventory>();
            if (activeInventory != null)
            {
                activeInventory.EquipStartingWeapon();
                Debug.Log("GameManager: Equipped starting weapon");
            }
            
            // Tìm và reset DeathScreen
            UI.DeathScreen deathScreen = FindObjectOfType<UI.DeathScreen>();
            if (deathScreen != null)
            {
                deathScreen.ResetDeathScreen();
                Debug.Log("GameManager: DeathScreen reset");
            }

            // Reset time scale
            Time.timeScale = 1f;
        }

        public void RestartGame()
        {
            ResetGameState();
            SceneManager.LoadScene("Scene1");
        }

        public void QuitToMainMenu()
        {
            ResetGameState();
            SceneManager.LoadScene("MainMenu");
        }
    }
} 