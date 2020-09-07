using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunReloadComponent : MonoBehaviour, IGunComponent
{
    [SerializeField] protected Animation reloadAnim;

    public void Action(Gun gun){
        
    }
}
