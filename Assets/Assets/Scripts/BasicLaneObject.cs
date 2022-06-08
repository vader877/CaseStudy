using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaneObject : MonoBehaviour, ISpawnable
{
    public bool alive = false;
    public float speedModifier = 1F;
    public bool poolable = true;

    private void Start()
    {
        poolable = true;
    }

    void FixedUpdate()
    {
        if (alive)
        {
            transform.position += Vector3.back * GameManager.GetPlayspeed() * (Time.deltaTime * speedModifier);
        }
    }

    public virtual void Spawn()
    {
        gameObject.SetActive(true);
        alive = true;
        enabled = true;
        poolable = false;
        GetComponent<BoxCollider>().enabled = true;
    }

    public bool IsAlive()
    {
        return alive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killzone")
        {
            alive = false;
            enabled = false;
            poolable = true;
        }
    }

}
