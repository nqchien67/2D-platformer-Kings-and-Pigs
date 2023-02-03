using UnityEngine;

public class Bomb : MonoBehaviour, IProjectile
{
    public float burnTime = 1f;
    public float explosionRadius;
    public int damage;
    public float kbStrength;

    [Header("Sound effect")]
    public AudioClip fuseBurn;
    public AudioClip explode;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private bool isBurning;

    public float TimeCounter { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isBurning = false;
        TimeCounter = burnTime;
    }

    private void Update()
    {
        if (isBurning)
        {
            transform.Rotate(0, 0, 2.5f);
            if (TimeCounter < 0f)
            {
                Boom();
                isBurning = false;
            }
            TimeCounter -= Time.deltaTime;
        }
    }

    public void Throw(Vector2 targetPosition, Transform thrower)
    {
        Vector2 force = CalculateForce(targetPosition);
        force.x = Random.Range(force.x - 0.5f, force.x + 0.5f);
        force.y = Random.Range(force.y - 0.5f, force.y + 0.5f);
        rb.AddForce(force, ForceMode2D.Impulse);

        isBurning = true;
        ExcludeCollision();

        transform.GetComponent<SpriteRenderer>().sortingLayerName = "Projectile";
        animator.SetTrigger("Active");
    }

    private void ExcludeCollision()
    {
        GetComponent<Collider2D>().excludeLayers = 1 << gameObject.layer | 1 << 9 | 1 << 13;
    }

    private Vector2 CalculateForce(Vector2 targetPosition)
    {
        Vector2 directionVector = targetPosition - (Vector2)transform.position;

        float g = -Physics2D.gravity.y * rb.gravityScale;

        float a = g * burnTime * burnTime;
        float height = Mathf.Pow(a + 2 * directionVector.y, 2) / (8 * a);
        float b = Mathf.Sqrt(2 * g * height);

        float angle = Mathf.Atan(b * burnTime / directionVector.x);
        float v0 = b / Mathf.Sin(angle);

        Vector2 force = new Vector2(v0 * Mathf.Cos(angle), v0 * Mathf.Sin(angle));
        return force;
    }

    private void Boom()
    {
        rb.simulated = false;
        transform.rotation = Quaternion.identity;

        DoDamage();

        animator.SetTrigger("Boom");
        audioSource.clip = explode;
        audioSource.Play();

        CameraController.instance.StartShake(0.5f, 0.4f);
    }

    public void EndBoomAnimationTrigger()
    {
        Destroy(gameObject, 0.8f);
    }

    private void DoDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D c in colliders)
        {
            IDamageable damageable = c.GetComponent<IDamageable>();
            IKnockbackable knockbackable = c.GetComponent<IKnockbackable>();

            if (damageable != null)
                damageable.TakeDamage(damage);

            if (knockbackable != null)
            {
                Vector2 kbAngle = c.transform.position - transform.position;
                knockbackable.Knockback(kbStrength, kbAngle);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
