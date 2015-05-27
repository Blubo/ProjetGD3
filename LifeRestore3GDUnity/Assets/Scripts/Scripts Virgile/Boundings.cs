using UnityEngine;

public class Boundings : MonoBehaviour
{
  Camera myCamera;

  void Start()
  {
    myCamera = Camera.main;
  }

  void LateUpdate()
  {
   
    float dist = myCamera.transform.InverseTransformPoint(transform.position).z;
    Vector3 localPos = myCamera.transform.InverseTransformPoint(transform.position);

    Vector3 leftBottom = myCamera.ViewportToWorldPoint(new Vector3(0, 0, dist));
    Vector3 rightTop = myCamera.ViewportToWorldPoint(new Vector3(1, 1, dist));
    leftBottom = myCamera.transform.InverseTransformPoint(leftBottom);
    rightTop = myCamera.transform.InverseTransformPoint(rightTop);

    float x = Mathf.Clamp(localPos.x, leftBottom.x+1.5f, rightTop.x-1.5f);
    float y = Mathf.Clamp(localPos.y, leftBottom.y+1.5f, rightTop.y-1.5f);

    transform.position = myCamera.transform.TransformPoint(new Vector3(x, y, localPos.z));
    transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
  }
}