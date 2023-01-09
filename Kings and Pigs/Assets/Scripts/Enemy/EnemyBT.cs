//using _BehaviorTree;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Enemy
//{
//    public class EnemyBT : BehaviourTree, IDamageable, IKnockbackable
//    {
//        public Transform[] waypoints;
//        public EnemyData data;

//        [HideInInspector] public bool IsFacingRight;
//        [HideInInspector] public bool isAnimationFinished;

//        public Rigidbody2D Rigidbody { get; private set; }
//        public CollisionSenses CollisionSenses { get; private set; }
//        public PlayerRangeCheck PlayerRangeCheck { get; private set; }

//        private Vector2 vector2;
//        private bool canSetVelocity;
//        private bool isKnockbackActive;

//        private void Awake()
//        {
//            Rigidbody = GetComponent<Rigidbody2D>();
//            CollisionSenses = GetComponentInChildren<CollisionSenses>();
//            PlayerRangeCheck = new PlayerRangeCheck(transform, data);
//            canSetVelocity = true;
//            IsFacingRight = false;
//            isAnimationFinished = true;
//        }

//        protected override Node SetupTree()
//        {
//            Node root = new Selector(new List<Node>
//            {
//               new Sequence(new List<Node>
//               {
//                    new CheckAttackRange(this),
//                    new Attack(this)
//               }),
//               new Sequence(new List<Node>
//               {
//                   new CheckEnemyFOVRange(this),
//                   new Charge(this)
//               }),
//               new Patrol(this, waypoints),
//            });

//            return root;
//        }

//        protected override void Update()
//        {
//            base.Update();
//            CheckKnockback();
//            Debug.DrawRay(transform.position, Rigidbody.velocity);
//        }

//        public void Damage(int amount)
//        {
//            //Debug.Log(amount + " damage taken!");
//            //Destroy(gameObject);
//        }

//        public void Knockback(float strength, Vector2 angle, int direction)
//        {
//            angle.Normalize();
//            SetVelocity(angle.x * strength * direction, angle.y * strength);
//            canSetVelocity = false;
//            isKnockbackActive = true;
//        }

//        private void CheckKnockback()
//        {
//            if (isKnockbackActive && Rigidbody.velocity.y <= 0.01f && CollisionSenses.isTouchingGround())
//            {
//                isKnockbackActive = false;
//                canSetVelocity = true;
//            }
//        }

//        public void Flip()
//        {
//            IsFacingRight = !IsFacingRight;
//            transform.Rotate(0f, 180f, 0f);
//        }

//        #region Set velocity
//        public void SetVelocityX(float x)
//        {
//            vector2 = Rigidbody.velocity;
//            vector2.x = x;
//            SetFinalVelocity();
//        }

//        public void SetVelocity(float x, float y)
//        {
//            vector2.Set(x, y);
//            SetFinalVelocity();
//        }

//        private void SetFinalVelocity()
//        {
//            if (canSetVelocity)
//                Rigidbody.velocity = vector2;
//        }
//        #endregion


//        #region Animation Trigger
//        public void FinishAnimation() { isAnimationFinished = true; }
//        #endregion
//    }
//}