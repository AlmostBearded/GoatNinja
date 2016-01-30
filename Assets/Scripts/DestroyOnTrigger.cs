using UnityEngine;
using System.Collections;

public class DestroyOnTrigger : MonoBehaviour {
  
  public void OnTriggerEnter(Collider c)
  {
    Destroy(c.gameObject);
  }
}
