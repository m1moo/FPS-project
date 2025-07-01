using UnityEngine;

public class AssaultRifle : WeaponBase
{
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();  // Initialize ammo
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (Input.GetButton("Fire1"))
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
            Debug.Log("AR Fired, Bullets left: " + currentAmmo);
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("AR Out of ammo, Press R to reload.");
        }
    }

    public override void Reload()
    {
        base.Reload();
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
        Debug.Log("AR Reloaded, Bullets refilled to: " + currentAmmo);
    }

    protected override void ShootProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * 30f;
        }
    }
}



