using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGameController : MonoBehaviour {

	public void OnClickBtnInicio()
    {
        SceneManager.LoadScene("Menu");
    }
}
