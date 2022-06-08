using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : BasicLaneObject
{
    IEnumerator BoostRoutine()
    {
        GameManager.Instance.BoostIconEnable(); 
        //sine ease
        for (float speed = GameManager.Playspeed; speed < GameManager.MaxBoostSpeed; speed += 0.1f)
        {
            GameManager.Playspeed = speed;
            yield return new WaitForSeconds(.1f);
        }

        yield return new WaitForSeconds(3f);

        for (float speed = GameManager.Playspeed; speed > GameManager.NormalSpeed; speed -= 0.1f)
        {
            GameManager.Playspeed = speed;
            yield return new WaitForSeconds(.1f);
        }
        GameManager.Instance.BoostIconDisable();
    }

    public void ActivateBoost()
    {
        //GameManager.Playspeed = 10F;
        //guard??
        StartCoroutine(BoostRoutine());
    }

    public override void Spawn()
    {
        base.Spawn();
        gameObject.tag = "Boost";
    }
}
