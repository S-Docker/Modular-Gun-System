using UnityEngine;

[CreateAssetMenu(fileName = "New Stun Chance Modifier", menuName = "Gun Mods/Generic/Stun Chance Modifier")]
public class StunChanceMod : GunModifier
{
    [Tooltip("Crit chance as a percentage in value form before modifiers.")]
    [SerializeField] int stunChanceValue;

    int StunChance(int currentValue){
        return currentValue + stunChanceValue;
    }

    public override void ApplyTo(Gun target){
        target.GunData.StunChance.AddMod(this.GetInstanceID(), StunChance);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.StunChance.RemoveMod(this.GetInstanceID());
    }
}
