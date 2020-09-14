using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Game Data/Gun Data", order = 1)]
public class GunData : ScriptableObject {
    [Header("Base Gun Settings")]
    [SerializeField] private FireMode fireMode = default; public FireMode FireMode => fireMode;
    [SerializeField] private GameObject projectilePrefab = default; public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] private AmmoCategory ammoCategory = default; public AmmoCategory AmmoCategory => ammoCategory;

    [Header("Base Gun Values")]
    [Tooltip("Guns base damage as a value before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseDamage;

    [Tooltip("Guns magazine size as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int baseMagazineSize;

    [Tooltip("Guns RPM as a value before modifiers.")]
    [SerializeField][MinAttribute(1)] private int baseRoundsPerMinute;

    [Tooltip("Guns crit chance as a percentage before modifiers.")]
    [SerializeField][MinAttribute(0)] private int baseCritChance;

    [Header("Modifiable Attributes")]
    private ModifiableAttribute<int> damage; public int Damage => damage.Value;
    private ModifiableAttribute<int> magazineSize; public int MagazineSize => magazineSize.Value;
    private ModifiableAttribute<int> roundsPerMinute; public int RoundsPerMinute => roundsPerMinute.Value;
    private ModifiableAttribute<int> critChance; public int CritChance => critChance.Value;


    void Awake(){
        damage = new ModifiableAttribute<int>(baseDamage);
        magazineSize = new ModifiableAttribute<int>(baseMagazineSize);
        roundsPerMinute = new ModifiableAttribute<int>(baseRoundsPerMinute);
        critChance = new ModifiableAttribute<int>(baseCritChance);
    }
}
