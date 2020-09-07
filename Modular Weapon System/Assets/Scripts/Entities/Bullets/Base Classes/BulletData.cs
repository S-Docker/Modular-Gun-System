using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Game Data/Bullet Data", order = 2)]
public class BulletData : ScriptableObject
{
    [SerializeField] private float bulletSpeed = default; public float BulletSpeed => bulletSpeed;
    [SerializeField] private float maxBulletTravel = default; public float MaxBulletTravel => maxBulletTravel;
    [SerializeField] private bool hasBulletEffect = false; public bool HasBulletEffect => hasBulletEffect;
    [SerializeField] private BulletEffect bulletEffect = null; public BulletEffect BulletEffect => bulletEffect;
}
