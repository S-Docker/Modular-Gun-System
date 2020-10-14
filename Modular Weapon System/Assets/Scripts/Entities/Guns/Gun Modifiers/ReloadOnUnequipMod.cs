using UnityEngine;

[CreateAssetMenu(fileName = "New Reload On Unequip Modifier", menuName = "Gun Mods/Reload On Unequip")]
public class ReloadOnUnequipMod : GunModifier
{
    float elapsedTime;
    [SerializeField] float maxTimeBetweenBulletReloadInSeconds;

    void OnUpdate(Gun target){
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= maxTimeBetweenBulletReloadInSeconds){
            AmmoCategory category = target.GunData.AmmoCategory;
            int availableAmmo = target.PlayerAmmoStorage.GetAmmoAmount(category);

            if (availableAmmo > 0){
                int magazineSizeAdjusted = (int)Mathf.Ceil(target.GunData.MagazineSize * target.GunData.MagazineSizeMultiplier.Value);

                if (target.BulletsInMagazine < magazineSizeAdjusted){
                    target.IncreaseMagazine(1);
                    target.PlayerAmmoStorage.ReduceAmmoAmount(category, 1);
                }

                elapsedTime = 0f;
            }
        }
    }

    void OnEquip(Gun target){
        target.onUpdate -= OnUpdate;
    }

    void OnUnequip(Gun target){
        elapsedTime = 0f;
        target.onUpdate += OnUpdate;
    }

    public override void ApplyTo(Gun target){
        target.onEquip += OnEquip;
        target.onUnequip += OnUnequip;
    }

    public override void RemoveFrom(Gun target){
        target.onEquip -= OnEquip;
        target.onUnequip -= OnUnequip;
        target.onUpdate -= OnUpdate;
    }
}
