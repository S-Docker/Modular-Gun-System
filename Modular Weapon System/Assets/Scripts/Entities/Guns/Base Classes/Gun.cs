using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] GameObject gunHoldPosition;
    [SerializeField] GameObject gunNozzlePosition; public GameObject GunNozzlePosition => gunNozzlePosition;
    [SerializeField] GunData gunData; public GunData GunData => gunData;

    [SerializeField] GunFireComponent fire;
    [SerializeField] GunAbilityComponent ability;
    [SerializeField] GunReloadComponent reload;

    public void Equip(){
        Debug.Log("Picked up");
        this.transform.parent = gunHoldPosition.transform;
        transform.localPosition = Vector3.zero;
    }

    public void Unequip(){
        this.transform.parent = null;
    }

    public void Fire(){
        fire.Action(this);
    }

    public void Ability(){
        ability.Action(this);
    }

    public void Reload(){
        reload.Action(this);
    }
}
