using UnityEngine;
using System.Collections;

public class SingletonVert : MonoBehaviour
{

  private static SingletonVert instance = null;
  public static SingletonVert Instance
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