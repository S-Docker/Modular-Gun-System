using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Gun heldGun;

    void Start() {

    }

    void Update() {
        Test();
    }

    public void Test(){
        if (Input.GetKeyDown(KeyCode.Space)){
            heldGun.Equip();
        }

        if (Input.GetKeyDown(KeyCode.F)){
            heldGun.Unequip();
        }

        if (Input.GetMouseButtonDown(0)){
            heldGun.Fire();
        }
    }

}
