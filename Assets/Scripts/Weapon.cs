
using UnityEngine;


[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public GameObject weapon;
    public GameObject projectile;

    public bool doubleShot;
    
    public bool shouldCharge;

    public int chargesToUse;
    
    public float baseForce;
    public float damage;

    public void Shoot(Vector3 projectileSpawnTransform, Vector3 shootDir, float shootForce, Quaternion projectileRot)
    {
        GameObject newProjectile = Instantiate(projectile, projectileSpawnTransform, projectileRot);
        Rigidbody newProjRb = newProjectile.GetComponent<Rigidbody>();
        
        newProjRb.AddForce(shootDir * (shootForce * baseForce), ForceMode.Impulse);
    }
}
