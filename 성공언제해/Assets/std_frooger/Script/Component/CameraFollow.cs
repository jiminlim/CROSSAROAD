using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    public float Smoothing = 5f; //따라오는 속도
    Vector3 m_OffsetVal; //비교

    void Start () {
        m_OffsetVal = transform.position - Target.position;
    }
	
	
	void Update () {
        if (Target != null)
        {
            Vector3 targetcamerapos = Target.position + m_OffsetVal;

            transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);
        }
    }
}
