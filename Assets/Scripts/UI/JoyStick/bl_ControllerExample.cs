using UnityEngine;

public class bl_ControllerExample : MonoBehaviour
{

  /// <summary>
  /// Step #1
  /// We need a simple reference of joystick in the script
  /// that we need add it.
  /// </summary>
  [SerializeField] private UI_JoyStick Joystick;//Joystick reference for assign in inspector

  [SerializeField] private float Speed = 5;
//
  void Start()
  {
    // Screen.SetResolution(720, 1280, true);
  }

  //카메라가 캐릭터를 따라갈 수 있는게 좋다.
  //조이스틱이 너무작게 나왔다.
  //조이스틱이 너무 좌측에만 있다.
  //조이스틱이 눌렸는지 아닌지 상태를 비교해서.. 손을떼어도 캐릭터가 바라보는 방향을 바꾸지 않아야 한다.
  void Update()
  {

    if (!Joystick.isPress) return;

    //Step #2
    //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
    float v = Joystick.Vertical; //get the vertical value of joystick
    float h = Joystick.Horizontal;//get the horizontal value of joystick

    //in case you using keys instead of axis (due keys are bool and not float) you can do this:
    //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

    //ready!, you not need more.
    Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
    transform.Translate(translate, Space.World);

    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(translate.normalized), 10 * Time.deltaTime);
  }
}