using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Attack attack;

        private void Start()
        {
            attack = GetComponentInParent<King>().attackStage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            attack.DoDamage(other);
        }
    }
}
