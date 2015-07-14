using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SpinEffect : MonoBehaviour {


	public float angleIncrease = 0f;
	
	Vortex vortex;

	// Use this for initialization
	void Start () {
	
		vortex = GetComponent<Vortex>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		vortex.angle += angleIncrease;
		
		if(vortex.angle > 1000f)
		{
			angleIncrease = -angleIncrease;
			
		}
		
		if(vortex.angle < 0)
		{
			vortex.angle = 0;
			angleIncrease = 0;
		}
	
	}
	
	public void StartEffect()
	{
		angleIncrease = 25f;
	}
}
