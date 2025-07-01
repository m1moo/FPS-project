using UnityEngine;

public class Pistol : WeaponBase
{
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public override void Fire()
    {
        if (Time.time >= lastFireTime + fireRate && currentAmmo > 0)
        {
            base.Fire();
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            Debug.Log("Fired, Bullets left: " + currentAmmo);
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo, Press R to reload.");
        }
    }

    public override void Reload()
    {
        base.Reload();
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
        Debug.Log("Reloaded, Bullets refilled to: " + currentAmmo);
    }

    protected override void ShootProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * 20f;
        }
    }
}



