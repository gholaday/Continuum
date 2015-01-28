using UnityEngine;
using System.Collections;

public class MusicBounce : MonoBehaviour {

	public int detail = 500;
	public float amplitude = 0.1f;
	private float startPosition;

	public FFTWindow window;
	
	void Start()
	{
		startPosition = transform.localScale.y;
	}
	
	void Update() 
	{
		float[] info = new float[detail];
		AudioListener.GetSpectrumData(info,0,window);
		float packagedData = 0.0f;
		
		for(int x=0; x < info.Length; x++)
		{
			packagedData += System.Math.Abs(info[x]);   
		}

		Vector3 pos;

		pos = transform.localScale;

		pos.x = startPosition + packagedData * amplitude;
		pos.y = startPosition + packagedData * amplitude;
		//pos.y = startPosition + packagedData * amplitude;


		transform.localScale = pos;
	}
}
