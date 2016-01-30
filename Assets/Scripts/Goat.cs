using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goat : MonoBehaviour
{
  public GameObject bloodExit;
  public GameObject bloodExit2;
  public GameObject bloodEnter;
  public Material bloodMaterial;
  public AudioSource sliceSound;
  private Vector3 enterPoint;
  private Plane playArea;
  private Cutter cutter;
  public bool cut
  {
    get; set;
  }

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
    cutter = GetComponent<Cutter>();
    cut = false;
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
        skinnedMeshRenderer.BakeMesh(mesh);
        Destroy(skinnedMeshRenderer);
        Destroy(GetComponent<Animator>());

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
        renderer.material = bloodMaterial;
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

        Plane slicePlane = cutter.CreateCuttingPlane(enterPoint, currPos, ray.origin);

        GameObject otherGoat = cutter.Cut(slicePlane);
        otherGoat.GetComponent<Goat>().cut = true;
        cut = true;

        Destroy((GameObject)Instantiate(bloodExit, enterPoint, Quaternion.LookRotation(currPos - enterPoint)), 10);
        Destroy((GameObject)Instantiate(bloodExit2, enterPoint, Quaternion.LookRotation(currPos - enterPoint)), 10);
        Destroy((GameObject)Instantiate(bloodEnter, enterPoint, Quaternion.LookRotation(enterPoint - currPos)), 10);
        sliceSound.Play();
      }
    }
  }

}
