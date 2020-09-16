using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Game Data/Projectile Data", order = 2)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private float projectileSpeed = default; public float ProjectileSpeed => projectileSpeed;
    [SerializeField] private float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;
    [SerializeField] private bool hasProjectileEffect = false; public bool HasProjectileEffect => hasProjectileEffect;
    [SerializeField] private ProjectileEffect projectileEffect = null; public ProjectileEffect ProjectileEffect => projectileEffect;
}
