using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon activeWeapon;
    private GameObject _activeWeaponObj;

    private GameObject _cameraConObj;
    private CameraController _cameraCon;
    
    [SerializeField] private GameObject weaponHolder;

    private void Start()
    {
        _cameraConObj = GameObject.Find("CameraController");
        _cameraCon = _cameraConObj.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPoint = transform.position + transform.forward;
            activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, 15f, transform.rotation);
        }
    }

    public void EquipWeapon()
    {
        _activeWeaponObj = Instantiate(activeWeapon.weapon, weaponHolder.transform.position, transform.rotation);
        _activeWeaponObj.transform.parent = weaponHolder.transform;
    }
}
