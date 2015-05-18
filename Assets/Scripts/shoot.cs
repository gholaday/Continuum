using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour
{

    public static float cooldown = 0.25f;
    public static string weaponName;

    void Start()
    {
        if(cooldown < .2f)
        {
            cooldown += .05f;
        }
        

        InitializeWeapon();
         
    }

    void Update()
    {
        if (cooldown < .1f)
            cooldown = .1f;

        
    }

    void InitializeWeapon()
    {
        if(weaponName == "Laser")
        {
            GetComponentInChildren<WeaponDoubleLaser>().enabled = true;
            GetComponentInChildren<WeaponLaserBeam>().enabled = false;
        }
        else if (weaponName == "Beam")
        {
            GetComponentInChildren<WeaponDoubleLaser>().enabled = false;
            GetComponentInChildren<WeaponLaserBeam>().enabled = true;
        }
    }
}
	



