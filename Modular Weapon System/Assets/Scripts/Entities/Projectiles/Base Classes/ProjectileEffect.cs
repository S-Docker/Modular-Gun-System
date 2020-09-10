using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEffect : MonoBehaviour
{
    public abstract void OnStartEffect();
    public abstract void ContinuousEffect();
    public abstract void OnEndEffect();
}
