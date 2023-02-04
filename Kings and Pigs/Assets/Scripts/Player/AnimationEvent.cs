using System.Collections;
using UnityEngine;

namespace Player
{
    public class AnimationEvent : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void DisableSimulate()
        {
            rb.simulated = false;
        }

        private void EnableSimulate()
        {
            rb.simulated = true;
        }
    }
}