using System.Collections;
using Misc;
using Player;
using UnityEngine;
using Managements;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 3;
        [SerializeField] private GameObject deathVFXPrefab;
        [SerializeField] private float knockBackThrust = 15f;

        private int currentHealth;
        private Knockback knockback;
        private Flash flash;

        private void Awake()
        {
            flash = GetComponent<Flash>();
            knockback = GetComponent<Knockback>();
        }

        private void Start()
        {
            currentHealth = startingHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(CheckDetectDeathRoutine());
        }

        private IEnumerator CheckDetectDeathRoutine()
        {
            yield return new WaitForSeconds(flash.GetRestoreMatTime());
            DetectDeath();
        }

        public void DetectDeath()
        {
            if (currentHealth <= 0)
            {
                Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
                GetComponent<PickUpSpawner>().DropItems();

                // Call Die() to remove enemy from EnemyManager
                GetComponent<EnemyAI>().Die();

                // Check if this was the last enemy and trigger victory
                StartCoroutine(CheckVictoryAfterDeath());

                Destroy(gameObject);
            }
        }

        private IEnumerator CheckVictoryAfterDeath()
        {
            // Wait a frame to ensure the enemy count is updated
            yield return null;

            // Check if all enemies are defeated
            if (EnemyManager.Instance != null && EnemyManager.Instance.AllEnemiesDefeated())
            {
                // Find and trigger victory screen
                UI.VictoryScreen victoryScreen = FindObjectOfType<UI.VictoryScreen>();
                if (victoryScreen != null)
                {
                    victoryScreen.ShowVictory();
                }
                else
                {
                    // Fallback: directly show victory object and return to menu
                    GameObject victoryObject = GameObject.Find("Victory");
                    if (victoryObject != null)
                    {
                        victoryObject.SetActive(true);
                        Debug.Log("Victory! All enemies defeated. Returning to menu in 3 seconds...");
                        StartCoroutine(ReturnToMenuAfterDelay());
                    }
                }
            }
        }

        private IEnumerator ReturnToMenuAfterDelay()
        {
            yield return new WaitForSeconds(3f);

            // Return to main menu
            Managements.GameManager gameManager = FindObjectOfType<Managements.GameManager>();
            if (gameManager != null)
            {
                gameManager.QuitToMainMenu();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
