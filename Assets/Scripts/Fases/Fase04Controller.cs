using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase04Controller : BaseFaseController
{

	void Start () {

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.transform.position = playerSpawn.position;

        Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);

    }
	
	public override void Update () {
        base.Update();	
	}


}
