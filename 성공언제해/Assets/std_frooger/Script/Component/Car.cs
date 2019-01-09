using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public float MoveSpeed = 0f;
    public float RangeDestroy = 12;
    void Start ()
    {
		
	}
     void levelup()
    {
        if (CRGameManager.Instance.Point <= 10)
        {
            MoveSpeed=Random.Range(1, 5);
        }else if(CRGameManager.Instance.Point > 10&& CRGameManager.Instance.Point <= 10)
        {
            MoveSpeed = Random.Range(5, 7);
        }
        else
        {
            MoveSpeed = 8f;
        }
        
    }
	void Update ()
    {
      //  float randomval = 0f;
       // randomval = Random.Range(1, 5);
        levelup();

        float movex = MoveSpeed * Time.deltaTime;
        this.transform.Translate(movex, 0f, 0f);
        if (this.transform.localPosition.x >= RangeDestroy) //차 제거
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
