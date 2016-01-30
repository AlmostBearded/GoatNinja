using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour
{
  public GameObject blood;
  private Vector3 enterPoint;
  private Plane playArea;
  private Cutter cutter;
  private Rigidbody rb;

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
    cutter = GetComponent<Cutter>();
    rb = GetComponent<Rigidbody>();
  }

  public void OnTriggerEnter(Collider c)
  {
    if (c.tag == "Player")
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      float playAreaDistance;
      playArea.Raycast(ray, out playAreaDistance);
      enterPoint = ray.GetPoint(playAreaDistance);
    }
  }

  public void OnTriggerExit(Collider c)
  {
    if (c.tag == "Player")
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      float playAreaDistance;
      if (playArea.Raycast(ray, out playAreaDistance))
      {
        Vector3 currPos = ray.GetPoint(playAreaDistance);

        Vector3 mouseToEnter = (enterPoint - ray.origin).normalized;
        Vector3 mouseToCurr = (currPos - ray.origin).normalized;
        Vector3 slicePlaneNormal = Vector3.Cross(mouseToEnter, mouseToCurr);

        Plane slicePlane = cutter.CreateCuttingPlane(enterPoint, currPos, ray.origin);
        //Debug.DrawLine(enterPoint, currPos, Color.white, 2, false);
        //Debug.DrawLine(enterPoint, ray.origin, Color.white, 2, false);
        //Debug.DrawLine(currPos, ray.origin, Color.white, 2, false);

        cutter.Cut(slicePlane);

        Instantiate(blood, enterPoint, Quaternion.LookRotation(currPos - enterPoint));
      }
    }
  }

  public void OnCollisionEnter(Collision c)
  {
    //Debug.Log("CollisionEnter");
    //enterPoint = c.contacts[0].point;
    //alreadyCut = false;
    //rb.isKinematic = true;
  }

  public void OnCollisionExit(Collision c)
  {
    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //float playAreaDistance;
    //if (!alreadyCut && playArea.Raycast(ray, out playAreaDistance))
    //{
    //  Vector3 currPos = ray.GetPoint(playAreaDistance);

    //  Vector3 mouseToEnter = (enterPoint - ray.origin).normalized;
    //  Vector3 mouseToCurr = (currPos - ray.origin).normalized;
    //  Vector3 slicePlaneNormal = Vector3.Cross(mouseToEnter, mouseToCurr);

    //  Plane slicePlane = cutter.CreateCuttingPlane(enterPoint, currPos, ray.origin);
    //  Debug.DrawLine(enterPoint, currPos, Color.white, 2, false);
    //  Debug.DrawLine(enterPoint, ray.origin, Color.white, 2, false);
    //  Debug.DrawLine(currPos, ray.origin, Color.white, 2, false);
    //  //Debug.DrawLine(enterPoint, enterPoint + slicePlane.normal * 3, Color.white, 2, false);

    //  //cutter.Cut(slicePlane);
    //  alreadyCut = true;
    //  rb.isKinematic = false;
    //  rb.AddForce(Vector3.up * 10);
    //  //rb.isKinematic = true;
    //  //rb.isKinematic = false;
    //}
  }

  public void OnCollisionStay(Collision c)
  {

  }
}
