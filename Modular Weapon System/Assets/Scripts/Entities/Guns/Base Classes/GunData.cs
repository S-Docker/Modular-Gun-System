using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 0)]
public class GunData : ScriptableObject {
// Remove value is never used warning from inspector
#pragma warning disable 0649
    [Header("Base Gun Settings")]
    [SerializeField] FireMode fireMode; public FireMode FireMode => fireMode;
    [SerializeField] GameObject projectilePrefab; public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] AmmoCategory ammoCategory; public AmmoCategory AmmoCategory => ammoCategory;

    [Header("Base Gun Values")]
    [Tooltip("Guns base damage as a value before modifiers.")]
    [SerializeField][MinAttribute(0)] int damage; public int Damage => damage;

    [Tooltip("Guns magazine size as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] int magazineSize; public int MagazineSize => magazineSize;

    [Tooltip("Guns RPM as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] int roundsPerMinute; public int RoundsPerMinute => roundsPerMinute;
    
    [Tooltip("The random deviation of bullets within a set radius before modifiers")]
    [SerializeField][MinAttribute(0)] float baseSpreadRadius = 0; 

    [Tooltip("Guns crit chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] int baseCritChance;

    [Tooltip("Guns crit multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField][MinAttribute(1)] float baseCritMultiplier;

    [Tooltip("Guns stun chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] int baseStunChance;

#pragma warning restore 0649
    
    [Header("Modifiable Attributes")]
    ModifiableAttribute<float> damageMultiplier; public ModifiableAttribute<float> DamageMultiplier => damageMultiplier;
    ModifiableAttribute<float> magazineSizeMultiplier; public ModifiableAttribute<float> MagazineSizeMultiplier => magazineSizeMultiplier;
    ModifiableAttribute<float> reloadTimeMultiplier; public ModifiableAttribute<float> ReloadTimeMultiplier => reloadTimeMultiplier;
    ModifiableAttribute<float> critMultiplier; public ModifiableAttribute<float> CritMultiplier => critMultiplier;
    ModifiableAttribute<float> roundsPerMinuteMultiplier; public ModifiableAttribute<float> RoundsPerMinuteMultiplier => roundsPerMinuteMultiplier;
    ModifiableAttribute<float> spreadRadiusModified; public ModifiableAttribute<float> SpreadRadius => spreadRadiusModified;
    ModifiableAttribute<float> spreadRadiusMultiplier; public ModifiableAttribute<float> SpreadRadiusMultiplier => spreadRadiusMultiplier;
    ModifiableAttribute<int> critChance; public ModifiableAttribute<int> CritChance => critChance;
    ModifiableAttribute<int> stunChance; public ModifiableAttribute<int> StunChance => stunChance;
    
    void OnEnable(){
        damageMultiplier = new ModifiableAttribute<float>(1f);
        magazineSizeMultiplier = new ModifiableAttribute<float>(1f);
        reloadTimeMultiplier = new ModifiableAttribute<float>(1f);
        roundsPerMinuteMultiplier = new ModifiableAttribute<float>(1f);
        spreadRadiusModified = new ModifiableAttribute<float>(baseSpreadRadius);
        spreadRadiusMultiplier  = new ModifiableAttribute<float>(1f);
        critChance = new ModifiableAttribute<int>(baseCritChance);
        critMultiplier = new ModifiableAttribute<float>(baseCritMultiplier);
        stunChance = new ModifiableAttribute<int>(baseStunChance);
    }

    /**
     * used to initialise new GunData assets created within the gun creator tool
     */
    public void InitialiseGunData(FireMode fireMode, AmmoCategory ammoCategory, GameObject projectilePrefab, int damage, int magazineSize, int roundsPerMinute, float baseSpreadRadius, int baseCritChance, float baseCritMultiplier, int baseStunChance){
        this.fireMode = fireMode;
        this.ammoCategory = ammoCategory;
        this.projectilePrefab = projectilePrefab;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.roundsPerMinute = roundsPerMinute;
        this.baseSpreadRadius = baseSpreadRadius;
        this.baseCritChance = baseCritChance;
        this.baseCritMultiplier = baseCritMultiplier;
        this.baseStunChance = baseStunChance;
    }
}
