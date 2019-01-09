using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentMapMananger : MonoBehaviour {

    public enum E_EnviromentType //여러개 같이 복제하기 위해서 
    {
        Grass = 0, Road, Water,
        Max
    }
    public enum E_LastRoadType //grass 나오고 나서 도로나 물길 생기게 하기위해
    {//도로에서 바로 물길은 별로니까
        Grass = 0, Road,
        Max
    }

    //public GameObject[] EnviromentObjectArray;
    [Header("복제용길")]
    public Road DefaultRoad = null;
    public Road WaterRoad = null;
    public GrassSpan GrassRoad = null;


    public Transform ParentTranform = null;
    public int MinPosZ = -20; //땅길이
    public int MaxPosZ = 20;

    public int FrontOffSetPosZ = 20;
    public int BackOffSetPosZ = 10;
    void Start ()
    {
        DefaultRoad.gameObject.SetActive(false);
        WaterRoad.gameObject.SetActive(false);
        GrassRoad.gameObject.SetActive(false);
	}


    public int GroupRandomRoadLine(int p_posz) //그룹형태로 도로 여러개만들수있게됨.
    {
        int randomcount = Random.Range(1, 4);
        for (int i = 0; i < randomcount; ++i)
        {
            GenerateRoadLine(p_posz + i);

        }
        return randomcount;
    }
    public int GroupRandomWaterLine(int p_posz)
    {
        int randomcount = Random.Range(1, 4);
        for (int i = 0; i < randomcount; ++i)
        {
            GenerateWaterLine(p_posz + i);

        }
        return randomcount;
    }
    public int GroupRandomGrassLine(int p_posz)
    {
        int randomcount = Random.Range(1, 3);
        for (int i = 0; i < randomcount; ++i)
        {
            GenerateGrassLine(p_posz + i);

        }
        return randomcount;
    }


    public void GenerateRoadLine(int p_posz) //road 생성
    {
        GameObject cloneobj = GameObject.Instantiate(DefaultRoad.gameObject);

        cloneobj.SetActive(true);

            Vector3 offsetpos = Vector3.zero;
            offsetpos.z = (float)p_posz;
            offsetpos.y = -1f; //임의로 계속 땅위 위치가 올라가서 내려줌
            cloneobj.transform.SetParent(ParentTranform);
            cloneobj.transform.position = offsetpos;

            int randomrot = Random.Range(0, 2);
            if (randomrot == 1)
            { //일이면 방향 바꾸기
                cloneobj.transform.rotation = Quaternion.Euler(0, 180f, 0f);
            }
            cloneobj.name = "Roadline_" + p_posz.ToString();
            m_LineMapList.Add(cloneobj.transform);



    }
    public void GenerateWaterLine(int p_posz) // water 생성
    {
        GameObject cloneobj = GameObject.Instantiate(WaterRoad.gameObject);
        cloneobj.SetActive(true);

        Vector3 offsetpos = Vector3.zero;
            offsetpos.z = (float)p_posz;
            offsetpos.y = -1f; //임의로 계속 땅위 위치가 올라가서 내려줌
            cloneobj.transform.SetParent(ParentTranform);
            cloneobj.transform.position = offsetpos;

            int randomrot = Random.Range(0, 2);
            if (randomrot == 1)
            { //일이면 방향 바꾸기
                cloneobj.transform.rotation = Quaternion.Euler(0, 180f, 0f);
            }
            cloneobj.name = "Waterline_" + p_posz.ToString();
            m_LineMapList.Add(cloneobj.transform);
            m_LineMapDic.Add(p_posz, cloneobj.transform);
        
        

    }
    public void GenerateGrassLine(int p_posz) //grass 생성
    {
        GameObject cloneobj=GameObject.Instantiate(GrassRoad.gameObject);

        cloneobj.SetActive(true);

        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = (float)p_posz;
        offsetpos.y = -1f; //임의로 계속 땅위 위치가 올라가서 내려줌
        cloneobj.transform.SetParent(ParentTranform);
        cloneobj.transform.position = offsetpos;

        cloneobj.name = "Grassline_" + p_posz.ToString();
        m_LineMapList.Add(cloneobj.transform);
        m_LineMapDic.Add(p_posz, cloneobj.transform);
    }

    protected E_LastRoadType m_LastRoadType = E_LastRoadType.Max;
    protected List<Transform> m_LineMapList = new List<Transform>();
    protected Dictionary<int, Transform> m_LineMapDic = new Dictionary<int, Transform>(); //지워야 할것이 생기기때무네
    protected int m_LastLinPos = 0;

    [SerializeField]
    protected int m_MinLine = 0;
    public int m_DeleteLine = 10; //얼마나 지울지
    public int m_BackOffLineCount = 30; 
    public void UpdateForwardNBackMove(int p_poz)//캐릭터의 위치에따라 만든닷
    {
        if (m_LineMapList.Count <= 0)
        {
            m_LastRoadType = E_LastRoadType.Grass;
            m_MinLine = MinPosZ;
            int i;
            //초기 라인 세팅
            for (i= MinPosZ;i< MaxPosZ; ++i)//플레이어위치 z=0f
            {
                int offsetval = 0;
                if (i < 4)//잔디만
                {
                    GenerateGrassLine(i);
                }
                else //그외엔 랜덤하게
                {
                    if (m_LastRoadType == E_LastRoadType.Grass)
                    {
                        int randomval = Random.Range(0, 2);
                        if (randomval == 0)
                        {
                            offsetval = GroupRandomWaterLine(i);
                        }
                        else
                        {
                            offsetval = GroupRandomRoadLine(i);
                        }
                        m_LastRoadType = E_LastRoadType.Road;
                    }
                    else
                    {
                        offsetval = GroupRandomGrassLine(i);
                        m_LastRoadType = E_LastRoadType.Grass;
                    }

                    i += offsetval - 1;
                }
            }
            m_LastLinPos = i;//i 값받아오기위해
        }
        
        //새롭게 생성
        if(m_LastLinPos<p_poz+ FrontOffSetPosZ)
        {
            int offsetval = 0;
            if (m_LastRoadType == E_LastRoadType.Grass)
            {
                int randomval = Random.Range(0, 2);
                if (randomval == 0)
                {
                    offsetval = GroupRandomWaterLine(m_LastLinPos);
                }
                else
                {
                    offsetval = GroupRandomRoadLine(m_LastLinPos);
                }
                m_LastRoadType = E_LastRoadType.Road;
            }
            else
            {
                offsetval = GroupRandomGrassLine(m_LastLinPos);
                m_LastRoadType = E_LastRoadType.Grass;
            }
            m_LastLinPos += offsetval;
        }

        //많이 지나갔으면 지우기
        if(p_poz - m_BackOffLineCount> m_MinLine - m_DeleteLine) // 몇이상되면 지우겠다/현재위치값이크면
        {
           int count = m_MinLine + m_DeleteLine;
            for(int i= m_MinLine; i < count; ++i)
            {
                RemoveLine(i);
            }
            m_MinLine += m_DeleteLine;

        }
    }
	
    void RemoveLine(int p_posz)
    {
        if (m_LineMapDic.ContainsKey(p_posz))
        {
            Transform transobj = m_LineMapDic[p_posz];
            GameObject.Destroy(transobj.gameObject);
            m_LineMapList.Remove(transobj);
            m_LineMapDic.Remove(p_posz);
        }
        else
        {
            Debug.LogErrorFormat("removeline error :{0}", p_posz);
        }
    }

}
