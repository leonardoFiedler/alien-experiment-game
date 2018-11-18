using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase04Controller : BaseFaseController
{
    [SerializeField]
    private GameObject[] bourbons;

    void Start () 
    {
        //Instancia o player que vai aparecer
        player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), 
							Quaternion.identity) as GameObject;

        //Para execucao normal, obtem o player
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        //player.transform.position = playerSpawn.position;
            
        //Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);   
        

        //para abrir todos os barrils
        foreach (var bourbon in bourbons)
        {
            bourbon.GetComponent<Animator>().SetBool("open", true);
        }

        

    }
        
    public override void Update () 
    {
        base.Update();
    }       

    public override void GetInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
        	Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
                    if (collectObject.Length > 0)
                    {
                            foreach (Collider2D collider2D in collectObject) 
                            {
                                    if(collider2D.tag == "Barril")
                                    {
                                            Debug.Log("Tag: " + collider2D.tag);
                                            int idCollectable = collider2D.GetComponent<CollectableBehavior>().Id;
                                            Debug.Log("Collect: " + idCollectable);
                                    }
                            }
                    }
        }
            base.GetInput();
    }
}
