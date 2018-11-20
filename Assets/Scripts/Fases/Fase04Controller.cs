using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase04Controller : BaseFaseController
{
    [SerializeField]
    private GameObject[] bourbons;

	private int[] ordemBourbons = new int[] { 1, 5, 3, 0, 2, 4 };
	private List<int> listaBourbons = new List<int>();
    private int idCollectable; //Id do objeto coletado;
	public Transform enemySpawn;

    void Start () 
    {
        //Instancia o player que vai aparecer
        player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), 
							Quaternion.identity) as GameObject;

        //Para execucao normal, obtem o player
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        //player.transform.position = playerSpawn.position;
        
		SetOpenBourbon();
		SetCloseBourbon();

		//Limpa a visualizacao
        StartCoroutine(ExecuteAfterTime());
    }
        
    public override void Update () 
    {
        base.Update();
    }       

    public override void GetInput()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			int count = 0;
			bool acertou = true;

			Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
            if (collectObject.Length > 0)
            {
                foreach (Collider2D collider2D in collectObject) 
				{
					if (collider2D.tag == "Barril")
					{
						idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;

						Debug.Log("Ordem: " + ordemBourbons.Length + " - Lista: "+ listaBourbons.Count);

						if (ordemBourbons.Length == (listaBourbons.Count + 1))
						{
							listaBourbons.Add(idCollectable);
							
							count = 0;
							foreach (int bourbon in ordemBourbons)
							{
								if (bourbon != listaBourbons[count])
								{
									acertou = false;
									break;
								}
								count++;
							}
							
							Debug.Log("Acertou: " + acertou);
							if (acertou)
								Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);
							else
							{
								for (int i = 0; i < 3; i++)
								{
									Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn.position.x, enemySpawn.position.y, 0), 
                                        Quaternion.identity);
								}
							}
						}
						else
						{
							listaBourbons.Add(idCollectable);
							Debug.Log("Barril: " + idCollectable);
						}

						break;
					}
                }
            }
		}
        base.GetInput();
	}

	public void SetOpenBourbon()
	{
		foreach (int i in ordemBourbons)
		{
			bourbons[i].GetComponent<Animator>().SetBool("open", true);
			StartCoroutine(ExecuteAfterTime());
		}
	}

	public void SetCloseBourbon()
	{
		foreach (var bourbon in bourbons)
		{
			bourbon.GetComponent<Animator>().SetBool("open", false);
			StartCoroutine(ExecuteAfterTime());
		}
	}

	IEnumerator ExecuteAfterTime()
	{
		yield return new WaitForSeconds(10);
	}
}
