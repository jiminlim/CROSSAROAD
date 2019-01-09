using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCTL : MonoBehaviour {

	public void GameStartBTN()
    {
        SceneManager.LoadScene("Playing");
    }
    
}
