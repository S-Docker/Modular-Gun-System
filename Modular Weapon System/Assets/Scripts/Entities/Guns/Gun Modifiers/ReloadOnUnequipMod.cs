using UnityEngine;

[CreateAssetMenu(fileName = "New Reload On Unequip Modifier", menuName = "Gun Mods/ReloadOnUnequip")]
public class ReloadOnUnequipMod : GunModifier
{

    void OnUnequip(Gun target){
        AmmoCategory category = target.GunData.AmmoCategory;
        int availableAmmo = target.PlayerAmmoStorage.GetAmmoAmount(category);

        if (availableAmmo > 0){
            int magazineSizeAdjusted = (int)Mathf.Ceil(target.GunData.MagazineSize * target.GunData.MagazineSizeMultiplier.Value);

            int maximumReloadAmount = Mathf.Min(availableAmmo, magazineSizeAdjusted - target.BulletsInMagazine); // maximum amount should never exceed magazine size
            target.IncreaseMagazine(maximumReloadAmount);
            target.PlayerAmmoStorage.ReduceAmmoAmount(category, maximumReloadAmount);
        }
    }

    public override void ApplyTo(Gun target){
        target.onUnequip += OnUnequip;
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritChance.RemoveMod(this.GetInstanceID());
    }
}
