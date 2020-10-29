using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
  public enum WeaponType
  {
    None,
    Range,
    Melee
  }

  WeaponType _nowWeaponType = WeaponType.None;

  private GameObject _weapon;
  private int num;

  public Weapon(int num)
  {
    this.num = num;
    //무기 번호로 정보를 가져온다.
    //정보중에 근접과 원거리무기를 판단한다. type = range;
    //프리펩을 만들어내야한다.
    //그리고 프리펩을 가져와야 한다.
  }

  public GameObject Attached(GameObject parent)
  {
    WeaponType type = WeaponType.Range;
    switch (type)
    {
      case WeaponType.Range:
        GameObject gun = Managers.Resource.Instantiate("Weapon/Gun");
        gun.transform.localPosition = parent.transform.position;
        gun.transform.SetParent(parent.transform);
        return gun;
      case WeaponType.Melee:
        GameObject sword = Managers.Resource.Instantiate("Weapon/Sword");
        sword.transform.localPosition = parent.transform.position;
        sword.transform.localPosition += new Vector3(0, 0, 1);
        sword.transform.SetParent(parent.transform);
        return sword;
      case WeaponType.None:
        return null;
    }
    return null;
  }

}
