using UnityEngine;

public class Core : MonoBehaviour, IKnockbackable, IDamageable
{
    public int maxHP;
    private int currentHP;

    public float groundCheckRadius;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Transform Dad { get; private set; }
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D _collider;
    private AudioSource audioSource;

    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool isKnockbacking;

    private Vector2 vector2;
    private bool canSetVelocity;

    private void Awake()
    {
        Dad = transform.parent;
        rb = Dad.GetComponent<Rigidbody2D>();
        animator = Dad.GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        canSetVelocity = true;
        currentHP = maxHP;
    }

    private void Update()
    {
        CheckKnockback();
    }

    public void Flip()
    {
        if (canSetVelocity)
        {
            isFacingRight = !isFacingRight;
            Dad.Rotate(0f, 180f, 0f);
        }
    }

    #region Set velocity
    public void SetVelocityX(float x)
    {
        vector2 = rb.velocity;
        vector2.x = x;
        SetFinalVelocity();
    }

    public void SetVelocity(float x, float y)
    {
        vector2.Set(x, y);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (canSetVelocity)
            rb.velocity = vector2;
    }
    #endregion

    #region Damage and Knockback
    public void TakeDamage(int amount)
    {
        if (isKnockbacking) return;

        currentHP -= amount;
        if (currentHP <= 0)
            animator.SetBool("Die", true);

        animator.SetTrigger("Hit");
        audioSource.Play();
        CameraController.instance.StartShake(0.12f, 0.1f);
    }

    public void Knockback(float strength, Vector2 angle)
    {
        angle.y += 0.5f;
        angle.Normalize();
        SetVelocity(angle.x * strength, angle.y * strength);
        canSetVelocity = false;
        isKnockbacking = true;
        animator.SetBool("Grounded", false);
    }

    private void CheckKnockback()
    {
        if (isKnockbacking && rb.velocity.y < 0.01f && IsTouchingGround())
        {
            if (currentHP <= 0)
            {
                Die();
                return;
            }
            isKnockbacking = false;
            canSetVelocity = true;
            animator.SetBool("Grounded", true);
        }
    }

    private void Die()
    {
        Dad.GetComponent<ICharacterController>().Die();
        rb.simulated = false;
        _collider.enabled = false;
        Dad.GetComponent<SpriteRenderer>().sortingLayerName = "Dead";
    }
    #endregion

    public bool IsTouchingGround() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
