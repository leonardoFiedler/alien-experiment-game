using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase01Controller : MonoBehaviour {

	public Transform playerSpawn;

	private GameObject player;

	private int idCollectable; //Id do objeto coletado;
	public Transform[] marcacaoPositions;
	private int[] marcacoes; //Marcacoes corretas
	private int[] marcacoesPlayer; //Marcacoes preenchidas pelo player
	
	private GameObject[] marcacoesGO; //Lista de marcacoes para poder destruir e manter os itens

	void Start () {
		marcacoes = new int[6] {1,2,3,1,2,3};
		marcacoesGO = new GameObject[6];
		marcacoesPlayer = marcacoes;

		//Instancia o player que vai aparecer
		player = Instantiate(Resources.Load("Player", typeof(GameObject)), 
							new Vector3(playerSpawn.position.x, 				
							 	playerSpawn.position.y, 0), 
							Quaternion.identity) as GameObject;
		SetMarkerPositions();

		//Limpa a visualizacao
		StartCoroutine(ExecuteAfterTime());
	}
	
	void Update () {
		GetInput();
	}

	void GetInput()
	{
		if (Input.GetKey(KeyCode.E))
		{
			Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
            if (collectObject.Length > 0)
            {
                foreach(Collider2D collider2D in collectObject) {
                    if (collider2D.tag == "Collect")
                    {
						idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;
                        Debug.Log("Collect " + idCollectable);
						break;
                    }

					if (collider2D.tag == "Drop" && idCollectable > 0 )
					{
						int pos = collider2D.GetComponent<DroppableBehavior>().Index;
						marcacoesPlayer[pos] = idCollectable;
						collider2D.SendMessage("SetResource", idCollectable, SendMessageOptions.DontRequireReceiver);
						
						Debug.Log("Marcacoes");
						foreach (var item in marcacoes)
						{
							Debug.Log("item :" + item);	
						}

						Debug.Log("Marcacoes Player");
						foreach (var item in marcacoesPlayer)
						{
							Debug.Log("item :" + item);	
						}

						idCollectable = 0;
						break;
					}
                }
            }
		}
	}

	void SetMarkerPositions()
	{
		for (int i = 0; i < marcacoesPlayer.Length; i++)
		{
			switch (marcacoesPlayer[i])
			{
				case 0: //Vazio - Madeira
				marcacoesGO[i] = Instantiate(Resources.Load("Fase01/Marcacao", typeof(GameObject)), 
									new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
									Quaternion.identity) as GameObject;
				marcacoesGO[i].GetComponent<DroppableBehavior>().Index = i;
				break;

				case 1: //Amarelo
				marcacoesGO[i] = Instantiate(Resources.Load("Fase01/BlocoAmarelo", typeof(GameObject)), 
									new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
									Quaternion.identity) as GameObject;
				break;

				case 2: //Roxo
				marcacoesGO[i] = Instantiate(Resources.Load("Fase01/BlocoRoxo", typeof(GameObject)), 
									new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
									Quaternion.identity) as GameObject;
				break;

				case 3: //Laranja
				marcacoesGO[i] = Instantiate(Resources.Load("Fase01/BlocoLaranja", typeof(GameObject)), 
									new Vector3(marcacaoPositions[i].position.x, marcacaoPositions[i].position.y, 0), 
									Quaternion.identity) as GameObject;
				break;
			}
		}
	}

	IEnumerator ExecuteAfterTime()
	{
		yield return new WaitForSeconds(5);
		
		//Destroi os objetos e recria com as marcacoes vazias
		foreach (var gm in marcacoesGO)
		{
			Destroy(gm);
		}
		
		marcacoesGO = new GameObject[6];
		marcacoesPlayer =  new int[] {0,0,0,0,0,0};	
		SetMarkerPositions();
	}
}
