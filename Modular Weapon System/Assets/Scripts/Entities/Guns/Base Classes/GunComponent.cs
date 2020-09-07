using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunComponent : MonoBehaviour
{
    [SerializeField] protected Animation componentAnim;
    [SerializeField] protected AudioClip componentAudio;

    protected Cooldown cooldown = new Cooldown();

    public abstract void Action(Gun gun);
    
    protected virtual void PlayAnimation(){
        if (componentAnim != null){

        }
    }

    protected virtual void PlayAudio(){
        if (componentAudio != null){
            AudioSource.PlayClipAtPoint(componentAudio, this.transform.position);
        }
    }
    
    protected virtual void Update() {
        if (cooldown.IsCooldown){
            cooldown.IncrementCooldownTimer(Time.deltaTime);
        }
    }
}
