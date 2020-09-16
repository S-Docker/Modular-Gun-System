﻿using System.Collections;
using UnityEngine;

public abstract class GunReloadComponent : GunComponent
{
    [Header("Reload Settings")]
    [Tooltip("the reload anim used for gun.")]
    [SerializeField] protected AnimationClip reloadAnimClip;
    protected float reloadTime;
    protected Coroutine reloadCoroutine;

    [SerializeField] protected AmmoStorage playerAmmoStorage;

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
            float reloadTime = this.reloadTime * reloadMultiplier;

            animator.SetFloat("reloadTimeMultiplier", reloadMultiplier);

            cooldown.StartCooldownTimer(reloadTime);
            reloadCoroutine = StartCoroutine(ReloadGun(gun, gunData, category, availableAmmo, reloadTime));
        }
    }

    IEnumerator ReloadGun(Gun gun, GunData gunData, AmmoCategory category, int availableAmmo, float reloadTime){
        PlayAudio();
        animator.SetTrigger("IsReload");
        yield return new WaitForSeconds(reloadTime); // do not reload gun until it has been completed

        int maximumReloadAmount = Mathf.Min(availableAmmo, gunData.MagazineSize.Value - gun.BulletsInMagazine); // maximum amount should never exceed magazine size
        gun.IncreaseMagazine(maximumReloadAmount);
        playerAmmoStorage.ReduceAmmoAmount(category, maximumReloadAmount);
        reloadCoroutine = null;
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
