using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCTL : MonoBehaviour {
    public void GameStartBTN()
    {
        SceneManager.LoadScene("start");
    }
}
