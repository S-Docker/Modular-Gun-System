using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Recoil Modifier", menuName = "Gun Mods/Generic/Gun Recoil modifier")]
public class GunRecoilMod : GunModifier
{
    Transform gunHolderTransform;
    
    Quaternion originalRotation; // default gun rotation
    Quaternion endRotation; // rotation after recoil application
    
    float elapsedTimeSinceShot;
    [SerializeField] float waitDurationBeforeReset;

    float elapsedTimeSinceStartReset;
    [SerializeField] float TimeToResetTransform;
    
    Vector3 targetRecoilAmount;

    void AddRecoil(Gun target){
        elapsedTimeSinceShot = 0;
        elapsedTimeSinceStartReset = 0;
        
        targetRecoilAmount = new Vector3(-0.5f, Random.Range(-2.5f, 2.5f), 0);
        gunHolderTransform.Rotate(targetRecoilAmount);
        
        endRotation = gunHolderTransform.rotation;
    }

    void OnUpdate(Gun target){
        // if still in recoil stage
         if (elapsedTimeSinceShot < waitDurationBeforeReset){
             elapsedTimeSinceShot += Time.deltaTime;
        }
        else{
            if (originalRotation == endRotation) return;

            ResetTransform(target, TimeToResetTransform);
        }
    }

    void ResetTransform(Gun target, float timeTakenToReset){
        Debug.Log("Attempting Reset");
        elapsedTimeSinceStartReset += Time.deltaTime;

        if (elapsedTimeSinceStartReset < TimeToResetTransform){
            float rotationProgress = elapsedTimeSinceStartReset / timeTakenToReset;
            
            gunHolderTransform.rotation = Quaternion.Slerp(endRotation, originalRotation, rotationProgress);
        }
    }
    
    
    public override void ApplyTo(Gun target){
        gunHolderTransform = target.transform.parent;

        originalRotation = gunHolderTransform.rotation;
        endRotation = gunHolderTransform.rotation;
        elapsedTimeSinceShot = waitDurationBeforeReset;
        
        
        target.FireComponent.onFire += AddRecoil;
        target.onUpdate += OnUpdate;
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= AddRecoil;
        target.onUpdate -= OnUpdate;
    }
}
