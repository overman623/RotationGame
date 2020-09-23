using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{

  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);// mask bit flag 로 계산
  PlayerStat _stat;

  public override void Init()
  {
    WroldObjectType = Define.WroldObject.Player;
    _stat = gameObject.GetComponent<PlayerStat>();
    Managers.Input.MouseAction -= OnMouseEvent; //다른데서 구독 신청하면 끊어버림
    Managers.Input.MouseAction += OnMouseEvent; //구독 추가
    if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
      Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform); //transform에 붙는다.
  }

  protected override void UpdateMoving()
  {
    //몬스터가 내 사정거리보다 가까우면 공격
    if (_lockTarget != null)
    {
      _destPos = _lockTarget.transform.position;
      float distance = (_destPos - transform.position).magnitude;
      if (distance <= 1)
      {
        State = Define.State.Skill;
        return;
      }
    }


    Vector3 dir = _destPos - transform.position;
    dir.y = 0;
    if (dir.magnitude < 0.1f) //dir이 아주 작은 값인지 체크해서 목적지에 도착했는지 판단한다.
    {
      State = Define.State.Idle;
    }
    else
    {
      Debug.DrawRay(transform.position, dir.normalized, Color.green);
      if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
      {
        if (Input.GetMouseButton(0) == false)
          State = Define.State.Idle;
        return;
      }
      float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
      transform.position += dir.normalized * moveDist;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
    }
  }

  void OnHitEvent()
  {
    if (_lockTarget != null)
    {
      Stat targetStat = _lockTarget.GetComponent<Stat>();
      targetStat.OnAttacked(_stat);
    }

    if (_stopSkill)
    {
      State = Define.State.Idle;
    }
    else
    {
      State = Define.State.Skill;
    }
  }

  protected override void UpdateSkill()
  {
    if (_lockTarget != null)
    {
      Vector3 dir = _lockTarget.transform.position - transform.position;
      Quaternion quat = Quaternion.LookRotation(dir);
      transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
    }
  }

  bool _stopSkill = false;
  void OnMouseEvent(Define.MouseEvent evt)
  {

    switch (State)
    {
      case Define.State.Idle:
        OnMouseEvent_IdleRun(evt);
        break;
      case Define.State.Moving:
        OnMouseEvent_IdleRun(evt);
        break;
      case Define.State.Skill:
        {
          if (evt == Define.MouseEvent.PointerUp)
            _stopSkill = true;
        }
        break;
    }

  }

  void OnMouseEvent_IdleRun(Define.MouseEvent evt)
  {

    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask); //100.0f 거리를 정한다. 
    // Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 1.0f -> 1초만 유지

    switch (evt)
    {
      case Define.MouseEvent.PointerDown:
        {
          if (raycastHit)
          {
            State = Define.State.Moving;
            _destPos = hit.point;
            _stopSkill = false;
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
              _lockTarget = hit.collider.gameObject;
            else
              _lockTarget = null;
          }
        }
        break;
      case Define.MouseEvent.Press:
        {
          if (_lockTarget == null && raycastHit)
            _destPos = hit.point;
        }
        break;
      case Define.MouseEvent.PointerUp:
        {
          _stopSkill = true;
        }
        break;

    }

  }

}
