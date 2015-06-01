using UnityEngine;
using System.Collections;

public class SingletonRed : MonoBehaviour {

  private static SingletonRed instance = null;
  public static SingletonRed Instance
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
