using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase01Controller : MonoBehaviour {

	public Transform playerSpawn;

	public Transform[] marcacaoPositions;
	private int[] marcacoes; //Marcacoes corretas
	private int[] marcacoesPlayer; //Marcacoes preenchidas pelo player
	private GameObject[] marcacoesGO; //Lista de marcacoes para poder destruir e manter os itens

	void Start () {
		marcacoes = new int[6] {1,2,3,1,2,3};
		marcacoesGO = new GameObject[6];
		marcacoesPlayer = marcacoes;

		//Instancia o player que vai aparecer
		Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity);
		SetMarkerPositions();

		//Limpa a visualizacao
		StartCoroutine(ExecuteAfterTime());
	}
	
	void Update () {
		
	}

	void SetMarkerPositions()
	{
		for (int i = 0; i < marcacoesPlayer.Length; i++)
		{
			switch (marcacoesPlayer[i])
			{
				case 0: //Vazio - Madeira
				Instantiate(Resources.Load("Fase01/Marcacao", typeof(GameObject)), 
							new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
							Quaternion.identity);
				break;

				case 1: //Amarelo
				Instantiate(Resources.Load("Fase01/BlocoAmarelo", typeof(GameObject)), 
							new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
							Quaternion.identity);
				break;

				case 2: //Roxo
				Instantiate(Resources.Load("Fase01/BlocoRoxo", typeof(GameObject)), 
							new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
							Quaternion.identity);
				break;

				case 3: //Laranja
				Instantiate(Resources.Load("Fase01/BlocoLaranja", typeof(GameObject)), 
							new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
							Quaternion.identity);
				break;
			}
		}
	}

	IEnumerator ExecuteAfterTime()
	{
		yield return new WaitForSeconds(5);
		
		//Destruir o game object corrente.
		//Setar os novos game objects para a lista
		marcacoesPlayer =  new int[] {0,0,0,0,0,0};	
		SetMarkerPositions();
	}
}
