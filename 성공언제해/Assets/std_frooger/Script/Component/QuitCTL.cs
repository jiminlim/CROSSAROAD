using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitCTL : MonoBehaviour {

    private Text overText;
    private Image overImg;
    private Button overBtn;

    void Start()
    {
        overBtn = GetComponentInChildren<Button>();
        overText = GetComponentInChildren<Text>();
        overImg = GetComponent<Image>();

        //  overBtn.enabled = false;
        overBtn.gameObject.SetActive(false);
        overText.enabled = false;
        overImg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (CRGameManager.Instance.IsGameOver && !overImg.enabled) //gameover참이고 이미지 실행 ㄴㄴ
        {
            // overBtn.enabled = true;
            overBtn.gameObject.SetActive(true);
            overText.enabled = true;
            overImg.enabled = true;

        }
    }
}
