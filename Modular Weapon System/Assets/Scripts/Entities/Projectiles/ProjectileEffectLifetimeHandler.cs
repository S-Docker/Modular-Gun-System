using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectLifetimeHandler : MonoBehaviour
{
    [SerializeField][HideInInspector] float effectLifetime;
    
    void OnValidate(){
        ParticleSystem partSys = GetComponent<ParticleSystem>();
        effectLifetime = partSys.main.duration;
    }

    void Start(){
       Destroy(gameObject, effectLifetime); 
    }
}
