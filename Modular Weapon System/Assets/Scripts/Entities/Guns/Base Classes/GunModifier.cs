using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Modifier", menuName = "Modifiers/Gun Modifier", order = 1)]
public class GunModifier : ScriptableObject
{
    [SerializeField] GunModType modType;
    [SerializeField] float modValue;

    public GunModType ModifierType(){
        return modType;
    }

    public float ModifierValue(){
        return modValue;
    }
}
