using System.Collections;
using Enemies;
using Managements;
using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

namespace Player
{
    public class PlayerHealth : Singleton<PlayerHealth>
    {
        public bool isDead { get; private set; }

        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float knockBackThrustAmount = 10f;
        [SerializeField] private float damageRecoveryTime = 1f;

        private Slider healthSlider;
        private int currentHealth;
        private bool canTakeDamage = true;
        private Knockback knockback;
        private Flash flash;

        const string HEALTH_SLIDER_TEXT = "Health Slider";
        const string TOWN_TEXT = "Scene1";
        readonly int DEATH_HASH = Animator.StringToHash("Death");

        protected override void Awake()
        {
            base.Awake();

            flash = GetComponent<Flash>();
            knockback = GetComponent<Knockback>();
        }

        private void OnEnable()
        {
            // Chỉ reset player khi scene mới load (không phải khi component được enable)
            // Không reset ở đây để tránh reset khi player đã chết
        }

        private void Start()
        {
            // Reset player khi bắt đầu game
            Debug.Log("PlayerHealth: Start - Initializing player");
            ResetPlayer();
        }

        public void ResetPlayer()
        {
            Debug.Log("PlayerHealth: ResetPlayer() called");
            isDead = false;
            currentHealth = maxHealth;
            canTakeDamage = true;
            
            // Reset animator nếu có
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                // Reset tất cả triggers
                animator.ResetTrigger(DEATH_HASH);
                // Reset về idle state
                animator.Play("Idle");
            }

            UpdateHealthSlider();
            Debug.Log("Player reset - Health: " + currentHealth + "/" + maxHealth);
        }

        // Tìm DeathScreen mỗi khi cần thiết
        private DeathScreen FindDeathScreen()
        {
            DeathScreen foundDeathScreen = FindObjectOfType<DeathScreen>();
            
            if (foundDeathScreen != null)
            {
                Debug.Log("DeathScreen found in scene!");
            }
            else
            {
                Debug.LogWarning("DeathScreen NOT found in scene! Make sure to add DeathScreen component to a GameObject.");
            }
            
            return foundDeathScreen;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

            if (enemy)
            {
                TakeDamage(1, other.transform);
            }
        }

        public void HealPlayer()
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                UpdateHealthSlider();
            }
        }

        public void TakeDamage(int damageAmount, Transform hitTransform)
        {
            if (!canTakeDamage) { return; }

            ScreenShakeManager.Instance.ShakeScreen();
            knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
            canTakeDamage = false;
            currentHealth -= damageAmount;
            StartCoroutine(DamageRecoveryRoutine());
            UpdateHealthSlider();
            CheckIfPlayerDeath();
        }

        private void CheckIfPlayerDeath()
        {
            Debug.Log($"CheckIfPlayerDeath: currentHealth={currentHealth}, isDead={isDead}");
            
            if (currentHealth <= 0 && !isDead)
            {
                Debug.Log("PlayerHealth: Player is dying! Setting isDead = true");
                isDead = true;
                
                // Chỉ hủy vũ khí hiện tại, không hủy ActiveWeapon.Instance
                if (ActiveWeapon.Instance != null && ActiveWeapon.Instance.CurrentActiveWeapon != null)
                {
                    Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
                    ActiveWeapon.Instance.WeaponNull();
                }
                
                currentHealth = 0;
                GetComponent<Animator>().SetTrigger(DEATH_HASH);
                StartCoroutine(DeathRoutine());
            }
            else if (currentHealth <= 0 && isDead)
            {
                Debug.Log("PlayerHealth: Player is already dead, not starting death routine again");
            }
        }

        private IEnumerator DeathRoutine()
        {
            Debug.Log("PlayerHealth: DeathRoutine started - Player died! Starting death routine...");
            yield return new WaitForSeconds(2f);
            
            Debug.Log("PlayerHealth: DeathRoutine - Attempting to show death screen...");
            
            // Tìm DeathScreen mỗi khi cần thiết
            DeathScreen currentDeathScreen = FindDeathScreen();
            
            // Hiển thị death screen thay vì load scene ngay lập tức
            if (currentDeathScreen != null)
            {
                Debug.Log("PlayerHealth: DeathRoutine - DeathScreen found! Calling ShowDeathScreen()");
                currentDeathScreen.ShowDeathScreen();
            }
            else
            {
                Debug.LogWarning("PlayerHealth: DeathRoutine - DeathScreen is null! Loading scene as fallback.");
                // Fallback nếu không tìm thấy DeathScreen
                SceneManager.LoadScene(TOWN_TEXT);
            }
        }

        private IEnumerator DamageRecoveryRoutine()
        {
            yield return new WaitForSeconds(damageRecoveryTime);
            canTakeDamage = true;
        }

        private void UpdateHealthSlider()
        {
            if (healthSlider == null)
            {
                healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            }

            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
}
