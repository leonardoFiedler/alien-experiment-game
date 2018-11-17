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
}
