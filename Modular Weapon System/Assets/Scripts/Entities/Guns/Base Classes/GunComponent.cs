using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunComponent : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip componentAudio;

    protected Cooldown cooldown = new Cooldown();

    public abstract void Action(Gun gun, GunData gunData);

    protected virtual void PlayAudio(){
        if (componentAudio != null){
            audioSource.clip = componentAudio;
            audioSource.Play();
        }
    }

    protected virtual void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    
    protected virtual void Update() {
        if (cooldown.IsCooldown){
            cooldown.IncrementCooldownTimer(Time.deltaTime);
        }
    }
}
