using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase03Controller : BaseFaseController 
{
    public Transform enemySpawn;
    public Transform mesa;
    private int idCollectable;
    
    private string[] ordemPortas = new string[] { "Porta02", "Porta05", "Porta04", "Porta01", "Porta03" };
    private List<string> listaPortas = new List<string>();

    // Use this for initialization
    void Start () {

        //Instancia o player que vai aparecer
        //Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity);

        player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), 
							Quaternion.identity) as GameObject;
        
        //Instancia o enemy que vai aparecer
        //Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn.position.x, enemySpawn.position.y, 0), Quaternion.identity);

        //Limpa a visualizacao
        StartCoroutine(ExecuteAfterTime());
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}

	public override void GetInput()
	{
		if (Input.GetKey(KeyCode.E))
		{
			Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 3.0f);
            if (collectObject.Length > 0)
            {
                foreach(Collider2D collider2D in collectObject) 
                {
                    //if(collider2D.GetComponent<CollectableBehavior>() == null)
                    //    Debug.Log("Aqui");
                    //break;
                    Debug.Log("Tag: " + collider2D.tag);
                    idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;
                    Debug.Log("Tag: " + collider2D.tag + " - ID: " + idCollectable);
                    
                    if (listaPortas.Count == 0 && collider2D.tag == "Porta02")
                        Debug.Log("Entrou porta 02");
                    
                    break;
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
