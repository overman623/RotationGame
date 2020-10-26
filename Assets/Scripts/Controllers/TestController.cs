using System.Collections;
using UnityEngine;

public class TestController : BaseController
{
  [SerializeField]
  Stat _stat;

  private UI_JoyStick _Joystick;//Joystick reference for assign in inspector
  [SerializeField] private float Speed = 10;

  [SerializeField] private GameObject BulletPos = null;
  [SerializeField] private GameObject BulletCasePos = null;
  [SerializeField] private GameObject SwordPos = null;
  [SerializeField] private float SwordRange;
  [SerializeField] private BoxCollider SwordCollider;

  private Weapon _weapon = new Weapon();

  private GameObject _character;

  //몬스터가 플레이어에 부딪히면 캐릭터가 밀린다.
  public override void Init()
  {
    WroldObjectType = Define.WorldObject.Player;
    _stat = gameObject.GetComponent<Stat>();

    if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
      Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform); //transform에 붙는다.

    GameObject goRoot = Managers.UI.Root;
    _Joystick = Util.FindChild<UI_JoyStick>(goRoot, "JoyStick", true);
    _Joystick.StateAction -= JoyStickAction;
    _Joystick.StateAction += JoyStickAction;

    _weapon.Parent = gameObject;
    _weapon.AttachedWeapon(Weapon.WeaponType.Gun);

    Managers.Input.ClickAction -= DirectionChange;
    Managers.Input.ClickAction += DirectionChange;
  }

  internal void SetCharater(GameObject character)
  {
    _character = character;
  }

  void Update()
  {
    if (!_Joystick.isPress) return;

    float v = _Joystick.Vertical; //get the vertical value of joystick
    float h = _Joystick.Horizontal; //get the horizontal value of joystick

    Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
    transform.Translate(translate, Space.World);

    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(translate.normalized), 10 * Time.deltaTime);
  }

  public float GetSpeed()
  {
    return Speed;
  }

  void JoyStickAction(bool isDragStart)
  {
    Animator anim = _character.GetComponent<Animator>();
    if (isDragStart)
    {
      Debug.Log("start Drag");
      anim.CrossFade("WALK", 0.1f);
    }
    else
    {
      Debug.Log("end Drag");
      anim.CrossFade("WAIT", 0.1f);

    }
  }

  void DirectionChange(Define.ClickEvent evt) // 보류
  {
    //조이스틱을 클릭했을때 잡아야한다.

    // Animator anim = _character.GetComponent<Animator>();
    // if (evt == Define.ClickEvent.Press && _Joystick.isPress)
    // {
    //   anim.CrossFade("WALK", 0.1f);
    //   Debug.Log("press");
    // }
    // else if (evt == Define.ClickEvent.Up && _Joystick.isFree)
    // {
    //   anim.CrossFade("WAIT", 0.1f);
    //   Debug.Log("up");
    // }
  }

  public void Attack(bool melee = false) //코루틴으로 구성해도 될것 같다.
  {
    if (!melee)
    {
      StartCoroutine("AttackGun");
    }
    else
    {
      StartCoroutine("AttackSword");
    }
  }
  IEnumerator AttackGun()
  {
    // 원거리 근거리는 몬스터가 들어왔을 사거리로 나누거나.
    // 버튼을 다르게 해서 판단하는게 좋음.
    Animator _anim = _character.GetComponent<Animator>();
    _anim.CrossFadeInFixedTime("RANGE", 0.1f);

    GameObject goBullet = Managers.Resource.Instantiate("Weapon/Bullet");
    goBullet.transform.localRotation = BulletPos.transform.rotation;
    goBullet.transform.position = BulletPos.transform.position;
    Rigidbody bulletRigid = goBullet.GetComponent<Rigidbody>();
    bulletRigid.velocity = goBullet.transform.forward * 50;
    // 애니메이션 효과 추가

    GameObject goBulletCase = Managers.Resource.Instantiate("Weapon/BulletCase");
    goBulletCase.transform.localRotation = BulletCasePos.transform.rotation;
    goBulletCase.transform.position = BulletCasePos.transform.position;
    Rigidbody bulletCaseRigid = goBulletCase.GetComponent<Rigidbody>();
    Vector3 caseVec = goBulletCase.transform.forward * Random.Range(-3, -2) + Vector3.left * Random.Range(-3, -2);
    bulletCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    bulletCaseRigid.AddForce(caseVec, ForceMode.Impulse);
    yield return null;

  }
  IEnumerator AttackSword()
  {

    GameObject goSword = Util.FindChild(SwordPos, "Sword");
    goSword.SetActive(true);
    // 애니메이션 재생. //충돌 플래그 on
    Animator anim = GetComponent<Animator>();
    anim.CrossFadeInFixedTime("SWORD", 0.1f);

    Animator _anim = _character.GetComponent<Animator>();
    _anim.CrossFadeInFixedTime("MELEE", 0.1f);

    // GameObject goSword = Util.FindChild(SwordPos, "Sword");
    // goSword.SetActive(false);


    // GameObject goSword = Managers.Resource.Instantiate("Weapon/Sword");
    // goSword.transform.localRotation = SwordPos.transform.rotation;
    // Vector3 v = new Vector3(10, 0, 10);
    // goSword.transform.localRotation = Quaternion.Slerp(SwordPos.transform.rotation, Quaternion.LookRotation(v.normalized), 10);
    // goSword.transform.position = SwordPos.transform.position;
    // Rigidbody swordRigid = goSword.GetComponent<Rigidbody>();
    // GameObject.Destroy(goSword, 3);
    // swordRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

    // yield return new WaitForSeconds(Random.Range(0, _spawnTime)); // 0~5 사이 숫자 반환 //시간동안 기다린후 다음줄 실행
    yield return null; // 0~5 사이 숫자 반환 //시간동안 기다린후 다음줄 실행

  }

  void AttackEnd()
  {
    GameObject goSword = Util.FindChild(SwordPos, "Sword");
    goSword.SetActive(false);
  }

  public void SetItem(Item item)
  {
    Managers.Data.setItemData(item);
  }

}
