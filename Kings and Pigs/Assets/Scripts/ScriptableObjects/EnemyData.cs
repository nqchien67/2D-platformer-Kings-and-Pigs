using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Range")]
    public float fovRange = 2f;
    public float fovWidth;
    public float closeRange;
    public float findNewAreaRadius;
    public float ledgeCheckDistance = 0.3f;

    [Header("Attack")]
    public float meleeTime;
    public float meleeRadius;
    public int damage;
    public float knockbackStrength;

    [Header("Move")]
    public float walkSpeed = 2f;
    public float sprintSpeed;

    [Header("Patrol")]
    public float minWaitTime = 0.5f;
    public float maxWaitTime = 5f;


    [Header("Projectile")]
    public LayerMask bombLayer;
    public LayerMask boxLayer;
    public float pickRange;
    public float throwRange = 7f;

    [Header("LayerMask")]
    public LayerMask playerLayer = 1 << 3;
}

