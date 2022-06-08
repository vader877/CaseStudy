using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCube : BasicLaneObject
{
    // Start is called before the first frame update

    public enum CubeColours
    {
        DEFAULT = -1,
        RED = 1,
        GREEN = 2,
        BLUE = 3,
    }

    public CubeColours cubeColour;

    public bool markedForScore = false;

    void Start()
    {
        //Colourize(cubeColour);
    }

    public void MarkForScore()
    {
        //Destroy(gameObject);
        markedForScore = true;
    }

    public bool IsMarked()
    {
        return markedForScore;
    }

    public CubeColours getCubeColourEnum()
    {
        return cubeColour;
    }

    public Color getCubeColour()
    {
        switch (cubeColour)
        {
            case CubeColours.RED:
                return Color.red;

            case CubeColours.GREEN:
                return Color.green;

            case CubeColours.BLUE:
                return Color.blue;

            default:
                return Color.black;
        };
    }


    public void Colourize(CubeColours colour)
    {
        cubeColour = colour;
        switch (cubeColour)
        {
            case CubeColours.RED:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                break;

            case CubeColours.GREEN:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
                break;

            case CubeColours.BLUE:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
                break;

            default:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
                break;
        };
    }

    public void RandomizeColor()
    {
        var colors = CubeColours.GetNames(typeof(CubeColours)).Length;
        Colourize((CubeColours) Random.Range(1, colors)); //omit default
    }

    public override void Spawn()
    {
        base.Spawn();

        gameObject.tag = "Cube";
        enabled = true;
        gameObject.transform.parent = null;
        RandomizeColor();
    }

    public void Freeze(GameObject transformParent)
    {
        gameObject.transform.SetParent(transformParent.transform);
        alive = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void UnFreeze()
    {
        gameObject.tag = "DedCube";
        gameObject.transform.parent = null;
        alive = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
