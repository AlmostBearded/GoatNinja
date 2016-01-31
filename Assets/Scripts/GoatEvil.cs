using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoatEvil : MonoBehaviour
{
  public GameObject gas;
  public GameObject bloodExit;
  public GameObject bloodExit2;
  public GameObject bloodEnter;
  public Material bloodMaterial;
  public AudioSource sliceSound;
  public AudioSource bloodSound;
  private Vector3 enterPoint;
  private Plane playArea;
  private Cutter cutter;
  public bool cut
  {
    get; set;
  }
  private bool dead;
  private live hp;
  private GameObject gascloud;

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
    cutter = GetComponent<Cutter>();
    cut = false;
    hp = GameObject.FindGameObjectWithTag("hp").GetComponent<Text>().GetComponent<live>();
    //gascloud = (GameObject)Instantiate(gas, gameObject.transform.position, gameObject.transform.rotation);
    //Destroy(gascloud, 10);

    }

    void Update()
    {
        //gascloud.transform.position = gameObject.transform.position;
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
        otherGoat.GetComponent<GoatEvil>().cut = true;
        if(!cut)
        {
            hp.decreaseHP();
        }
        cut = true;

        Destroy((GameObject)Instantiate(bloodExit, enterPoint, Quaternion.LookRotation(currPos - enterPoint)), 10);
        Destroy((GameObject)Instantiate(bloodExit2, enterPoint, Quaternion.LookRotation(currPos - enterPoint)), 10);
        Destroy((GameObject)Instantiate(bloodEnter, enterPoint, Quaternion.LookRotation(enterPoint - currPos)), 10);
        sliceSound.Play();
        bloodSound.Play();
      }
    }
  }

}
