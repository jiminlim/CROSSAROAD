using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpan : MonoBehaviour {

    public List<Transform> EnviromentObjectList = new List<Transform>();
    public int StartMinVal = -12;
    public int StartMaxVal = 12;

    public int SpawnCreateRandom = 50; //생성되는 비율

    void GeneratorRoundBlock()
    {
        int randomindex = 0;
        GameObject tempclone = null;
        Vector3 offsetpos = Vector3.zero;


        for (int i = StartMinVal; i < StartMaxVal; ++i)//길이 증가하면서 랜덤하게 생성
        {
            if (i<-5 || i>5)
            {
                randomindex = Random.Range(0, EnviromentObjectList.Count);
                tempclone = GameObject.Instantiate(EnviromentObjectList[randomindex].gameObject);
                tempclone.SetActive(true);
                offsetpos.Set(i, 1f, 0f);

                tempclone.transform.SetParent(this.transform); //현재위치 값 세팅
                tempclone.transform.localPosition = offsetpos;
            }
        }
    }
    void GeneratorBackBlock()
    {
        int randomindex = 0;
        GameObject tempclone = null;
        Vector3 offsetpos = Vector3.zero;


        for (int i = StartMinVal; i < StartMaxVal; ++i)//길이 증가하면서 랜덤하게 생성
        {
            randomindex = Random.Range(0, EnviromentObjectList.Count);
            tempclone = GameObject.Instantiate(EnviromentObjectList[randomindex].gameObject);
            tempclone.SetActive(true);
            offsetpos.Set(i, 1f, 0f);

            tempclone.transform.SetParent(this.transform); //현재위치 값 세팅
            tempclone.transform.localPosition = offsetpos;
        }
    }

    void GeneratorTree()//식물 랜덤하게 나타나도록 함
    {
        int randomindex = 0;
        int randomval = 0; //비교
        GameObject tempclone = null;
        Vector3 offsetpos = Vector3.zero;


        for (int i = StartMinVal; i < StartMaxVal; ++i)//길이 증가하면서 랜덤하게 생성
        {
            randomval = Random.Range(0, 100);
            if (randomval < SpawnCreateRandom)
            {
                randomindex = Random.Range(0, EnviromentObjectList.Count);
                tempclone = GameObject.Instantiate(EnviromentObjectList[randomindex].gameObject);
                tempclone.SetActive(true);
                offsetpos.Set(i, 1f, 0f);

                tempclone.transform.SetParent(this.transform); //현재위치 값 세팅
                tempclone.transform.localPosition = offsetpos;
            }
        }

        //어떤 애를 기준으로 만들것인지
    }
    void GeneratorEnviroment() 
    {
        if (this.transform.position.z <= -4)
        {
            GeneratorBackBlock(); //뒤에 다막아버림
        }
        else if (this.transform.position.z <= 5)
        {
            GeneratorRoundBlock();//주변만 없애버림
        }
        else
        {
            GeneratorTree();
        }
    }
    void Start () {
        GeneratorEnviroment();

    }
	
	
	void Update () {
		
	}
}
