using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour
{
  public GameObject bloodExit;
  public GameObject bloodEnter;
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

      SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
      if (skinnedMeshRenderer != null)
      {
        Mesh mesh = new Mesh();
        Material[] materials = skinnedMeshRenderer.materials;
        skinnedMeshRenderer.BakeMesh(mesh);
        Destroy(skinnedMeshRenderer);
        Destroy(GetComponent<Animator>());

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
        renderer.materials = materials;
        transform.localScale = new Vector3(1, 1, 1);
      }
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

        cutter.Cut(slicePlane);

        Instantiate(bloodExit, enterPoint, Quaternion.LookRotation(currPos - enterPoint));
        Instantiate(bloodEnter, enterPoint, Quaternion.LookRotation(enterPoint - currPos));
      }
    }
  }
}
