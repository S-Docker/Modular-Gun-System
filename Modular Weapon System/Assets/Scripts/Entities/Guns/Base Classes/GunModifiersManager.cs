using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Modifier Manager", menuName = "Modifier Managers/Gun Modifier Manager", order = 1)]
public class GunModifiersManager : ScriptableObject
{
    [SerializeField] GunModifier[] startingModifiers;

    List<GunModifier> modifiers = new List<GunModifier>();

    void Awake(){
        InitializeModifierLists(startingModifiers);
    }

    void InitializeModifierLists(GunModifier[] mods){
        foreach (GunModifier mod in mods){
            AddModifier(mod);
        }
    }

    public void AddModifier(GunModifier mod){
        modifiers.Add(mod);
    }

    float GetModifierValueByType(GunModType type){
        float value = 0;
        foreach (GunModifier mod in modifiers){
            GunModType modType = mod.GetModType();

            if (modType == type){
                GunModCategory gunModCategory = mod.GetModCategory();

                switch(gunModCategory) {
                    case GunModCategory.Positive: value += mod.GetModValue() ; break;
                    case GunModCategory.Negative: value -= mod.GetModValue() ; break;
                }
            }
        }
    return value;
    }

    public float GetModifierPercentByType(GunModType type){
        float value = GetModifierValueByType(type);
        return 1 + (value / 100);
    }
}
