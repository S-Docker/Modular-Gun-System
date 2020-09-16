using UnityEngine;
using System.Collections.Generic;

public delegate void OnGunAction(Gun target);

[RequireComponent(typeof(Animator))]
public abstract class Gun : MonoBehaviour, IEquippable, IModdable<GunModifier>
{
    Animator animator; public Animator Animator => animator;

    [Header("Gun Modifiers")]
    [SerializeField] List<GunModifier> mods; 

    [Header("Gun Object Settings")]
    [Tooltip("Assign a GameObject to represent the gun muzzle position of the gun model.")]
    [SerializeField] GameObject gunMuzzlePosition; public GameObject GunMuzzlePosition => gunMuzzlePosition;

    [Header("Gun Data")]
    [Tooltip("Assign a GunData ScriptableObject containing the default gun values and behaviours")]
    [SerializeField] GunData gunData; public GunData GunData => gunData;

    [Tooltip("Number of bullets contained inside the gun magazine at creation.")]
    [SerializeField] int bulletsInMagazine; public int BulletsInMagazine => bulletsInMagazine;

    [Header("Gun Components")]
    [SerializeField] GunFireComponent fireComponent; public GunFireComponent FireComponent => fireComponent;
    [SerializeField] GunReloadComponent reloadComponent; public GunReloadComponent ReloadComponent => reloadComponent;
    [SerializeField] GunAbilityComponent abilityComponent; public GunAbilityComponent AbilityComponent => abilityComponent;

    [Header("Gun Action Delegates")]
    public OnGunAction onUpdate;

    protected void Start(){
        // Make instance of gun data so runtime changes are unique per-gun application
        gunData = Instantiate(gunData);

        InitializeAttachedMods();

        animator = GetComponent<Animator>();
    }

    protected void Update(){
        onUpdate?.Invoke(this);
    }

    public void OnEquipped(){

    }

    public void OnUnequipped(){
        animator.SetTrigger("IsUnequipped");
        reloadComponent.StopReload();
    }  

    public void Fire(){
        if (bulletsInMagazine > 0 && !reloadComponent.IsReloading()){
            fireComponent.Action(this, gunData);
        }
    }

    public void Ability(){
        abilityComponent.Action(this, gunData);
    }

    public void Reload(){
        if (bulletsInMagazine < gunData.MagazineSize){
            reloadComponent.Action(this, gunData);
        }
    }

    public void IncreaseMagazine(int amount){
        bulletsInMagazine += amount;
    }

    public void DecrementMagazine(){
        bulletsInMagazine--;
    }

    protected void InitializeAttachedMods(){
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

    public void AddMod(GunModifier mod){
        if (!mods.Contains(mod)){
            mods.Add(mod);
            mod.ApplyTo(this);
        }
    }

    public void RemoveMod(GunModifier mod){
        mods.Remove(mod);
        mod.RemoveFrom(this);
    }
}
