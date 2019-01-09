using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDragBTN : MonoBehaviour
   , IBeginDragHandler
    , IEndDragHandler
    , IPointerDownHandler //드래그를 했을때
    , IPointerUpHandler //손에서 뗐을때{
{
    private int point = 1;
    public int Point
    {
        set { point = value; }
        get { return point; }
    }
    public ActorMove PlayerMove = null;
    Vector2 m_BeginPoint = Vector2.zero;
    Vector2 m_EndPoint = Vector2.zero;
    bool m_ISDragBegin = false;
    public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        m_ISDragBegin = true;
        m_BeginPoint = eventData.position;

    }

    public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Vector2 offsetpos = eventData.position - m_BeginPoint;
        if(offsetpos.sqrMagnitude<=0.01) //드래그를 너무 작게 하면
        {
            SetActorMove(ActorMove.E_DirectionType.Up);
           
            return;
        }
        offsetpos.Normalize();

        if (offsetpos.y >= 0)
        {
            if (offsetpos.x < -0.1f)
            {
                SetActorMove(ActorMove.E_DirectionType.Left);
            }
            else if(offsetpos.x <= 0.3f)
            {
                SetActorMove(ActorMove.E_DirectionType.Right);
            }
            else {
                SetActorMove(ActorMove.E_DirectionType.Up);
                

            }
        }
        else
        {
            if (offsetpos.x < -0.1f)
            {
                SetActorMove(ActorMove.E_DirectionType.Left);
            }
            else if(offsetpos.x>0.3f)
            {
                SetActorMove(ActorMove.E_DirectionType.Right);
            }
            else
            {
                SetActorMove(ActorMove.E_DirectionType.Down);
            }
        }

    }

    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {

        m_ISDragBegin = false;
    }

    public void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (m_ISDragBegin)
        {
            return;
        }
        SetActorMove(ActorMove.E_DirectionType.Up);
        CRGameManager.Instance.Point += point;
        if (CRGameManager.Instance.BestPoint <= CRGameManager.Instance.Point)
        {
            CRGameManager.Instance.BestPoint = CRGameManager.Instance.Point;
        }
    }

    public void SetActorMove(ActorMove.E_DirectionType p_movetype)
    {
        PlayerMove._On_ActorMove((int)p_movetype);


    }

}
