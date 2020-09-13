using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Modifier", menuName = "Modifiers/Gun Modifier", order = 1)]
public class GunModifier : ScriptableObject
{
    [SerializeField] GunModCategory modCategory;
    [SerializeField] GunModType modType;
    [SerializeField] float modValue;

    public GunModCategory GetModCategory(){
        return modCategory;
    }

    public GunModType GetModType() {
        return modType;
    }

    public float GetModValue(){
        return modValue;
    }
}
