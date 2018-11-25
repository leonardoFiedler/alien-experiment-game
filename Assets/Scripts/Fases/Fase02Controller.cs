using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase02Controller : BaseFaseController
{
    public Transform enemySpawn01;
    public Transform enemySpawn02;
    public Transform enemySpawn03;
    public Transform enemySpawn04;
    public Transform enemySpawn05;

    void Start ()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
			player = GameObject.FindGameObjectsWithTag("Player")[0];
        	player.transform.position = playerSpawn.position;
		} else {
			player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity) as GameObject;
		}

        //Instancia a posicao para ir para a proxima fase
        Instantiate(Resources.Load("Papers"), papersPosition.position, Quaternion.identity);
        //Instancia o enemy que vai aparecer
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn01.position.x, enemySpawn01.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn02.position.x, enemySpawn02.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn03.position.x, enemySpawn03.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn04.position.x, enemySpawn04.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn05.position.x, enemySpawn05.position.y, 0), Quaternion.identity);

        //Limpa a visualizacao
        StartCoroutine(ExecuteAfterTime());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void GetInput()
    {
        base.GetInput();
    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(5);
    }
}
