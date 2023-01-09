using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Pig : BehaviourTreeRunner, ICharacterController
    {
        public Transform[] waypoints;

        public EnemyData data;

        public Core Core { get; private set; }
        public Blackboard Blackboard { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public Transform AttackTransform { get; private set; }

        public Vector2[] ActiveArea { get; private set; }

        private Transform ledgeCheck;

        private Wait wait;
        private MoveToProjectile moveToProjectile;
        private PickProjectile pickProjectile;

        private void Awake()
        {
            Core = GetComponentInChildren<Core>();
            Blackboard = new Blackboard();
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            AttackTransform = transform.GetChild(0);
            ledgeCheck = transform.GetChild(4);
            Core.isFacingRight = false;

            SetActiveArea();
            CreateNodeInstances();
        }

        private void CreateNodeInstances()
        {
            wait = new Wait();
            pickProjectile = new PickProjectile(this);
            moveToProjectile = new MoveToProjectile(this);
        }

        protected override Node SetupTree()
        {
            return new Repeat(new InterruptSelector(new List<Node>
            {
                new Sequencer(new List<Node>()
                {
                    new MoveOutOfExplosionRange(this),
                    new CheckFOVRange(this),
                    new Selector(new List<Node>()
                    {
                        new Sequencer(new List<Node>()
                        {
                            new CheckThrowRange(this),
                            new Wait(0.4f),
                            new ThrowProjectile(this),
                            wait
                        }),
                        new Sequencer(new List<Node>()
                        {
                            new CheckMeleeRange(this),
                            new MeleeAttack(this),
                        }),
                        new Sequencer(new List<Node>()
                        {
                            new CheckIfBombCloserThanTarget(this),
                            moveToProjectile,
                            pickProjectile,
                            wait
                        }),
                        new RandomSelector(new List<Node>
                        {
                            new Charge(this),
                            new Sequencer(new List<Node>()
                            {
                                new CheckIfBoxCloserThanTarget(this),
                                moveToProjectile,
                                pickProjectile,
                                wait,
                            })
                        }),
                        new Stare(this),
                    }),
                }),
                new RandomSelector(new List<Node>()
                {
                    new Sequencer(new List<Node>()
                    {
                        new CheckBombNearby(this),
                        moveToProjectile,
                        pickProjectile,
                        wait
                    }),
                    new Sequencer(new List<Node>()
                    {
                        new CheckBoxNearby(this),
                        moveToProjectile,
                        pickProjectile,
                        wait
                    }),
                    new Selector(new List<Node>()
                    {
                        new Sequencer(new List<Node>()
                        {
                            new MoveToRandomPosition(this),
                            new Idle(this),
                        }),
                        new TurnAround(this),
                    }),
                }),
            }));
        }

        protected override void Update()
        {
            base.Update();

            if (Mathf.Abs(transform.position.y - ActiveArea[0].y) > 1f)
                ChangeActiveArea();

            Animator.SetFloat("XVelocity", Mathf.Abs(Rigidbody.velocity.x));
        }

        private void ChangeActiveArea()
        {
            Collider2D otherPig = Physics2D.OverlapCircle(transform.position, data.findNewAreaRadius, 1 << gameObject.layer);
            if (otherPig)
                ActiveArea = otherPig.GetComponent<Pig>().ActiveArea;
        }

        private void SetActiveArea()
        {
            ActiveArea = new Vector2[2];

            ActiveArea[0] = waypoints[0].position;
            ActiveArea[0].y = transform.position.y;

            ActiveArea[1] = waypoints[1].position;
            ActiveArea[1].y = transform.position.y;
        }

        public void MoveToPosition(Vector2 position, float speed)
        {
            FacingToPosition(position);
            float moveDirection = transform.position.x - position.x > 0f ? -1f : 1f;
            Core.SetVelocityX(speed * moveDirection);
        }

        public void FacingToPosition(Vector2 position)
        {
            bool isFacingRight = Core.isFacingRight;
            if (isFacingRight && transform.position.x - position.x > 0f)
                Core.Flip();
            if (!isFacingRight && transform.position.x - position.x < 0f)
                Core.Flip();
        }

        #region Animation trigger
        private void TriggerAttack()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackTransform.position, data.meleeRadius);
            foreach (Collider2D c in colliders)
            {
                if (c.CompareTag("Pig") || c.CompareTag("Box"))
                    continue;

                IDamageable damageable = c.GetComponent<IDamageable>();
                IKnockbackable knockbackable = c.GetComponent<IKnockbackable>();

                if (damageable != null)
                    damageable.TakeDamage(data.damage);

                if (knockbackable != null)
                {
                    Vector2 kbAngle = c.transform.position - transform.position;
                    knockbackable.Knockback(data.knockbackStrength, kbAngle);
                }
            }
        }
        #endregion

        public bool IsDetectedLedge() => !Physics2D.Raycast(ledgeCheck.position, Vector2.down, data.ledgeCheckDistance, Core.groundLayer);

        public void Die() { this.enabled = false; }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

    }
}
