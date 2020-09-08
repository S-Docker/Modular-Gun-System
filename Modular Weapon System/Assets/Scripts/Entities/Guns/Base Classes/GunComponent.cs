using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunComponent : MonoBehaviour
{
    [SerializeField] protected Animation componentAnim;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip componentAudio;

    protected Cooldown cooldown = new Cooldown();

    public abstract void Action(Gun gun);
    
    protected virtual void PlayAnimation(){
        if (componentAnim != null){
            componentAnim.Play();
        }
    }

    protected virtual void PlayAudio(){
        if (componentAudio != null){
            audioSource.clip = componentAudio;
            audioSource.Play();
        }
    }

    protected virtual void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    protected virtual void Update() {
        if (cooldown.IsCooldown){
            cooldown.IncrementCooldownTimer(Time.deltaTime);
        }
    }
}
