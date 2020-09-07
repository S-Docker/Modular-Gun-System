using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletEffect: MonoBehaviour
{
    public abstract void OnStartEffect();
    public abstract void ContinuousEffect();
    public abstract void OnEndEffect();
}
