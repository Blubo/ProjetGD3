using UnityEngine;
using System.Collections;

public class SingletonTargetCamera : MonoBehaviour
{

  private static SingletonTargetCamera instance = null;
  public static SingletonTargetCamera Instance
  {
    get { return instance; }
  }

  void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(this.gameObject);
      return;
    }
    else
    {
      instance = this;
    }
    DontDestroyOnLoad(this.gameObject);
  }
}