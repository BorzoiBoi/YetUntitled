using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header ("Gun Reference")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform barrel;
    [SerializeField] Transform bullet;
    float timeSinceLastShot;

    public void Start()
    {
        Player.shootInput += Shoot;
        gunData.currentAmmo = gunData.magSize;
    }

    private bool CanShoot() => true; //(timeSinceLastShot > 1f / (gunData.fireRate / 60f));

    public void Shoot()
    {
        if(CanShoot())
        {
            Debug.Log("Bang");
            Transform bulletTransform = Instantiate(bullet, barrel.position, Quaternion.identity);
            bulletTransform.GetComponent<Bullet>().Setup(transform.right);

            gunData.currentAmmo--;
            timeSinceLastShot = 0;
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }
}
