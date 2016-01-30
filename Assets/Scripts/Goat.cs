using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour {

  private Vector3 enterPoint;
  private Plane playArea;

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
  }

  public void OnCollisionEnter(Collision c)
  {
    enterPoint = c.contacts[0].point;
  }

  public void OnCollisionExit(Collision c)
  {
  }

  public void OnCollisionStay(Collision c)
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    float playAreaDistance;
    if (playArea.Raycast(ray, out playAreaDistance))
    {
      Vector3 currPos = ray.GetPoint(playAreaDistance);

      Vector3 mouseToEnter = (enterPoint - ray.origin).normalized;
      Vector3 mouseToCurr = (currPos - ray.origin).normalized;
      Vector3 slicePlaneNormal = Vector3.Cross(mouseToEnter, mouseToCurr);

      Plane slicePlane = new Plane(slicePlaneNormal, enterPoint);
      Debug.DrawLine(enterPoint, enterPoint + slicePlane.normal * 3, Color.white, 2, false);
    }

  }
}
