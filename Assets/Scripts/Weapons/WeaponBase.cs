using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected int maxAmmo = 10;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;

    protected int currentAmmo;
    protected float lastFireTime;

    // Public properties to expose ammo counts to UIManager
    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;

    // Initialize ammo count
    protected virtual void Awake()
    {
        currentAmmo = maxAmmo;
    }

    protected virtual void Update()
    {
        // Child classes will handle input here
    }

    // Called when firing the weapon
    public virtual void Fire()
    {
        if (Time.time >= lastFireTime + fireRate && currentAmmo > 0)
        {
            ShootProjectile();
            currentAmmo--;
            lastFireTime = Time.time;
        }
    }

    // Spawn the projectile at firePoint
    protected virtual void ShootProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    // Reload weapon to max ammo
    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
    }
}




