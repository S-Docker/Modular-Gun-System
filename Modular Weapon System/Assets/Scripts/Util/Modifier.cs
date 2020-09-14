using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Modifier<T> : ScriptableObject
{
    public abstract void ApplyTo(T target);
    public abstract void RemoveFrom(T target);
}
