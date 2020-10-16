using UnityEngine;
using System.Collections.Generic;

public delegate void OnGunAction(Gun target);

[DisallowMultipleComponent][RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour, IEquippable, IModdable<GunModifier>
{
    Animator animator;
    
// Remove value is never used warning from inspector
#pragma warning disable 0649
    [Header("Player Ammo Storage Script")]
    [SerializeField] AmmoStorage playerAmmoStorage; public AmmoStorage PlayerAmmoStorage => playerAmmoStorage;

    [Header("Gun Modifiers")]
    [SerializeField] List<GunModifier> mods; 

    [Header("Gun Data")]
    [Tooltip("Assign a GunData ScriptableObject containing the default gun values and behaviours")]
    [SerializeField] GunData gunData; public GunData GunData => gunData;

    [Header("Gun Components")]
    [SerializeField] GunFireComponent fireComponent; public GunFireComponent FireComponent => fireComponent;
    [SerializeField] GunReloadComponent reloadComponent; public GunReloadComponent ReloadComponent => reloadComponent;
    
    [Header("Gun Ability")]
    [SerializeField] GunAbility ability;

    [Header("Gun Object Settings")]
    [Tooltip("Assign a GameObject to represent the gun muzzle position of the gun model.")]
    [SerializeField] GameObject gunMuzzlePosition; public GameObject GunMuzzlePosition => gunMuzzlePosition;

    [Tooltip("Number of bullets contained inside the gun magazine at creation.")]
    [SerializeField] int bulletsInMagazine; public int BulletsInMagazine => bulletsInMagazine;
#pragma warning restore 0649
    
    [Header("Gun Action Delegates")]
    public OnGunAction onUpdate;
    public OnGunAction onEquip;
    public OnGunAction onUnequip;

    void Start(){
        // Make instance of gun data so runtime changes are unique per-gun application
        gunData = Instantiate(gunData);
        InitializeAttachedMods();
        animator = GetComponent<Animator>();
    }

    void Update(){
        onUpdate?.Invoke(this);
    }

    public void OnEquipped(){
        gameObject.SetActive(true);
        onEquip?.Invoke(this);
    }

    public void OnUnequipped(){
        animator.SetTrigger("IsUnequipped");
        reloadComponent.StopReload();
        onUnequip?.Invoke(this);
    }  

    public void Fire(){
        if (reloadComponent.IsReloading()) return;
        
        if (bulletsInMagazine > 0){
            fireComponent.Action(this, gunData);
        }
    }
    
    public void Reload(){
        int magazineSizeAdjusted = (int)Mathf.Ceil(gunData.MagazineSize * gunData.MagazineSizeMultiplier.Value);
        
        if (bulletsInMagazine < magazineSizeAdjusted){
            reloadComponent.Action(this, gunData);
        }
    }

    public void Ability(){
        if (reloadComponent.IsReloading()) return;
        if (ability == null) return;
        
        ability.Action(this, gunData);
    }

    public void IncreaseMagazine(int amount){
        bulletsInMagazine += amount;
    }

    public void DecrementMagazine(){
        bulletsInMagazine--;
    }
    
    public void EnableCrosshair(GameObject crosshair){
        crosshair.SetActive(true);
        RectTransform[] children = crosshair.GetComponentsInChildren<RectTransform>();
        float spreadValue = gunData.SpreadRadius.Value * gunData.SpreadRadiusModifier.Value;

        // 0 is parent and 1 is center that should not be offset
        for (int i = 2; i < children.Length; i++){
            // radius * 20 gives a fair visual representation of spread
            children[i].localPosition += children[i].up * (spreadValue * 20);
        }
    }

    /**
     * used to initialise new gun prefabs created within the gun creator tool
     */
    public void InitializeGun(GunData gunData, GunAbility ability){
        fireComponent = GetComponent<GunFireComponent>();
        reloadComponent = GetComponent<GunReloadComponent>();
        this.gunData = gunData;

        if (ability == null) return;
        this.ability = ability;
    }

    public void AddMod(GunModifier mod){
        if (mods.Contains(mod)) return;
        mods.Add(mod);
        mod.ApplyTo(this);
    }

    public void RemoveMod(GunModifier mod){
        mods.Remove(mod);
        mod.RemoveFrom(this);
    }

    void InitializeAttachedMods(){
        if(mods == null) {
            mods = new List<GunModifier>(); 
        } else {
            for(int i = 0; i < mods.Count; i++){
                // Make instance of mod so runtime changes are unique per-mod application
                mods[i] = ScriptableObject.Instantiate<GunModifier>(mods[i]);
                mods[i].ApplyTo(this);
            }
        }
    }
}
