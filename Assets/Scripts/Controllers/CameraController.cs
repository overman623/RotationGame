using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  [SerializeField]
  Define.CameraMode _mode = Define.CameraMode.QuarterView;
  [SerializeField]
  Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
  [SerializeField]
  Quaternion _quaternion = Quaternion.Euler(40, 0, 0);
  [SerializeField]
  GameObject _player = null;

  public void SetPlayer(GameObject player)
  {
    _player = player;
  }

  void Start()
  {
  }

  //카메라의 이동을 무조건 플레이어 업데이트 끝난 이후에

  void LateUpdate()
  {

    if (_mode == Define.CameraMode.QuarterView)
    {

      if (_player == null || _player.IsValid() == false)
      {
        return;
      }

      RaycastHit hit;
      if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
      {
        float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
        transform.position = _player.transform.position + _delta.normalized * dist;
      }
      else
      {
        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform); //카메라가 플레이어의 좌표 주시
        transform.rotation = _quaternion; //카메라의 각도 조절
      }

    }

  }

  public void SetQuaterView(Vector3 delta)
  {
    _mode = Define.CameraMode.QuarterView;
    _delta = delta;
  }
}
