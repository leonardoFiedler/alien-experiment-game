using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase04Controller : BaseFaseController
{
        void Start () 
        {
                //player = GameObject.FindGameObjectsWithTag("Player")[0];
                //player.transform.position = playerSpawn.position;   
                player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity) 
                as GameObject;    
                Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);    
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
