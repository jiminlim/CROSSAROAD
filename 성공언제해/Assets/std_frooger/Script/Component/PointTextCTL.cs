using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTextCTL : MonoBehaviour {

    private Text myText;
    public bool is_Best = false;

	void Start () {
        myText = GetComponent<Text>();
       

        if (myText != null)
        {
            CRGameManager.Instance.OnGetPoint();
            MyTextUpdate();
        }
       
	}
	

	void Update () {
        MyTextUpdate();
    }

    private void MyTextUpdate()
    {
        if (myText != null)
        {
            if (!is_Best)
            {
                myText.text = CRGameManager.Instance.Point.ToString();
            }
            else
            {

                myText.text = "최고점수 : " + CRGameManager.Instance.BestPoint.ToString();
            }
        }
       
    }
}
