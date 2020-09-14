﻿using System.Collections.Generic;
using System;

[System.Serializable]
public class ModifiableAttribute<T>
{
    //Each entry holds a uniqueID and a function that takes this attribute type as a parameter and returns a modified value
    Dictionary<int, Func<T,T>> modifiers;
    private T baseValue;

    public T Value {
        get {
            var modifiedValue = baseValue;

            //For each mod, take the current modifier value and return the updated value
            foreach (var mod in modifiers){
                modifiedValue = mod.Value.Invoke(modifiedValue);
            }

            // if generic is of type int, convert modified value to a float and round up
            Type valueType = typeof(T);
            if (valueType == typeof(int)){
                float tempValue = Convert.ToSingle(modifiedValue);
                Math.Ceiling(tempValue);
                modifiedValue = (T)Convert.ChangeType(tempValue, typeof(T));
            }

            return modifiedValue;
        }
    }

    public ModifiableAttribute(T baseValue){
        modifiers = new Dictionary<int, Func<T, T>>();
        this.baseValue = baseValue;
    }

    public void AddMod(int modID, Func<T, T> func){
        if (!modifiers.ContainsKey(modID)){
            modifiers.Add(modID, func);
        }
    }
    public void RemoveMod(int modID){ 
        modifiers.Remove(modID);
    }

    public override string ToString() => Value.ToString();
}