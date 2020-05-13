using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	[SerializeField]
	Text score;
	float scoreTime = 0;
	bool timerOn = true;

	// Use this for initialization
	void Start () {
		score.text = "" + scoreTime;
	}

	public void StopGameTimer()
    {
		timerOn = false;
    }

	// Update is called once per frame
	void Update () {
		if (timerOn)
		{
			scoreTime += Time.deltaTime;
			score.text = "" + (int)scoreTime;
		}
	}
}
