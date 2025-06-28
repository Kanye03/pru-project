using UnityEngine;

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
                Destroy(gameObject); // ??m b?o không có 2 EnemyManager
            }
        }

        public void EnemySpawned()
        {
            aliveEnemies++;
            Debug.Log($"Enemy Spawned. Alive enemies: {aliveEnemies}");
        }

        public void EnemyDied()
        {
            aliveEnemies = Mathf.Max(0, aliveEnemies - 1); // Ng?n không cho âm
            Debug.Log($"Enemy Died. Alive enemies: {aliveEnemies}");
        }

        public bool AllEnemiesDefeated()
        {
            return aliveEnemies <= 0;
        }
    }
}
