using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 1)]
public class GunData : ScriptableObject {
    //[SerializeField] private FireMode fireMode = default; public FireMode FireMode => fireMode;
    [SerializeField] private GameObject bulletPrefab = default; public GameObject BulletPrefab => bulletPrefab;
    [SerializeField] private AmmoCategory ammoCategory = default; public AmmoCategory AmmoCategory => ammoCategory;
    [SerializeField] private int baseDamage = default; public int BaseDamage => baseDamage;
    [SerializeField] private int magazineSize = default; public int MagazineSize => magazineSize;
    [SerializeField] private float reloadTime = default; public float ReloadTime => reloadTime;
    [SerializeField] private int roundsPerMinute = default; public int RoundsPerMinute => roundsPerMinute;
    [SerializeField] private int maxBulletTravel = default; public int MaxBulletTravel => maxBulletTravel;
}
