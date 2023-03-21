using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Name")]
    public new string name;

    [Header("Damage Info")]
    public float damage;
    public float maxDistance;

    [Header("Ammo Info")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
}
