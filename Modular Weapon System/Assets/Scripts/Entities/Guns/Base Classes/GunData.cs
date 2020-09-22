using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 0)]
public class GunData : ScriptableObject {
// Remove value is never used warning from inspector
#pragma warning disable 0649
    [Header("Base Gun Settings")]
    [SerializeField] private FireMode fireMode; public FireMode FireMode => fireMode;
    [SerializeField] private GameObject projectilePrefab; public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] private AmmoCategory ammoCategory; public AmmoCategory AmmoCategory => ammoCategory;

    [Header("Base Gun Values")]
    [Tooltip("Guns base damage as a value before modifiers.")]
    [SerializeField][MinAttribute(0)] private int damage; public int Damage => damage;

    [Tooltip("Guns magazine size as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int magazineSize; public int MagazineSize => magazineSize;

    [Tooltip("Guns RPM as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int roundsPerMinute; public int RoundsPerMinute => roundsPerMinute;

    [Tooltip("Guns crit chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseCritChance;

    [Tooltip("Guns crit multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField][MinAttribute(1)] private float baseCritMultiplier;

    [Tooltip("Guns stun chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseStunChance;
#pragma warning restore 0649
    
    [Header("Modifiable Attributes")]
    private ModifiableAttribute<float> damageMultiplier; public ModifiableAttribute<float> DamageMultiplier => damageMultiplier;
    private ModifiableAttribute<float> magazineSizeMultiplier; public ModifiableAttribute<float> MagazineSizeMultiplier => magazineSizeMultiplier;
    private ModifiableAttribute<float> reloadTimeMultiplier; public ModifiableAttribute<float> ReloadTimeMultiplier => reloadTimeMultiplier;
    private ModifiableAttribute<float> critMultiplier; public ModifiableAttribute<float> CritMultiplier => critMultiplier;
    private ModifiableAttribute<float> roundsPerMinuteMultiplier; public ModifiableAttribute<float> RoundsPerMinuteMultiplier => roundsPerMinuteMultiplier;
    private ModifiableAttribute<int> critChance; public ModifiableAttribute<int> CritChance => critChance;
    private ModifiableAttribute<int> stunChance; public ModifiableAttribute<int> StunChance => stunChance;


    void Awake(){
        damageMultiplier = new ModifiableAttribute<float>(1f);
        magazineSizeMultiplier = new ModifiableAttribute<float>(1f);
        reloadTimeMultiplier = new ModifiableAttribute<float>(1f);
        roundsPerMinuteMultiplier = new ModifiableAttribute<float>(1f);
        critChance = new ModifiableAttribute<int>(baseCritChance);
        critMultiplier = new ModifiableAttribute<float>(baseCritMultiplier);
        stunChance = new ModifiableAttribute<int>(baseStunChance);
    }
}
