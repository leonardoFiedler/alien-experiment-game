using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase01Controller : BaseFaseController
{
	private int idCollectable; //Id do objeto coletado;
	public Transform[] marcacaoPositions;
	private int[] marcacoes; //Marcacoes corretas
	private int[] marcacoesPlayer; //Marcacoes preenchidas pelo player
	private GameObject[] marcacoesGO; //Lista de marcacoes para poder destruir e manter os itens
    
	void Start () {
        nextSceneName = "Fase02";
		marcacoes = new int[6] {1,2,3,1,2,3};
		marcacoesGO = new GameObject[6];
		marcacoesPlayer = marcacoes;

		if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
			player = GameObject.FindGameObjectsWithTag("Player")[0];
        	player.transform.position = playerSpawn.position;
		} else {
			player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity) as GameObject;
		}

        //Apos carregar o player mantem ele vivo entre as fases
        DontDestroyOnLoad(player);

        SetMarkerPositions();
		//Limpa a visualizacao
		StartCoroutine(ExecuteAfterTime());
	}
	
	public override void Update () {
        base.Update();
	}

	public override void GetInput()
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

                        bool isEnd = true;
						foreach (var item in marcacoesPlayer)
						{
                            if (item == 0)
                            {
                                isEnd = false;
                                break;
                            }
						}

                        if (isEnd)
                        {
                            //Se nao ha mais o que preencher, verifica se esta correto
                            bool isRight = true;
                            for (int i = 0; i < marcacoes.Length; i++)
                            {
                               if (marcacoes[i] != marcacoesPlayer[i])
                               {
                                    isRight = false;
                                    break;
                               }
                            }

                            if (isRight) {
                                //Instancia o papel e vai para a proxima fase
                                Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);
                            } else {
                                //Limpa os blocos e pede para preencher novamente
                                marcacoesPlayer = new int[] { 0, 0, 0, 0, 0, 0 };
                                foreach(var marcacao in marcacoesGO) {
                                    Destroy(marcacao);
                                }

                                marcacoesGO = new GameObject[6];
                                SetMarkerPositions();
                            }
                        }

						idCollectable = 0;
						break;
					}
                }
            }
		}
        base.GetInput();
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
