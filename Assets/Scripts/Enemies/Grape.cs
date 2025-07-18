using Player;
using UnityEngine;

namespace Enemies
{
    public class Grape : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject grapeProjectilePrefab;

        private Animator myAnimator;
        private SpriteRenderer spriteRenderer;

        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Attack()
        {
            myAnimator.SetTrigger(ATTACK_HASH);

            if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        public void SpawnProjectileAnimEvent()
        {
            Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
        }
    }
}
