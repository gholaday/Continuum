using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour{

	private int score = 0;

	public float duration = 0.5f;

	public void SetScore(int value)
	{
		score = value;
	}

	public void IncreaseScore(float value, float multiplier){

		score += (int)((value * (1.0))* multiplier);
	}

	public int GetScore(){
		return score;
	}



}
