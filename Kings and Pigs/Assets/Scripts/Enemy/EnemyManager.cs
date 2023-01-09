using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private Rigidbody2D rb;

        public int healthpoints = 30;
        private bool facingRight = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            Flip();
        }

        private void Flip()
        {
            if (rb.velocity.x != 0)
            {
                if (rb.velocity.x > 0 && !facingRight)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                }
                if (rb.velocity.x < 0 && facingRight)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                facingRight = !facingRight;
            }
        }

        public bool TakeHit()
        {
            healthpoints -= 10;
            bool isDead = healthpoints <= 0;
            if (isDead) _Die();
            return isDead;
        }

        private void _Die()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            // Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}