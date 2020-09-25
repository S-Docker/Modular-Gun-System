using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AmmoStorage : MonoBehaviour
{
    [SerializeField] AmmoCategory[] startingAmmoCategories;
    [SerializeField] int[] startingAmmoAmounts;

    Dictionary<AmmoCategory, int> ammoTypeAndAmount;

    void Start(){
        ammoTypeAndAmount = new Dictionary<AmmoCategory, int>();

        for (int i = 0; i < startingAmmoCategories.Length; i++){
            ammoTypeAndAmount.Add(startingAmmoCategories[i], startingAmmoAmounts[i]);
        }
    }

    public int GetAmmoAmount(AmmoCategory category){
        return ammoTypeAndAmount[category];
    }

    public void AddAmmoAmount(AmmoCategory category, int amount){
        ammoTypeAndAmount[category] += amount;
    }

    public void ReduceAmmoAmount(AmmoCategory category, int amount){
        ammoTypeAndAmount[category] -= amount;
    }

    void OnValidate(){
        int[] temp = startingAmmoAmounts;
        InitialiseArraysInInspector();

        if (temp == null) return;
        // needed to stop old values set in inspector from being overwritten on validation
        startingAmmoAmounts = temp;
    }

    /**
    * Used to initialise two equal length arrays based on ammo categories available and set
    * each element in startingAmmoCategories equal to the enums available
    */
    void InitialiseArraysInInspector(){
        string[] ammoCategories = System.Enum.GetNames(typeof(AmmoCategory));
        int categoryCount = ammoCategories.Length;

        startingAmmoCategories = new AmmoCategory[categoryCount];
        startingAmmoAmounts = new int[startingAmmoCategories.Length];

        for (int i = 0; i < categoryCount; i++){
            AmmoCategory type = (AmmoCategory)System.Enum.Parse(typeof(AmmoCategory), ammoCategories[i]);
            startingAmmoCategories[i] = type;
        }
    }
}
