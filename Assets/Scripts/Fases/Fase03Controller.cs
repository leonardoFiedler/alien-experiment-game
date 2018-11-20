using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase03Controller : BaseFaseController 
{
    public Transform enemySpawn;
    public Transform mesa;
    private int idCollectable;
    
    private int[] ordemPortas = new int[] { 2, 5, 4, 1, 3 };
    private List<int> listaPortas = new List<int>();

    // Use this for initialization
    void Start () {

        //Instancia o player que vai aparecer
        player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), 
							Quaternion.identity) as GameObject;

        //Para execucao normal, obtem o player
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        //player.transform.position = playerSpawn.position;
        
        //Limpa a visualizacao
        StartCoroutine(ExecuteAfterTime());
    }
	
	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
	}

	public override void GetInput()
	{
        int count = 0;
		if (Input.GetKeyDown(KeyCode.E))
		{
			Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
            if (collectObject.Length > 0)
            {
                foreach(Collider2D collider2D in collectObject) 
                {
                    Debug.Log("Tag: " + collider2D.tag);
                    if (collider2D.tag == "Porta")
                    {
                        idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;
                        Debug.Log("Tag: " + collider2D.tag + " - ID: " + idCollectable);
                        
                        if (idCollectable > 0)
                        {
                            count = listaPortas.Count;
                            Debug.Log("2 - ID: " + idCollectable + " - Count: " + count);
                            if (count == 0 && idCollectable == 2)
                            {
                                listaPortas.Add(idCollectable);
                                Debug.Log("Entrou porta 02");
                            }
                            else if (ordemPortas[count] == idCollectable)
                            {
                                 listaPortas.Add(idCollectable);
                                Debug.Log("Entrou porta " + idCollectable);
                            }
                            else
                            {
                                Debug.Log("Carregou inimigo");
                                Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(player.transform.position.x, player.transform.position.y, 0), 
                                            Quaternion.identity);
                            }

                            if (ordemPortas.Length == listaPortas.Count)
                            {
                                //Instancia o papel e vai para a proxima fase
                               Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
		}
        base.GetInput();
	}

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(5);
    }
}
