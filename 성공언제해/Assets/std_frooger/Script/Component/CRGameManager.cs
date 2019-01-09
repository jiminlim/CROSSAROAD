using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRGameManager : MonoBehaviour {

    private static CRGameManager instance = null;

    public static CRGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("_CRGameManager");
                instance = gameObject.AddComponent<CRGameManager>();
            }
            return instance;
        }
    }
    private int point = 0;
    public int Point
    {
        set { point = value; }
        get { return point; }
    }

    private bool isGameOver = false;
    public bool IsGameOver
    {
        set { isGameOver = value; }
        get { return isGameOver; }
    }

    private int bestPoint = 0;
    public int BestPoint
    {
        set { bestPoint = value; }
        get { return bestPoint; }
    }
    public void OnGetPoint()
    {
        bestPoint = PlayerPrefs.GetInt("BestPoint");
    }
    private void Awake()
    {
        instance = this;
    }
}
