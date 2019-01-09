using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public Transform CloneTarget = null;
    public Transform GenerationPos = null;
    public int GenerationPersent = 350;
    public float CloneDelaySec = 1f;
    protected float NextSecToClone = 0f;

    void Start () {
		
	}
	
	
	void Update () {
        float currSec = Time.time;

        if (NextSecToClone <= currSec) //차 복제
        {
            int randomval = Random.Range(0, 100);
            if (randomval <= GenerationPersent)
            {
                CloneCar();
            }
            NextSecToClone = currSec + CloneDelaySec; //현재시간과 클론시간
        }
    }
    void CloneCar()
    {
        Transform clonepos = GenerationPos;
        Vector3 offsetpos = clonepos.position;
        offsetpos.y = 0f;
        GameObject cloneobj = GameObject.Instantiate(CloneTarget.gameObject, offsetpos, GenerationPos.rotation, this.transform);
        cloneobj.SetActive(true);
    }
}
