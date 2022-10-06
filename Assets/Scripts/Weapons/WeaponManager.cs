
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon activeWeapon;
    private GameObject _activeWeaponObj;
    
    private BattleUIController _battleUICon;

    public bool canShoot = false;

    public int charges = 3;
    
    public float maxChargeTime = 1f;
    private float _shootMultiTimer = 0f;
    private bool _isCharging = false;
    private bool _countUp = true;

    private GameObject _cameraConObj;
    private CameraController _cameraCon;
    
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject dualPos1, dualPos2;

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
                    if (activeWeapon.chargesToUse <= charges)
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
                                SoundManager.SoundManagerInstance.PlaySound(activeWeapon.fireSound);
                                
                                Vector3 spawnPoint = this.transform.GetChild(0).transform.position + (transform.forward * 1.5f);
                                charges -= activeWeapon.chargesToUse;
                                activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, activeWeapon.baseForce, transform.rotation);
                                _battleUICon.SetChargeImgs(charges);
                            }
                            
                        }

                        if (Input.GetMouseButtonUp(0))
                        {
                            if (activeWeapon.shouldCharge)
                            {
                                _battleUICon.shootSliderHolder.SetActive(false);
                                _isCharging = false;

                                if (activeWeapon.doubleShot)
                                {
                                    Vector3 spawnPoint = dualPos1.transform.position + (transform.forward * 1.5f);
                                    charges -= activeWeapon.chargesToUse;
                                    activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, _shootMultiTimer * activeWeapon.baseForce, transform.rotation);
                                    spawnPoint = dualPos2.transform.position + (transform.forward * 1.5f);
                                    activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, _shootMultiTimer * activeWeapon.baseForce, transform.rotation);
                                    _battleUICon.SetChargeImgs(charges);
                                }


                                else
                                {
                                    Vector3 spawnPoint = this.transform.GetChild(0).transform.position + (transform.forward * 1.5f);
                                    charges -= activeWeapon.chargesToUse;
                                    activeWeapon.Shoot(spawnPoint, _cameraCon.activeCamera.transform.forward, _shootMultiTimer * activeWeapon.baseForce, transform.rotation);
                                    _battleUICon.SetChargeImgs(charges);
                                }
                                
                                SoundManager.SoundManagerInstance.PlaySound(activeWeapon.fireSound);
                                
                            }

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
