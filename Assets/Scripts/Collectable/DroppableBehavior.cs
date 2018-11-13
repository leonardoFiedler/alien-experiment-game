using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableBehavior : MonoBehaviour {
	
	[SerializeField]
	private int index;

	public int Index {
		get {
			return index;
		}

		set {
			index = value;
		}
	}

	void SetResource(int id)
	{
		Debug.Log("Set the selected id " + id);
		switch (id)
		{
			case 0: //Vazio - Madeira
			 	this.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("Fase01/Marcacao").GetComponent<SpriteRenderer>().sprite;
			break;

			case 1: //Amarelo
				this.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("Fase01/BlocoAmarelo").GetComponent<SpriteRenderer>().sprite;
			break;

			case 2: //Roxo
				this.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("Fase01/BlocoRoxo").GetComponent<SpriteRenderer>().sprite;
			break;

			case 3: //Laranja
				this.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("Fase01/BlocoLaranja").GetComponent<SpriteRenderer>().sprite;
			break;
		}
	}
}
