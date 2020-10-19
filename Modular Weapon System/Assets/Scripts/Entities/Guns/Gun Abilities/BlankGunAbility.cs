using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Blank Gun Ability", menuName = "Gun Abilities/Blank Gun Ability")]
public class BlankGunAbility : GunAbility
{
    public override void Perform(Gun gun, GunData gunData){
        Debug.Log("Ability used");
    }
}
