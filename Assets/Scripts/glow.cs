using UnityEngine;
using System.Collections;

public class glow : MonoBehaviour {

    public Material[] materials;
    public float changeInterval = 0.33F;

    public Renderer renderer;

    public int highscore;

	void Start ()
    {
        renderer = GameObject.FindGameObjectWithTag("glow").GetComponent<MeshRenderer>();
        highscore = PlayerPrefs.GetInt("highscore", 1200);
    }

    void Update ()
    {
        int currentHighscore = PlayerPrefs.GetInt("lastscore", 0);
        float alpha = 100 * currentHighscore / highscore;
        alpha = alpha * 0.01f;
        Color color = renderer.materials[1].color;
        color.a = alpha;
        renderer.materials[1].color = color;
    }
}
