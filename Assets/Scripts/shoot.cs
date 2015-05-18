using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour
{

    public static float cooldown = 0.25f;
    public static string weaponName = "Laser";

    void Start()
    {

        cooldown += .05f;
        
    }
}
	



