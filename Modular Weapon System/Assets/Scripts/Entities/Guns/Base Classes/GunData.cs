using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 0)]
public class GunData : ScriptableObject {
    [Header("Base Gun Settings")]
    [SerializeField] private FireMode fireMode; public FireMode FireMode => fireMode;
    [SerializeField] private GameObject projectilePrefab; public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] private AmmoCategory ammoCategory; public AmmoCategory AmmoCategory => ammoCategory;

    [Header("Base Gun Values")]
    [Tooltip("Guns base damage as a value before modifiers.")]
    [SerializeField][MinAttribute(0)] private int damage; public int Damage => damage;

    [Tooltip("Guns magazine size as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int baseMagazineSize;

    [Tooltip("Guns reload speed as a value before modifiers.")]
    [SerializeField][MinAttribute(0)] private float reloadTime; public float ReloadTime => reloadTime;

    [Tooltip("Guns RPM as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int baseRoundsPerMinute;

    [Tooltip("Guns crit chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseCritChance;

    [Tooltip("Guns crit multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField][MinAttribute(1)] private float baseCritMultiplier;

    [Tooltip("Guns stun chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseStunChance;

    [Header("Modifiable Attributes")]
    private ModifiableAttribute<float> damageMultiplier; public ModifiableAttribute<float> DamageMultiplier => damageMultiplier;
    private ModifiableAttribute<int> magazineSize; public ModifiableAttribute<int> MagazineSize => magazineSize;
    private ModifiableAttribute<float> reloadTimeMultiplier; public ModifiableAttribute<float> ReloadTimeMultiplier => reloadTimeMultiplier;
    private ModifiableAttribute<int> roundsPerMinute; public ModifiableAttribute<int> RoundsPerMinute => roundsPerMinute;
    private ModifiableAttribute<int> critChance; public ModifiableAttribute<int> CritChance => critChance;
    private ModifiableAttribute<float> critMultiplier; public ModifiableAttribute<float> CritMultiplier => critMultiplier;
    private ModifiableAttribute<int> stunChance; public ModifiableAttribute<int> StunChance => stunChance;


    void Awake(){
        damageMultiplier = new ModifiableAttribute<float>(1f);
        magazineSize = new ModifiableAttribute<int>(baseMagazineSize);
        reloadTimeMultiplier = new ModifiableAttribute<float>(1f);
        roundsPerMinute = new ModifiableAttribute<int>(baseRoundsPerMinute);
        critChance = new ModifiableAttribute<int>(baseCritChance);
        critMultiplier = new ModifiableAttribute<float>(baseCritMultiplier);
        stunChance = new ModifiableAttribute<int>(baseStunChance);
    }
}
