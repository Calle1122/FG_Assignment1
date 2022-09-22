using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon activeWeapon;
    private GameObject _activeWeaponObj;
    
    private BattleUIController _battleUICon;

    public bool canShoot = false;
    
    public float maxChargeTime = 1f;
    private float _shootMultiTimer = 0f;
    private bool _isCharging = false;
    private bool _countUp = true;

    private GameObject _cameraConObj;
    private CameraController _cameraCon;
    
    [SerializeField] private GameObject weaponHolder;

    private void Start()
    {
        _cameraConObj = GameObject.Find("CameraController");
        _cameraCon = _cameraConObj.GetComponent<CameraController>();
        _battleUICon = GameObject.Find("BattleUIController").GetComponent<BattleUIController>();
    }

    private void Update()
    {
        if (canShoot)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                if (activeWeapon != null)
                {
                    if (_isCharging)
                    {
                        ChargeTime();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (activeWeapon.shouldCharge)
                        {
                            _shootMultiTimer = 0;
                
                            _battleUICon.shootSliderHolder.SetActive(true);
                            _isCharging = true;
                        }

                        else
                        {
                            Vector3 spawnPoint = this.transform.GetChild(0).transform.position + (transform.forward * 1.5f);
                            activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, activeWeapon.baseForce, transform.rotation);
                        }
                        
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (activeWeapon.shouldCharge)
                        {
                            _battleUICon.shootSliderHolder.SetActive(false);
                            _isCharging = false;

                            Vector3 spawnPoint = this.transform.GetChild(0).transform.position + (transform.forward * 1.5f);
                            activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, _shootMultiTimer * 10f, transform.rotation);
                        }

                    }
                    
                }
                
            }
        }
    }

    public void EquipWeapon(Weapon toEquip)
    {
        NoWeaponActive();
        
        activeWeapon = toEquip;
        
        _activeWeaponObj = Instantiate(activeWeapon.weapon, weaponHolder.transform.position, transform.rotation);
        _activeWeaponObj.transform.parent = weaponHolder.transform;
    }

    public void NoWeaponActive()
    {
        activeWeapon = null;
        
        Destroy(_activeWeaponObj);
    }
    
    void ChargeTime()
    {
        switch(_countUp)
        {
            case true:
                _shootMultiTimer += Time.deltaTime;
                if (_shootMultiTimer > maxChargeTime)
                {
                    _shootMultiTimer = maxChargeTime;
                    _countUp = false;
                }
                break;
                
            case false:
                _shootMultiTimer -= Time.deltaTime;
                if (_shootMultiTimer < 0)
                {
                    _shootMultiTimer = 0;
                    _countUp = true;
                }
                break;
        }

        _battleUICon.shootSlider.value = _shootMultiTimer / maxChargeTime;
    }
}
