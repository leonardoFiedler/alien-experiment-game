using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase02Controller : MonoBehaviour {

    public Transform playerSpawn;
    public Transform enemySpawn01;
    public Transform enemySpawn02;
    public Transform enemySpawn03;
    public Transform enemySpawn04;
    public Transform enemySpawn05;

    // Use this for initialization
    void Start () {

        //Instancia o player que vai aparecer
        Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity);

        //Instancia o enemy que vai aparecer
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn01.position.x, enemySpawn01.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn02.position.x, enemySpawn02.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn03.position.x, enemySpawn03.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn04.position.x, enemySpawn04.position.y, 0), Quaternion.identity);
        Instantiate(Resources.Load("Enemy", typeof(GameObject)), new Vector3(enemySpawn05.position.x, enemySpawn05.position.y, 0), Quaternion.identity);

        //Limpa a visualizacao
        StartCoroutine(ExecuteAfterTime());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(5);
        
    }
}
