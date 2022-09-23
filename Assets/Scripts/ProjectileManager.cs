using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    //Terrain damage
    public bool destroyTerrain;
    
    //Area damage
    public bool splashDamage;
    public float splashRadius;
    
    private GameObject _planeGenerator;
    private MeshController _planeMeshCon;

    private ActivePlayerController _activePlayerCon;

    //BlastSphere
    [SerializeField] private GameObject blastSphere;

    private float _damage;

    private bool _hasDealtDamage = false;
    
    void Awake()
    {
        _planeGenerator = GameObject.Find("MeshHolder");
        _activePlayerCon = GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>();
        _damage = _activePlayerCon.activePlayer.GetComponent<WeaponManager>().activeWeapon.damage;
        _planeMeshCon = _planeGenerator.GetComponent<MeshController>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, this.gameObject.GetComponent<Rigidbody>().velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasDealtDamage == false)
        {
            GameObject newHitParticle = Instantiate(hitParticle, transform.position, hitParticle.transform.rotation);
            Destroy(newHitParticle, 1f);
        
            if (splashDamage)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRadius);
                GameObject newBlastSphere = Instantiate(blastSphere, transform.position, Quaternion.identity);

                newBlastSphere.GetComponent<ScaleMe>().StartScale(splashRadius);
            
                Destroy(newBlastSphere, .5f);
            
                foreach (Collider hitCol in hitColliders)
                {
                    if (hitCol.transform.CompareTag("Player"))
                    {
                        hitCol.gameObject.GetComponent<PlayerManager>().TakeDamage(_damage);
                        _hasDealtDamage = true;
                    
                        Rigidbody currentRb =hitCol.gameObject.GetComponent<Rigidbody>();
                        _activePlayerCon.ResetGroundTimer();
                        currentRb.isKinematic = false;
                        currentRb.AddExplosionForce(350, transform.position, splashRadius * 2f);
                    }
                }
            }

            else
            {
                if (other.CompareTag("Player"))
                {
                    other.gameObject.GetComponent<PlayerManager>().TakeDamage(_damage);
                    _hasDealtDamage = true;
                }
            }
        
            if (destroyTerrain)
            {
                _planeMeshCon.DeformMesh(transform.position);
            }
        
            Destroy(this.gameObject);
        }
        
    }

    
}
