using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseFaseController : MonoBehaviour
{

    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected string nextSceneName;

    [SerializeField]
    protected Transform playerSpawn;

    [SerializeField]
    public Transform papersPosition;

    public virtual void Update()
    {
        GetInput();
        CheckPlayerDeath();
    }

    public virtual void GetInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Collider2D[] collectObject = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
            if (collectObject.Length > 0)
            {
                foreach (Collider2D collider2D in collectObject)
                {
                    if (collider2D.tag == "nextStage")
                    {
                        Debug.Log("Loading next Stage");
                        SceneManager.LoadScene(nextSceneName);
                    }
                }
            }
        }
    }

    //Check de death do player - este controle e feito em cada fase
    public void CheckPlayerDeath()
    {
        if (player.GetComponent<PlayerCharacterController>().Health.MyCurrentValue <= 0)
        {
            StartCoroutine(DeathKillCam());
        }
    }

    //Controla a death kill cam do Player - espera 2 segundos e direciona para a tela de derrota
    private IEnumerator DeathKillCam()
    {
        yield return new WaitForSeconds(2);
        Destroy(player);
        SceneManager.LoadScene("Derrota");
    }
}
