using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Gun : MonoBehaviour, IEquippable
{
    [Header("Gun Object Settings")]
    [Tooltip("Assign a GameObject to represent the gun muzzle position of the gun model.")]
    [SerializeField] GameObject gunMuzzlePosition; public GameObject GunMuzzlePosition => gunMuzzlePosition;

    [Header("Gun Data")]
    [Tooltip("Assign a GunData ScriptableObject containing the default gun values and behaviours")]
    [SerializeField] GunData gunData;

    [Tooltip("Number of bullets contained inside the gun magazine at creation.")]
    [SerializeField] int bulletsInMagazine; public int BulletsInMagazine => bulletsInMagazine;

    [Header("Gun Components")]
    [SerializeField] GunFireComponent fire;
    [SerializeField] GunAbilityComponent ability;
    [SerializeField] GunReloadComponent reload;

    void Start(){
        gunData = Instantiate(gunData); // make a copy so any changes made do not persist
    }

    public void OnEquipped(){

    }

    public void OnUnequipped(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("IsUnequipped");
        reload.StopReload();
    }  

    public void Fire(){
        if (bulletsInMagazine > 0 && !reload.IsReloading()){
            fire.Action(this, gunData);
        }
    }

    public void Ability(){
        ability.Action(this, gunData);
    }

    public void Reload(){
        if (bulletsInMagazine < gunData.MagazineSize){
            reload.Action(this, gunData);
        }
    }

    public void IncreaseMagazine(int amount){
        bulletsInMagazine += amount;
    }

    public void DecrementMagazine(){
        bulletsInMagazine--;
    }
}
