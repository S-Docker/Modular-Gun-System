using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Recoil Modifier", menuName = "Gun Mods/Generic/Gun Recoil Modifier")]
public class GunRecoilMod : GunModifier
{
    [Header("Transform information")]
    Transform gunHolderTransform;
    Quaternion originalRotation; // default gun rotation
    
    [Header("Quaternions used for recoil calculations")]
    Quaternion startingRotation; // Rotation before applying new recoil
    Quaternion targetRotation; // Target rotation after full recoil application
    Quaternion currentRotation; // rotation after each lerped recoil application

    [Header("Rotation Reset Timers")]
    [Tooltip("Should gun reset transform back to original position after delay period.")]
    [SerializeField] bool canResetTransform = false;
    [Tooltip("Wait time before transform resets back to original rotation after player stops shooting.")]
    [SerializeField][MinAttribute(0)] float transformResetDelay;
    [Tooltip("Total time in seconds taken to reset gun from current rotation to its original rotation.")]
    [SerializeField][MinAttribute(0)] float transformResetDuration;
    float elapsedTimeSinceShot;
    float elapsedTimeSinceResetStart;

    [Header("Recoil Settings")]
    [Tooltip("Horizontal recoil amount in degrees.")]
    [SerializeField][MinAttribute(0)] float horizontalRecoilAmount;
    [Tooltip("Vertical recoil amount in degrees.")]
    [SerializeField][MinAttribute(0)] float verticalRecoilAmount;
    [Tooltip("How quick the gun goes from current rotation to target rotation after shooting.")]
    [SerializeField][MinAttribute(0)] float recoilSpeed;

    void CalculateRecoil(Gun target){
        elapsedTimeSinceShot = 0;
        elapsedTimeSinceResetStart = 0;

        startingRotation = gunHolderTransform.rotation;
        Vector3 targetRecoilAmount = new Vector3(-horizontalRecoilAmount, Random.Range(-verticalRecoilAmount, verticalRecoilAmount), 0);
        targetRotation = startingRotation * Quaternion.Euler(targetRecoilAmount);
    }

    void OnUpdate(Gun target){
        // if still in recoil stage
        if (elapsedTimeSinceShot < transformResetDelay){
            gunHolderTransform.rotation = Quaternion.Slerp(startingRotation, targetRotation, recoilSpeed * elapsedTimeSinceShot);

            currentRotation = gunHolderTransform.rotation;
            elapsedTimeSinceShot += Time.deltaTime;
        }
        else{
            if (currentRotation == originalRotation) return;

            if (canResetTransform){
                ResetTransform();
            }
        }
    }

    void ResetTransform(){
        elapsedTimeSinceResetStart += Time.deltaTime;

        if (elapsedTimeSinceResetStart < transformResetDuration){
            float rotationProgress = elapsedTimeSinceResetStart / transformResetDuration;
            
            gunHolderTransform.rotation = Quaternion.Slerp(currentRotation, originalRotation, rotationProgress);
        }
    }

    public override void ApplyTo(Gun target){
        gunHolderTransform = target.transform.parent;

        originalRotation = gunHolderTransform.rotation;
        currentRotation = originalRotation;
        elapsedTimeSinceShot = transformResetDelay;

        target.FireComponent.onFire += CalculateRecoil;
        target.onUpdate += OnUpdate;
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= CalculateRecoil;
        target.onUpdate -= OnUpdate;
    }
}