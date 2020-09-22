using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Game Data/Projectile Data", order = 2)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private float projectileSpeed = default; public float ProjectileSpeed => projectileSpeed;
    [SerializeField] private float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;
    [SerializeField] private bool hasOnHitEffect = false; public bool HasOnHitEffect => hasOnHitEffect;
    [SerializeField] private GameObject onHitEffect = null; public GameObject OnHitEffect => onHitEffect;
}
