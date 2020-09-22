using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Game Data/Projectile Data", order = 2)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private float projectileSpeed = default; public float ProjectileSpeed => projectileSpeed;
    
    [Header("Projectile Lifetime Settings")]
    [SerializeField] private int maxCollisions = 1; public int MaxCollisions => maxCollisions;
    [SerializeField] private float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;
    
    [Header("Projectile Effect Settings")]
    [SerializeField] private bool hasOnHitEffect = false; public bool HasOnHitEffect => hasOnHitEffect;
    [SerializeField] private GameObject onHitEffect = null; public GameObject OnHitEffect => onHitEffect;
}
