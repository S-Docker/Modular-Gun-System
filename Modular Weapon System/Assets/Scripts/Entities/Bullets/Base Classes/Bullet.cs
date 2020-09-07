using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletData bulletData;
    Vector3 startPosition;

    protected virtual void Start(){
        startPosition = this.transform.position;

        if (bulletData.HasBulletEffect){
            bulletData.BulletEffect.OnStartEffect();
        }
    }

    protected virtual void Update()
    {
        MoveBullet();

        if (MaxDistanceTravelled()){
            DestroyBullet();
        }
    }

    protected virtual void MoveBullet(){
        transform.position += Vector3.forward * bulletData.BulletSpeed * Time.deltaTime;

        if (bulletData.HasBulletEffect){
            bulletData.BulletEffect.ContinuousEffect();
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        Debug.Log("Target Hit");
        IHealthComponent healthComponent = other.GetComponent<IHealthComponent>();
        if (healthComponent == null) return;

        if (bulletData.HasBulletEffect){
            bulletData.BulletEffect.OnEndEffect();
        }
        healthComponent.TakeDamage();
        Destroy(this.gameObject);
    }


    protected bool MaxDistanceTravelled(){
        return (int)Vector3.Distance(startPosition, transform.position) > bulletData.MaxBulletTravel;
    }

    protected void DestroyBullet(){
        Destroy(this.gameObject);
    }
}
