using System.Collections;
using System.Collections.Generic;
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
            heldGun.Equip(gunHoldPosition);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            heldGun.Unequip();
        }

        if (Input.GetMouseButtonDown(0)){
            heldGun.Fire();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            heldGun.Reload();
        }
    }

}
