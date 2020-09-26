using UnityEngine;

[CreateAssetMenu(fileName = "New Magazine Size Multiplier", menuName = "Gun Mods/Generic/Magazine Size Multiplier")]
public class MagazineSizeMod : GunModifier
{
    [Tooltip("Magazine size multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] int magazineSizeMultiplier;

    float MagazineSizeMultiplier(float currentMagazineSpace){
        return currentMagazineSpace + magazineSizeMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.MagazineSizeMultiplier.AddMod(this.GetInstanceID(), MagazineSizeMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.MagazineSizeMultiplier.RemoveMod(this.GetInstanceID());
    }
}
