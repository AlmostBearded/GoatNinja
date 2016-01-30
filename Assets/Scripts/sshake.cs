using UnityEngine;
using System.Collections;

public class sshake : MonoBehaviour
{
    public static Camera main;
    public AnimationCurve curve;
    public float shake = 0;
    public float shakeAmount;
    public float decreaseFactor;
    public Vector3 pos;

    public void shakeIt(float s)
    {
        shake = s;
        pos = main.transform.localPosition;
    }

    void Start()
    {
        shake = 0;
        decreaseFactor = 0.5f;
        shakeAmount = 0.010f;
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    
    void Update()
    {
        if (shake > 0)
        {
            main.transform.localPosition = Random.insideUnitSphere * shakeAmount * shake;
            shake -= Time.deltaTime * decreaseFactor;
        }
        if(shake < 0)
        {
            main.transform.localPosition = pos;
            shake = 0.0f;
        }
    }

}