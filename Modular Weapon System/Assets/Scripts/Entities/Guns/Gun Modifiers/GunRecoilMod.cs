using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Recoil Modifier", menuName = "Gun Mods/Generic/Gun Recoil modifier")]
public class GunRecoilMod : GunModifier
{
    Transform gunHolderTransform;
    
    Quaternion originalRotation; // default gun rotation
    
    Quaternion startingRotation; // Rotation before applying new recoil
    Quaternion targetRotation; // Target rotation after full recoil application
    Quaternion currentRotation; // rotation after each lerped recoil application
    
    float elapsedTimeSinceShot;
    [SerializeField] float waitDurationBeforeReset;

    float elapsedTimeSinceStartReset;
    [SerializeField] float maxTimeToResetTransform;

    [SerializeField] float horizontalRecoilAmount;
    [SerializeField] float verticalRecoilAmount;

    void AddRecoil(Gun target){
        elapsedTimeSinceShot = 0;
        elapsedTimeSinceStartReset = 0;

        startingRotation = gunHolderTransform.rotation;
        Vector3 targetRecoilAmount = new Vector3(-horizontalRecoilAmount, Random.Range(-verticalRecoilAmount, verticalRecoilAmount), 0);
        targetRotation = startingRotation * Quaternion.Euler(targetRecoilAmount);
    }

    void OnUpdate(Gun target){
        // if still in recoil stage
        if (elapsedTimeSinceShot < waitDurationBeforeReset){

            gunHolderTransform.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTimeSinceShot);

            currentRotation = gunHolderTransform.rotation;
            elapsedTimeSinceShot += Time.deltaTime;
        }
        else{
            if (currentRotation == originalRotation) return;

            ResetTransform(target, maxTimeToResetTransform);
        }
    }

    void ResetTransform(Gun target, float timeTakenToReset){
        elapsedTimeSinceStartReset += Time.deltaTime;

        if (elapsedTimeSinceStartReset < timeTakenToReset){
            Debug.Log("rotate here");
            float rotationProgress = elapsedTimeSinceStartReset / Mathf.Min(timeTakenToReset);
            
            gunHolderTransform.rotation = Quaternion.Slerp(currentRotation, originalRotation, rotationProgress);
        }
    }
    
    
    public override void ApplyTo(Gun target){
        gunHolderTransform = target.transform.parent;

        originalRotation = gunHolderTransform.rotation;
        currentRotation = originalRotation;
        elapsedTimeSinceShot = waitDurationBeforeReset;
        
        
        target.FireComponent.onFire += AddRecoil;
        target.onUpdate += OnUpdate;
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= AddRecoil;
        target.onUpdate -= OnUpdate;
    }
}
