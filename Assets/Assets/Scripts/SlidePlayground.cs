using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlayground : MonoBehaviour
{
    private Renderer MyRenderer;

    public float ScrollX = 0.0F;
    public float ScrollY = -0.5F;

    private float HoleRestPos = -0.548F;


    // Start is called before the first frame update
    void Start()
    {
        MyRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float OffsetX = Time.time * ScrollX;
        float OffsetY = (Time.time * ScrollY) + GameManager.GetPlayspeed();

        //Debug.Log("Speed " + GameManager.GetPlayspeed());

        //MyRenderer.material.mainTextureOffset = new Vector2 (OffsetX, OffsetY);
        //MyRenderer.material.mainTextureOffset = Vector2.Lerp (MyRenderer.material.mainTextureOffset, new Vector2(OffsetX, OffsetY), Time.deltaTime * GameManager.GetPlayspeed());
        //MyRenderer.material.SetVector("_HolePosition", new Vector2(OffsetX, Time.time * OffsetY));
        //MyRenderer.material.SetVector("_HolePosition", Vector2.Lerp (MyRenderer.material.mainTextureOffset, new Vector2(OffsetX, OffsetY), Time.deltaTime * GameManager.GetPlayspeed()));

    }
}
