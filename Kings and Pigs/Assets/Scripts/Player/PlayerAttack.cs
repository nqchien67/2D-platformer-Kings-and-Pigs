using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Attack attack;

        private void Start()
        {
            attack = GetComponentInParent<King>().attackState;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            attack.DoDamage(other);
        }
    }
}
