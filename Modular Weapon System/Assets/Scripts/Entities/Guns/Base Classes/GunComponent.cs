using UnityEngine;

public abstract class GunComponent : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;

    protected readonly Cooldown cooldown = new Cooldown();

    public abstract void Perform(Gun gun, GunData gunData);

    protected virtual void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    
    protected virtual void Update(){
        if (cooldown.IsCooldown){
            cooldown.IncrementCooldownTimer(Time.deltaTime);
        }
    }
}
