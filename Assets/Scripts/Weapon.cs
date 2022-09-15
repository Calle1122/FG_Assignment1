using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float range;

    public int maxAmmo;
    private int _ammo;

    public void Shoot()
    {
        _ammo--;
    }

    public void Reload()
    {
        _ammo = maxAmmo;
    }
}
