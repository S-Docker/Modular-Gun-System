using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Magazine Size", menuName = "Gun Mods/IncreaseMagazineSize")]
public class IncreaseMagazineSizeMod : GunModifier
{
    [Tooltip("Amount to increase magazine size by.")]
    [SerializeField] int additionalMagazineSpace;

    int IncreaseMagazineSize(int currentMagazineSpace){
        return currentMagazineSpace + additionalMagazineSpace;
    }

    public override void ApplyTo(Gun target){
        target.GunData.MagazineSize.AddMod(this.GetInstanceID(), IncreaseMagazineSize);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.MagazineSize.RemoveMod(this.GetInstanceID());
    }
}
