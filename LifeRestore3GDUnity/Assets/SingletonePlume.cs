using UnityEngine;
using System.Collections;

public class SingletonePlume : MonoBehaviour
{

  private static SingletonePlume instance = null;
  public static SingletonePlume Instance
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