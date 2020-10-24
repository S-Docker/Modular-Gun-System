using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class GunReloadComponent : GunComponent
{
    [Header("Reload Settings")]
    [Tooltip("Reload animation used for the selected gun.")]
    [SerializeField] protected AnimationClip reloadAnimClip;
    float reloadTime;
    Coroutine reloadCoroutine;

    [Header("Audio Files")]
    [SerializeField] protected AudioClip startReloadAudio;
    [SerializeField] protected AudioClip endReloadAudio;
    float endReloadAudioLength;
    
    [Header("Reload Delegates")]
    public OnGunAction onReload;

    protected override void Start(){
        base.Start();
        if (reloadAnimClip != null){
            reloadTime = reloadAnimClip.length;
        }

        if (endReloadAudio != null){
            endReloadAudioLength = endReloadAudio.length;
        }
    }

    public override void Perform(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        AmmoCategory category = gunData.AmmoCategory;
        int availableAmmo = gun.PlayerAmmoStorage.GetAmmoAmount(category);

        if (availableAmmo > 0){
            float reloadMultiplier = gunData.ReloadTimeMultiplier.Value;
            float reloadTimeAdjusted = reloadTime * reloadMultiplier;

            animator.SetFloat("reloadTimeMultiplier", reloadMultiplier);

            cooldown.StartCooldownTimer(reloadTimeAdjusted);
            reloadCoroutine = StartCoroutine(ReloadGun(gun, gunData, category, availableAmmo, reloadTimeAdjusted));
        }
    }

    IEnumerator ReloadGun(Gun gun, GunData gunData, AmmoCategory category, int availableAmmo, float reloadTime){
        PlayAudio(startReloadAudio);
        animator.SetTrigger("IsReload");
        
        // Start playing audio x seconds before full reload time where x is the length of the end reload audio length
        yield return new WaitForSeconds(reloadTime - endReloadAudioLength);
        PlayAudio(endReloadAudio);
        
        yield return new WaitForSeconds(endReloadAudioLength);
        
        // round magazine size to highest int and ensure magazine can hold at least 1 bullet after modifiers
        int magazineSizeAdjusted = (int)Mathf.Ceil(gunData.MagazineSize * gunData.MagazineSizeMultiplier.Value);
        magazineSizeAdjusted = Mathf.Max(1, magazineSizeAdjusted);
        
        int maximumReloadAmount = Mathf.Min(availableAmmo, magazineSizeAdjusted - gun.BulletsInMagazine); // maximum amount should never exceed magazine size
        gun.IncreaseMagazine(maximumReloadAmount);
        gun.PlayerAmmoStorage.ReduceAmmoAmount(category, maximumReloadAmount);
        reloadCoroutine = null;

        onReload?.Invoke(gun);
    }

    public bool IsReloading(){
        return cooldown.IsCooldown;
    }

    public void StopReload(){
        if (reloadCoroutine != null){
            cooldown.StopCooldownTimer();
            StopCoroutine(reloadCoroutine);
        }

        if (audioSource.isPlaying){
            audioSource.Stop();
        }
    }

    void PlayAudio(AudioClip audio){
        audioSource.clip = audio;
        audioSource.Play();
    }
}
