using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gunHoldPosition;
    [SerializeField] GameObject crosshair;
    public Gun heldGun;
    bool fireHeld;

    void Update() {
        Test();
    }

    public void Test(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            heldGun.transform.parent = gunHoldPosition.transform;
            heldGun.transform.localPosition = Vector3.zero;
            heldGun.OnEquipped();
            heldGun.EnableCrosshair(crosshair);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            heldGun.transform.parent = null;
            heldGun.OnUnequipped();
            crosshair.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)){
            if (heldGun.GunData.FireMode == FireMode.Automatic){
                fireHeld = true;
            }
            heldGun.Fire();
        }

        if (Input.GetMouseButtonUp(0)){
            fireHeld = false;
        }
        
        if (Input.GetMouseButtonDown(1)){
            heldGun.PerformAbility();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            heldGun.Reload();
        }

        if (fireHeld){
            heldGun.Fire();
        }
    }
}
