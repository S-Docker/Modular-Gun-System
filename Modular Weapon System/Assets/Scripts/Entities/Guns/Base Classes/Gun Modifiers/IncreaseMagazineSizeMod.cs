using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Magazine Size Multiplier", menuName = "Gun Mods/IncreaseMagazineSizeMultiplier")]
public class IncreaseMagazineSizeMod : GunModifier
{
    [Tooltip("Magazine size multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] int magazineSizeMultiplier;

    float IncreaseMagazineSize(float currentMagazineSpace){
        return currentMagazineSpace + magazineSizeMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.MagazineSizeMultiplier.AddMod(this.GetInstanceID(), IncreaseMagazineSize);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.MagazineSizeMultiplier.RemoveMod(this.GetInstanceID());
    }
}
