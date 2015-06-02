using UnityEngine;
using System.Collections;

public class SingletonPlayer : MonoBehaviour {

 private static SingletonPlayer instance = null;
 public static SingletonPlayer Instance {
  get { return instance; }
 }

 void Awake() {
  if (instance != null && instance != this) {
   Destroy(this.gameObject);
   return;
  } else {
   instance = this;
  }
  DontDestroyOnLoad(this.gameObject);
 }
}