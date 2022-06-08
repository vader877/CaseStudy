using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public FloatingJoystick joystick;
    public float MaxMovementRange = 1F;

    private LineRenderer lineRenderer;
    private Vector3[]lineSpeed;
    public int trailResolution = 2;
    public float lag;
    public float sensitivity = 2f;

    private bool play = true;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.positionCount = trailResolution;

        lineSpeed = new Vector3[trailResolution];

        for (int i = 0; i < trailResolution; i++)
        {
            lineSpeed[i] = gameObject.transform.position;
        }
    }

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartPlay()
    {
        play = true;
    }

    public void StopPlay()
    {
        play = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(play)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + joystick.Horizontal / sensitivity, -0.9f, 0.9f), transform.position.y, transform.position.z);

            //Mathf.Clamp(transform.position.x, -0.9f, 0.9f);

        }


        //Debug.Log(direction);
        UpdateTrail();
    }

    public void UpdateTrail()
    {
        Vector3 latestPos = 5 * GameManager.GetPlayspeed() * - transform.forward;

        //because of transformZ option on line render gotta swap axis
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));


        //for(int i = 1; i < trailResolution; i++)    
        //{
        //    lineSpeed[i].Scale(Vector3.one * Mathf.Log(i));
        //}

        for (int i = 1; i < trailResolution; i++)
        {
            //update left
            lineRenderer.SetPosition(i,
                Vector3.SmoothDamp(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i - 1) +
               -transform.forward, ref lineSpeed[i], lag));

            //Vector3 finalPiece = transform.right * Mathf.Lerp(lineRenderer.GetPosition(i).x, lineRenderer.GetPosition(i - 1).x, 2F);
            //lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) - transform.forward * Mathf.Log10(i) / 5 + finalPiece);

        }


        if (lineRenderer.startColor != GameLogic.CurrentColour)
        {
            lineRenderer.endColor = lineRenderer.startColor;
            lineRenderer.startColor = GameLogic.CurrentColour;
        }
    }
}
