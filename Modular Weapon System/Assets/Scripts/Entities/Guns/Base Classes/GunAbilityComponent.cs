using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunAbilityComponent : MonoBehaviour, IGunComponent
{
    [SerializeField] protected Animation abilityAnim;
    [SerializeField] protected float abilityCooldownTimer;
    protected Cooldown cooldown;
    
    void Start() {
        cooldown = new Cooldown();
    }

    public void Action(Gun gun){
        
    }
}
