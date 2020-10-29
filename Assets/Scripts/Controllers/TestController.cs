using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestController : BaseController, IPointerClickHandler
{
  [SerializeField]
  Stat _stat;

  private UI_JoyStick _Joystick;//Joystick reference for assign in inspector
  [SerializeField] private float Speed = 10;

  [SerializeField] private GameObject BulletPos = null;
  [SerializeField] private GameObject BulletCasePos = null;
  [SerializeField] private GameObject SwordPos = null;
  [SerializeField] private float SwordRange = 0;
  [SerializeField] private BoxCollider SwordCollider = null;

  [SerializeField] private GameObject _weaponPos = null;

  private Weapon _weapon = null;

  private GameObject _character;

  private bool startDrag = false;

  //몬스터가 플레이어에 부딪히면 캐릭터가 밀린다.
  public override void Init()
  {
    WroldObjectType = Define.WorldObject.Player;
    _stat = gameObject.GetComponent<Stat>();

    if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
      Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform); //transform에 붙는다.

    GameObject goRoot = Managers.UI.Root;
    _Joystick = Util.FindChild<UI_JoyStick>(goRoot, "JoyStick", true);

    //무기를 장착 시켜야함.
    //총을 가져와서 총의 위치에 장착 시켜줘야함.

    // _weapon.Parent = gameObject;
    // _weapon.AttachedWeapon(Weapon.WeaponType.Range);

    //무기의 위치를 정의 해야함.
    AttachedWeapon();
  }

  public void AttachedWeapon(int num = 0)
  {

    // Weapon weapon = new Weapon(num);
    // weapon.Attached(_weaponPos); //나중에 사용한다.

    // GameObject gun = Managers.Resource.Instantiate("Weapon/Gun");
    // gun.transform.localPosition = _weaponPos.transform.position;
    // gun.transform.SetParent(_weaponPos.transform);

    GameObject sword = Managers.Resource.Instantiate("Weapon/Sword");
    sword.transform.localPosition = _weaponPos.transform.position;
    sword.transform.localPosition += new Vector3(0, 0, 1);
    sword.transform.rotation = Quaternion.LookRotation(Vector3.zero, new Vector3(0, 0, -45));
    sword.transform.SetParent(_weaponPos.transform);
  }

  public float GetSpeed()
  {
    return Speed;
  }

  protected override void UpdateIdle()
  {
    if (!startDrag && _Joystick.isPress) //버튼을 누르면 move로 바뀌게된다.
    {
      startDrag = true;
      State = Define.State.Moving;
    }

  }
  public void AttackAnimationEnd()
  {
    State = Define.State.Idle;
  }

  protected override void UpdateMoving()
  {
    if (!_Joystick.isPress)
    {
      startDrag = false;
      State = Define.State.Idle;
      return;
    }

    float v = _Joystick.Vertical; //get the vertical value of joystick
    float h = _Joystick.Horizontal; //get the horizontal value of joystick

    Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
    transform.Translate(translate, Space.World);

    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(-h, 180, -v).normalized), 10 * Time.deltaTime);
  }

  public void Attack(bool melee = false) //코루틴으로 구성해도 될것 같다.
  {
    if (!melee)
    {
      State = Define.State.Range;
      // StartCoroutine("AttackRange");
    }
    else
    {
      // 검 장착
      State = Define.State.Melee;
      // StartCoroutine("AttackMelee");
    }
  }

  protected override void UpdateMelee()
  {
    Debug.Log("UpdateMelee");
  }
  protected override void UpdateRange()
  {
    Debug.Log("UpdateRange");
  }
  IEnumerator AttackRange()
  {
    // 원거리 근거리는 몬스터가 들어왔을 사거리로 나누거나.
    // 버튼을 다르게 해서 판단하는게 좋음.

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
  IEnumerator AttackMelee()
  {

    GameObject goSword = Util.FindChild(SwordPos, "Sword");
    goSword.SetActive(true);
    // 애니메이션 재생. //충돌 플래그 on
    Animator anim = GetComponent<Animator>();
    anim.CrossFadeInFixedTime("SWORD", 0.1f);

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

  public void SetItem(Item item) //아이템을 먹은 경우.
  {
    Managers.Data.setItemData(item);
  }

  public void SetWeapon(GameObject weapon)
  {
    _weaponPos = weapon;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("test ok");
  }
}
