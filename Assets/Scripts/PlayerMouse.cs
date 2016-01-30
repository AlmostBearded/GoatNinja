using UnityEngine;
using System.Collections;

public class PlayerMouse : MonoBehaviour
{
  public GameObject sliceMarkerPrefab;
  private GameObject sliceMarker;
  private Plane playArea;

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
  }

  public void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      sliceMarker = Instantiate(sliceMarkerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    }
    if (Input.GetButton("Fire1"))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      float playAreaDistance;
      if (playArea.Raycast(ray, out playAreaDistance))
      {
        Vector3 playAreaIntersection = ray.GetPoint(playAreaDistance);
        sliceMarker.transform.position = playAreaIntersection;
      }
    }
    if (Input.GetButtonUp("Fire1"))
    {
      Destroy(sliceMarker);
    }
  }
}
