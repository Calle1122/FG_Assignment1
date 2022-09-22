using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    public bool destroyTerrain;
    
    private GameObject _planeGenerator;
    private MeshController _planeMeshCon;

    private float _damage;
    
    void Awake()
    {
        _planeGenerator = GameObject.Find("MeshHolder");
        _damage = GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>().activePlayer.GetComponent<WeaponManager>().activeWeapon.damage;
        _planeMeshCon = _planeGenerator.GetComponent<MeshController>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, this.gameObject.GetComponent<Rigidbody>().velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject newHitParticle = Instantiate(hitParticle, transform.position, hitParticle.transform.rotation);
        Destroy(newHitParticle, 1f);

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(_damage);
        }

        if (destroyTerrain)
        {
            _planeMeshCon.DeformMesh(transform.position);
        }
        
        Destroy(this.gameObject);
    }
}
