using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

	public Image[] content;
	public Text textLifebar;
	private int currentValue;

	private static int maxValue = 5;

	public int MyCurrentValue
	{
		get
		{
			return currentValue;
		}

		set
		{
			if (value > maxValue) {
				currentValue = maxValue;
			} else {
				currentValue = value;
			}
		} 
	}


	void Start () {
		
	}
	
	void Update () {
		Debug.Log(currentValue);
		UpdateLifeBar();
		textLifebar.text = currentValue + " / " + maxValue;
	}

	void UpdateLifeBar() {
		for (int i = 0; i < content.Length; i++)
		{
			content[i].enabled = false;
		}

		for (int i = 0; i < currentValue; i++)
		{
			content[i].enabled = true;
		}
	}
}
