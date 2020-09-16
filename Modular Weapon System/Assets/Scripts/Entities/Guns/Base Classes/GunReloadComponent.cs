using System.Collections;
using UnityEngine;

public abstract class GunReloadComponent : GunComponent
{
    [Header("Reload Settings")]
    [Tooltip("the reload anim used for gun.")]
    [SerializeField] protected AnimationClip reloadAnimClip;
    protected float reloadTime;
    protected Coroutine reloadCoroutine;

    [SerializeField] protected AmmoStorage playerAmmoStorage;

    [Header("Reload Delegates")]
    public OnGunAction onReload;

    protected override void Start()
    {
        base.Start();
        reloadTime = reloadAnimClip.length;
    }

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        AmmoCategory category = gunData.AmmoCategory;
        int availableAmmo = playerAmmoStorage.GetAmmoAmount(category);

        if (availableAmmo > 0){
            float reloadMultiplier = gunData.ReloadTimeMultiplier.Value;
            float reloadTimeAdjusted = this.reloadTime / reloadMultiplier;

            animator.SetFloat("reloadTimeMultiplier", reloadMultiplier);

            cooldown.StartCooldownTimer(reloadTimeAdjusted);
            reloadCoroutine = StartCoroutine(ReloadGun(gun, gunData, category, availableAmmo, reloadTimeAdjusted));
        }
    }

    IEnumerator ReloadGun(Gun gun, GunData gunData, AmmoCategory category, int availableAmmo, float reloadTime){
        PlayAudio();
        animator.SetTrigger("IsReload");
        yield return new WaitForSeconds(reloadTime); // do not reload gun until it has been completed

        int magazineSizeAdjusted = (int)Mathf.Ceil(gunData.MagazineSize * gunData.MagazineSizeMultiplier.Value);

        int maximumReloadAmount = Mathf.Min(availableAmmo, magazineSizeAdjusted - gun.BulletsInMagazine); // maximum amount should never exceed magazine size
        gun.IncreaseMagazine(maximumReloadAmount);
        playerAmmoStorage.ReduceAmmoAmount(category, maximumReloadAmount);
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
}
