using TMPro;
using UnityEngine;

public class Box : MonoBehaviour, IKnockbackable, IProjectile
{
    public GameObject[] pieces;
    public int damageAmount;
    public float kbStrength = 8f;
    public Vector2 kbAngle;
    public int knockbackableLayer = 12;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isKnockbacking;
    private Vector2 vectorContactPointToBox;
    private Transform thrower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isKnockbacking = false;
        thrower = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isKnockbacking) return;

        GameObject collidedObject = collision.gameObject;

        bool isObjGround = collidedObject.layer == 6;
        if (isObjGround)
        {
            vectorContactPointToBox = (Vector2)transform.position - collision.contacts[0].point;
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (!isKnockbacking) return;

        if (otherCollider == GetColliderFromThrower(thrower)) return;

        GameObject collidedObject = otherCollider.gameObject;

        if (collidedObject.layer == knockbackableLayer)
        {
            if (collidedObject.TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(damageAmount);

            if (collidedObject.TryGetComponent<IKnockbackable>(out var knockbackable))
            {
                vectorContactPointToBox = transform.position - collidedObject.transform.position;
                float direction = rb.velocity.x > 0 ? 1f : -1f;
                kbAngle.x *= direction;
                knockbackable.Knockback(kbStrength, kbAngle);
            }

            Break();
        }
    }

    private Collider2D GetColliderFromThrower(Transform thrower)
    {
        if (thrower == null) return null;
        Component knockbackable = thrower.GetComponentInChildren<IKnockbackable>() as Component;
        return knockbackable.GetComponent<Collider2D>();
    }

    private void Break()
    {
        rb.simulated = false;
        animator.SetTrigger("Hit");
        GetComponent<AudioSource>().Play();
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        for (int i = 0; i < pieces.Length; i++)
            pieces[i] = Instantiate(pieces[i], transform.position, Quaternion.identity);

        pieces[0].transform.position += new Vector3(-0.17f, -0.13f, 0f);
        pieces[1].transform.position += new Vector3(-0.17f, 0.16f, 0f);
        pieces[2].transform.position += new Vector3(0.16f, 0.21f, 0f);
        pieces[3].transform.position += new Vector3(0.16f, -0.11f, 0f);

        foreach (GameObject piece in pieces)
        {
            Vector2 vectorBoxToPiece = piece.transform.position - transform.position;
            Vector2 bounceBack = vectorContactPointToBox + vectorBoxToPiece;
            piece.GetComponent<Rigidbody2D>().velocity = bounceBack * Random.Range(5f, 10f);
            Destroy(piece, 4f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject, 0.01f);
    }

    public void Knockback(float strength, Vector2 direction)
    {
        direction.y += 0.5f;
        strength *= 2f;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * strength, direction.y * strength);
        isKnockbacking = true;
        ExcludeCollision();
    }

    public void Throw(Vector2 targetPosition, Transform thrower)
    {
        Vector2 force = CalculateForce(targetPosition);
        force *= Random.Range(0.8f, 1.2f);
        rb.AddForce(force, ForceMode2D.Impulse);
        isKnockbacking = true;

        this.thrower = thrower;
        ExcludeCollision();

        transform.GetComponent<SpriteRenderer>().sortingLayerName = "Projectile";
    }

    private void ExcludeCollision()
    {
        GetComponent<Collider2D>().excludeLayers = 1 << gameObject.layer | 1 << 9 | 1 << 11;
    }

    private Vector2 CalculateForce(Vector2 targetPosition)
    {
        Vector2 directionVector = targetPosition - (Vector2)transform.position;

        float height = CalculateHeight(directionVector);

        float g = -Physics2D.gravity.y * rb.gravityScale;
        float xt = directionVector.x;
        float yt = directionVector.y;

        float a = (-0.5f * g);
        float b = Mathf.Sqrt(2 * g * height);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1f);
        float tmin = QuadraticEquation(a, b, c, -1f);
        float time = tplus > tmin ? tplus : tmin;

        float angle = Mathf.Atan(b * time / xt);
        float v0 = b / Mathf.Sin(angle);

        Vector2 force = new Vector2(v0 * Mathf.Cos(angle), v0 * Mathf.Sin(angle));
        return force;
    }

    private float CalculateHeight(Vector2 directionVector)
    {
        float yt = directionVector.y;
        float magnitude = directionVector.magnitude;

        float height;
        if (yt < -0.5f)
            height = -yt / 2f;
        else
            height = yt + magnitude / Random.Range(6, 16);

        return Mathf.Max(0.01f, height);
    }

    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }
}
