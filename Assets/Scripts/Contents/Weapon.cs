using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
  public enum WeaponType
  {
    None,
    Gun,
    Sword
  }

  WeaponType _nowWeaponType = WeaponType.None;
  private GameObject _charactor;
  public GameObject Parent
  {
    set
    {
      _charactor = value;
    }
  }

  private GameObject _weapon;

  public GameObject AttachedWeapon(WeaponType type)
  {
    if (_charactor == null)
    {
      return null;
    }
    _nowWeaponType = type;
    // string name = System.Enum.GetName(typeof(WeaponType), _nowWeaponType);
    // Debug.Log(name);
    GameObject goWeapon = Util.FindChild(_charactor, "Weapon");
    switch (type)
    {
      case WeaponType.Gun:
        GameObject gun = Managers.Resource.Instantiate("Weapon/Gun");
        gun.transform.SetParent(goWeapon.transform);
        gun.transform.position = new Vector3(0.45f, 1, 0.5f);
        _weapon = gun;
        return gun;
      case WeaponType.Sword:
        return null;
      case WeaponType.None:
        return null;
    }
    return null;
  }

}
