using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase05Controller : BaseFaseController {

    [SerializeField]
    private EnemyCharacterController boss;

	void Start () {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
			player = GameObject.FindGameObjectsWithTag("Player")[0];
        	player.transform.position = playerSpawn.position;
		} else {
			player = Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0), Quaternion.identity) as GameObject;
		}
    }
	
	public override void Update () {
        base.Update();

        CheckEndGame();
    }

    void CheckEndGame()
    {
        if (boss.Health.MyCurrentValue <= 0)
        {
            //EndGame
            StartCoroutine(DeathKillCam());
        }
    }

    //Controla a death kill cam do Boss - espera 2 segundos e direciona para a tela de vitoria
    private IEnumerator DeathKillCam()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Vitoria");
    }
}
