using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentIndex = 0;

    void Start()
    {
        //only the first weapon at start
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentIndex);
        }
    }

    void Update()
    {
        // Switch weapons with number keys  
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
            }
        }
    }

    void SwitchWeapon(int index)
    {
        if (index == currentIndex) return;

        weapons[currentIndex].SetActive(false);
        weapons[index].SetActive(true);
        currentIndex = index;
    }
}

