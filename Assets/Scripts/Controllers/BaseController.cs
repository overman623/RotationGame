using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseController : MonoBehaviour
{
  public Define.WroldObject WroldObjectType { get; protected set; } = Define.WroldObject.Unknown;

  [SerializeField]
  protected Vector3 _destPos;

  [SerializeField]
  Define.State _state = Define.State.Idle;
  [SerializeField]
  protected GameObject _lockTarget;
  public virtual Define.State State
  {
    get { return _state; }
    set
    {
      _state = value;
      Animator anim = GetComponent<Animator>();
      switch (_state)
      {
        case Define.State.Die:
          break;
        case Define.State.Idle:
          anim.CrossFade("WAIT", 0.1f);
          break;
        case Define.State.Moving:
          anim.CrossFade("RUN", 0.1f);
          break;
        case Define.State.Skill:
          anim.CrossFade("ATTACK", 0.1f, -1, 0); //첫 위치부터 다시 재생(반복재생)
          break;
      }
    }
  }

  private void Start()
  {
    Init();
  }

  void Update()
  {
    switch (State)
    {
      case Define.State.Die:
        UpdateDie();
        break;
      case Define.State.Moving:
        UpdateMoving();
        break;
      case Define.State.Idle:
        UpdateIdle();
        break;
      case Define.State.Skill:
        UpdateSkill();
        break;
    }
  }

  public abstract void Init();

  protected virtual void UpdateDie() { }
  protected virtual void UpdateMoving() { }
  protected virtual void UpdateIdle() { }
  protected virtual void UpdateSkill() { }

}