using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gunHoldPosition;
    public Gun heldGun;

    void Start() {

    }

    void Update() {
        Test();
    }

    public void Test(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            heldGun.transform.parent = gunHoldPosition.transform;
            heldGun.transform.localPosition = Vector3.zero;
            heldGun.OnEquipped();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            heldGun.transform.parent = null;
            heldGun.OnUnequipped();
        }

        if (Input.GetMouseButtonDown(0)){
            heldGun.Fire();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            heldGun.Reload();
        }
    }

}
