using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	void Start () 
	{
		
	}

	public void Iniciar()
	{
		SceneManager.LoadScene("Fase01");
	}
}
