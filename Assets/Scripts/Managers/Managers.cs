using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

  static Managers s_Instance;
  static Managers Instance { get { init(); return s_Instance; } }

  #region Content
  GameManagerEx _game = new GameManagerEx();

  public static GameManagerEx Game { get { return Instance._game; } }
  #endregion

  #region Core
  DataManager _data = new DataManager();
  InputManager _input = new InputManager();
  PoolManager _pool = new PoolManager();
  ResourceManager _resource = new ResourceManager();
  SceneManagerEx _scene = new SceneManagerEx();
  SoundManager _sound = new SoundManager();
  UIManager _ui = new UIManager();

  public static DataManager Data { get { return Instance._data; } }
  public static InputManager Input { get { return Instance._input; } }
  public static PoolManager Pool { get { return Instance._pool; } }
  public static ResourceManager Resource { get { return Instance._resource; } }
  public static SceneManagerEx Scene { get { return Instance._scene; } }
  public static SoundManager Sound { get { return Instance._sound; } }
  public static UIManager UI { get { return Instance._ui; } }
  #endregion

  void Start()
  {
    init();
  }

  void Update()
  {
    _input.OnUpdate();
  }

  static void init()
  {

    if (s_Instance == null)
    {
      GameObject go = GameObject.Find("@Managers");
      if (go == null)
      {
        go = new GameObject { name = "@Managers" };
        go.AddComponent<Managers>();
      }
      DontDestroyOnLoad(go); //왠만해서는 삭제되지 않도록 추가함.
      s_Instance = go.GetComponent<Managers>();

      s_Instance._data.Init(); //클리어 할 필요는 없다.
      s_Instance._sound.init();
      s_Instance._pool.init();
    }

  }

  public static void Clear()
  {
    Input.Clear();
    Sound.Clear();
    Scene.Clear();
    UI.Clear();
    Pool.Clear();
  }

}
