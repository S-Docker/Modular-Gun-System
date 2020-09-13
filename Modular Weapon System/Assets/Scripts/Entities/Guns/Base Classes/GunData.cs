using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 1)]
public class GunData : ScriptableObject {
    [SerializeField] private GunModifiersManager gunModifiersManager; public GunModifiersManager GunModifiersManager => gunModifiersManager;
    [SerializeField] private FireMode fireMode = default; public FireMode FireMode => fireMode;
    [SerializeField] private GameObject projectilePrefab = default; public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] private AmmoCategory ammoCategory = default; public AmmoCategory AmmoCategory => ammoCategory;
    [SerializeField] private float damage = default; public float Damage => damage;
    [SerializeField] private int magazineSize = default; public int MagazineSize => magazineSize;
    [SerializeField] private int roundsPerMinute = default; public int RoundsPerMinute => roundsPerMinute;

    void Start(){
        gunModifiersManager = Instantiate(gunModifiersManager); // Create local copy
    }
}
