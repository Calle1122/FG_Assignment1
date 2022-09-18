using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public GameObject weapon;
    public GameObject projectile;
    
    public float damage;

    public void Shoot(Vector3 projectileSpawnTransform, Vector3 shootDir, float shootForce, Quaternion projectileRot)
    {
        GameObject newProjectile = Instantiate(projectile, projectileSpawnTransform, projectileRot);
        Rigidbody newProjRb = newProjectile.GetComponent<Rigidbody>();
        
        newProjRb.AddForce(shootDir * shootForce, ForceMode.Impulse);
    }
}
