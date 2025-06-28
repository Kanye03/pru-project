using System.Collections;
using Player;
using UnityEngine;
using Managements; // ?? dùng EnemyManager

namespace Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float roamChangeDirFloat = 2f;
        [SerializeField] private float attackRange = 0f;
        [SerializeField] private MonoBehaviour enemyType;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private bool stopMovingWhileAttacking = false;

        private bool canAttack = true;

        private enum State
        {
            Roaming,
            Attacking
        }

        private Vector2 roamPosition;
        private float timeRoaming = 0f;

        private State state;
        private EnemyPathfinding enemyPathfinding;

        private void Awake()
        {
            enemyPathfinding = GetComponent<EnemyPathfinding>();
            state = State.Roaming;
        }

        private void Start()
        {
            roamPosition = GetRoamingPosition();

            // ??m s? enemy khi t?o ra
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.EnemySpawned();
            }
        }

        private void Update()
        {
            MovementStateControl();
        }

        private void MovementStateControl()
        {
            switch (state)
            {
                default:
                case State.Roaming:
                    Roaming();
                    break;

                case State.Attacking:
                    Attacking();
                    break;
            }
        }

        private void Roaming()
        {
            timeRoaming += Time.deltaTime;

            enemyPathfinding.MoveTo(roamPosition);

            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
            {
                state = State.Attacking;
            }

            if (timeRoaming > roamChangeDirFloat)
            {
                roamPosition = GetRoamingPosition();
            }
        }

        private void Attacking()
        {
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
            {
                state = State.Roaming;
            }

            if (attackRange != 0 && canAttack)
            {
                canAttack = false;
                (enemyType as IEnemy).Attack();

                if (stopMovingWhileAttacking)
                {
                    enemyPathfinding.StopMoving();
                }
                else
                {
                    enemyPathfinding.MoveTo(roamPosition);
                }

                StartCoroutine(AttackCooldownRoutine());
            }
        }

        private IEnumerator AttackCooldownRoutine()
        {
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        private Vector2 GetRoamingPosition()
        {
            timeRoaming = 0f;
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        // G?i hàm này khi enemy b? gi?t (t? Health script ch?ng h?n)
        public void Die()
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.EnemyDied();
            }

            Destroy(gameObject);
        }

        // D? phòng: n?u object b? h?y, v?n ??m b?o tr? ?úng
        private void OnDestroy()
        {
            if (EnemyManager.Instance != null && EnemyManager.Instance.aliveEnemies > 0)
            {
                EnemyManager.Instance.EnemyDied();
            }
        }
    }
}
