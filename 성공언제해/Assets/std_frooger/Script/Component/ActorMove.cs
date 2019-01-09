using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour {


    public Rigidbody ActorBody = null;
    public EnviromentMapMananger EnviromentMapManagerCom = null;


    void Start () {

        string[] templayer = new string[] { "Plant" };
        m_TreeLayerMask = LayerMask.GetMask(templayer);

        EnviromentMapManagerCom.UpdateForwardNBackMove((int)this.transform.position.z);//길만들기 호출
    }
    public enum E_DirectionType
    { Up = 0, Down, Left, Right }
    protected E_DirectionType m_DirectionType = E_DirectionType.Up;
    protected int m_TreeLayerMask = -1;
    protected bool ISCheckDirectionViewMove(E_DirectionType p_moveType)
    {
        Vector3 direction = Vector3.zero;
        switch (p_moveType)
        {
            case E_DirectionType.Up:
                {
                    direction = Vector3.forward;
                }
                break;
            case E_DirectionType.Down:
                {
                    direction = Vector3.back;
                }
                break;
            case E_DirectionType.Left:
                {
                    direction = Vector3.left;
                }
                break;
            case E_DirectionType.Right:
                {
                    direction = Vector3.right;
                }
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_moveType);
                break;
        }
        RaycastHit hitobj; //장애물이 있으면 못지나가도ㅗㄱ

            if (Physics.Raycast(this.transform.position, direction, out hitobj, 1f, m_TreeLayerMask))
            { //layerMask 충돌을 무시하려고 할때
                return false; //앞에 뭐가 있으면 안리턴
        }
        else { return true; }
            




    }

    [EnumAction(typeof(E_DirectionType))] //ui를 int값말고 enum으로 받아주기위해
    public void _On_ActorMove(int p_movetype) //소스 받아온거 써줌
    {
        SetActorMove((E_DirectionType)p_movetype);
    }
    public Transform ChildModel = null; //움직일때 회전
    void SetDirectionRot(E_DirectionType p_moveType)
    {
        switch (p_moveType)
        {
            case E_DirectionType.Up:
                ChildModel.rotation = Quaternion.identity;
                break;
            case E_DirectionType.Down:
                ChildModel.rotation = Quaternion.Euler(0,180,0);
                break;
            case E_DirectionType.Left:
                ChildModel.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case E_DirectionType.Right:
                ChildModel.rotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                break;
        }
    } //캐릭터회전
    protected void SetActorMove(E_DirectionType p_moveType) //사용자 키보드 누를때 움직이는 방향
    {
        if (! ISCheckDirectionViewMove(p_moveType)){
            return;
        }
        Vector3 offsetpos = Vector3.zero;
        switch (p_moveType)
        {
            case E_DirectionType.Up:
                {
                    offsetpos = Vector3.forward;
                }
                break;
            case E_DirectionType.Down:
                {
                    offsetpos = Vector3.back;
                }
                break;
            case E_DirectionType.Left:
                {
                    offsetpos = Vector3.left;
                }
                break;
            case E_DirectionType.Right:
                {
                    offsetpos = Vector3.right;
                }
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_moveType);
                break;
        }
        SetDirectionRot(p_moveType);

        this.transform.position += offsetpos;
        m_RaftOffsetpos += offsetpos;

        EnviromentMapManagerCom.UpdateForwardNBackMove((int)this.transform.position.z);//길만들기 호출
        //키보드 누를때마다 길생성해야하므로
       
    }

    
   /*protected void InputUpdate()//키보드 눌렀을때
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(E_DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(E_DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(E_DirectionType.Right);
        }
    }*/
    Vector3 m_RaftOffsetpos = Vector3.zero;
    protected void UpdateRaft()
    {
        if (RaftObject == null) //뗏목 빠져나오면 반환
        {
            return;
        }
        //현재 플레이어 위치 
        Vector3 actorpos = RaftObject.transform.position + m_RaftOffsetpos;
        this.transform.position = actorpos;
    }
    void Update()
    {
        if (CRGameManager.Instance.IsGameOver) return;
        //InputUpdate(); //ㅋㅣ보트 입력
        UpdateRaft();
        
    }

    [Header("[확인용]"),SerializeField]
    protected Raft RaftObject = null;
    protected Transform RaftCompareObj = null;

    protected void OnTriggerEnter(Collider other)//차에 부딪힐때 //뗏목에 올라탔을때
    {
        Debug.LogFormat("OnTriggerEnter:{0},{1}", other.name, other.tag);

        if (other.tag.Contains("Raft"))
        {
            RaftObject = other.transform.parent.GetComponent<Raft>();
            if (RaftObject != null)
            {
                RaftCompareObj = RaftObject.transform;
                m_RaftOffsetpos = this.transform.position - RaftObject.transform.position;
                //캐릭터의 위치 - 뗏목이 움직이는 위치
            }
            Debug.LogFormat("뗏목 탔다 : {0},{1}", other.name, m_RaftOffsetpos);
            return;
        }


        if (other.tag.Contains("Crash")) //layer에 해당되면
        {
            Debug.LogFormat("부딪혔다 : gameover");
            CRGameManager.Instance.IsGameOver = true;
            Destroy(this.gameObject);
            int bestPoint = 0;
            bestPoint = CRGameManager.Instance.Point;
            PlayerPrefs.SetInt("BestPoint", bestPoint);
        }

        if (other.tag.Contains("River"))
        {
            Debug.LogFormat("떨어졌다 : gameover");
            CRGameManager.Instance.IsGameOver = true;
            Destroy(this.gameObject);
            int bestPoint = 0;
            bestPoint = CRGameManager.Instance.Point;
            PlayerPrefs.SetInt("BestPoint", bestPoint);
        }
    }
    protected void OnTriggerExit(Collider other) //뗏목에서 내리면 영향 안가도록 빠져나옴
    {
        Debug.LogFormat("OnTriggerExit:{0},{1}", other.name, other.tag);
        if (other.tag.Contains("Raft") && RaftCompareObj == other.transform.parent)
        {
            RaftCompareObj = null;
            RaftObject = null;
            m_RaftOffsetpos = Vector3.zero;
        }


    }
}
