using UnityEngine;
namespace Player
{
    [CreateAssetMenu(menuName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        public float movingSpeed;
        public float jumpForce;
        public int jumpStep;

        public int attackDamage;
        public float knockbackStrength = 8f;

        [Header("Assists")]
        [Range(0.01f, 0.5f)] public float coyoteTime;
        [Range(0.01f, 0.5f)] public float jumpInputBufferTime;
    }
}