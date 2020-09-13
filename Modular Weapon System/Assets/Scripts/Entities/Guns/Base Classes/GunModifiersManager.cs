using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Modifier Manager", menuName = "Modifier Managers/Gun Modifier Manager", order = 1)]
public class GunModifiersManager : ScriptableObject
{
    [SerializeField] GunModifier[] startingModifiers;

    List<GunModifier> modifiers;

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
        GunModType modType = mod.ModifierType();
    }

    public float GetModifierValueByType(GunModType type){
        float value = 0;

        foreach (GunModifier mod in modifiers){
            GunModType modType = mod.ModifierType();

            if (modType == type){
                value += mod.ModifierValue();
                // switch case using Op enum to allow for negative traits
            }
        }
    return value;
    }
}
