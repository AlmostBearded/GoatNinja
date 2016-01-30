using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyOnTrigger : MonoBehaviour {
    private live hp;

    void  Start ()
    {
        hp = GameObject.FindGameObjectWithTag("hp").GetComponent<Text>().GetComponent<live>();
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "goat")
        {
            if (!c.gameObject.GetComponent<Goat>().cut)
            {
                hp.decreaseHP(); 
            }
        }
        Destroy(c.gameObject);
  }
}
