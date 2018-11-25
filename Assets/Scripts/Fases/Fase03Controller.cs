using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase03Controller : BaseFaseController 
{
    public GameObject enemy;
    public Transform enemySpawn;
    private int idCollectable;
    
    private int[] ordemPortas = new int[] { 2, 5, 4, 1, 3 };
    private List<int> listaPortas = new List<int>();

    // Use this for initialization
    void Start () 
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
			player = GameObject.FindGameObjectsWithTag("Player")[0];
        	player.transform.position = playerSpawn.position;
		} else {
			player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity) as GameObject;
		}

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
                    if (collider2D.tag == "Porta")
                    {
                        idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;
                        
                        if (idCollectable > 0)
                        {
                            Debug.Log("Porta " + idCollectable);

                            count = listaPortas.Count;
                            if (count == 0 && idCollectable == 2)
                            {
                                listaPortas.Add(idCollectable);
                            }
                            else if (ordemPortas[count] == idCollectable)
                            {
                                listaPortas.Add(idCollectable);
                            }
                            else
                            {
                                enemy = Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn.position.x, 
                                                        enemySpawn.position.y, 0), Quaternion.identity) as GameObject;
                                    GameObject goRange = enemy.transform.Find("Range").gameObject;
                                    goRange.GetComponent<CircleCollider2D>().radius = 12.0f;
                                
                                 listaPortas.Clear();
                                 Debug.Log("Errou");
                            }

                            if (ordemPortas.Length == listaPortas.Count)
                            {
                                Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);
                                listaPortas.Clear();
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

        Destroy(enemy);
    }
}
