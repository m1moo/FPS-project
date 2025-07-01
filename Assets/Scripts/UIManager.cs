using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        UpdateHealthUI();
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        WeaponBase currentWeapon = null;

        Transform player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            // Search anywhere under player, including inactive objects, and pick the first active weapon
            WeaponBase[] weapons = player.GetComponentsInChildren<WeaponBase>(true);

            foreach (var weapon in weapons)
            {
                if (weapon.gameObject.activeInHierarchy)
                {
                    currentWeapon = weapon;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Player not found");
        }

        if (currentWeapon != null)
        {
            ammoText.text = "Ammo: " + currentWeapon.CurrentAmmo + " / " + currentWeapon.MaxAmmo;
        }
        else
        {
            ammoText.text = "Ammo: -";
        }
    }



    void UpdateHealthUI()
    {
        if (playerHealth != null)
        {
            healthText.text = "Health: " + Mathf.Ceil(playerHealth.CurrentHealth);
        }
    }
}

