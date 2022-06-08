using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public int laneCount = 3;
    public int difficulty = 2;
    private float laneSize;
    public GameObject SpawnLeft;
    public GameObject SpawnRight;

    public float interval = 3;
    private float nextTime = 0;

    void Start()
    {
        laneSize = Vector3.Distance(SpawnLeft.transform.position, SpawnRight.transform.position);
    }

    int bonusCounter = 0;
    int obstacleCounter = 0;

    public int bonusThreshold = 5;
    public int obstacleThreshold = 3;

    public int bonusVariant = 5;
    public int obstacleVariant = 3;

    void FixedUpdate()
    {
        Debug.Log("Generating");
        if (Time.time >= nextTime)
        {
            if (bonusCounter > bonusThreshold)
            {
                nextTime += Random.Range(1F, 2F);
                int coin = Random.Range(1, bonusVariant);
                bonusCounter = 0;

                if (coin == 1)
                {
                    SpawnRandomGate();
                }
                else if (coin == 2)
                {
                    SpawnOrderGate();
                }
                else if (coin == 3)
                {
                    SpawnBoostPad();
                }
            }
            else if (obstacleCounter > obstacleThreshold)
            {
                nextTime += Random.Range(1F, 2F);
                int coin = Random.Range(0, obstacleVariant);
                obstacleCounter = 0;

                if (coin == 1)
                {
                    SpawnObstacleCube();
                }
                else if (coin == 2)
                {
                    SpawnSpinningTrap();
                }
            }
            else
            {
                nextTime += Random.Range(1F, 2F);
                SpawnCube();
                bonusCounter++;
                obstacleCounter++;
            }
        }
    }

    private void SpawnRandomGate()
    {
        SimplePool.Instance.SpawnFromPool("RandomGate", SpawnLeft.transform.position + Vector3.right * laneSize / 2);
    }

    private void SpawnOrderGate()
    {
        SimplePool.Instance.SpawnFromPool("OrderGate", SpawnLeft.transform.position + Vector3.right * laneSize / 2);
    }

    private void SpawnObstacleCube()
    {
        int lane = Random.Range(0, laneCount);
        float spawnOffset = laneSize / laneCount * lane;
        SimplePool.Instance.SpawnFromPool("Obstacle", SpawnLeft.transform.position + Vector3.right * spawnOffset);
    }

    private void SpawnBoostPad()
    {
        int lane = Random.Range(0, laneCount);
        float spawnOffset = laneSize / laneCount * lane;
        SimplePool.Instance.SpawnFromPool("BoostPad", SpawnLeft.transform.position + Vector3.right * spawnOffset);
    }

    private void SpawnSpinningTrap()
    {

    }

    private void SpawnCube()
    {
        int lane = Random.Range(0, laneCount);
        float spawnOffset = laneSize / laneCount * lane;
        SimplePool.Instance.SpawnFromPool("Cube", SpawnLeft.transform.position + Vector3.right * spawnOffset);
    }
}
