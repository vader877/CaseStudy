using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BasicCube;

public class GameLogic : MonoBehaviour
{
    //mastercube
    private List<GameObject> PlayerStack = new List<GameObject>();
    public GameObject character;
    public static Color CurrentColour = Color.cyan;
    //quick hack for adjusting height on pickup
    private float riseFactor = 0.6F;

    //max limit for cubes
    public int CubeLimit = 5;

    public int CubeMatchGoal = 3;
    public float Score = 0F;

    public int FrenzyGoal = 3;
    public int FrenzyCombo = 0;


    //hacky functions that I am not very happy with ¯\_(?)_/¯
    //private void RiseOneCube()
    //{
    //    Vector3 tempo = character.transform.position;
    //    tempo.y += riseFactor;
    //    character.transform.position = tempo;
    //    CameraManager.IncreaseFOV(1);
    //}

    //private void FallCubes(int count)
    //{
    //    Vector3 tempo = character.transform.position;
    //    tempo.y -= riseFactor * count;
    //    character.transform.position = tempo;
    //    CameraManager.DecreaseFOV(1);
    //}

    private void RecalculateHeight()
    {
        character.transform.position = new Vector3(character.transform.position.x, riseFactor * (PlayerStack.Count), 0);

        for (int x = 0; x < PlayerStack.Count; x++)
        {
            PlayerStack[x].transform.position = character.transform.position + (Vector3.down * riseFactor * (x + 1));
        }
    }

    private void LoseOneCube()
    {
        GameObject temp = PlayerStack[PlayerStack.Count - 1];
        temp.GetComponent<BasicCube>().UnFreeze();
        RemoveFromStack(temp);
        CameraManager.DecreaseFOV(1);
    }

    private void StackCubes(GameObject NewGuy)
    {
        PlayerStack.Add(NewGuy);
        //RecalculateHeights(0);

        NewGuy.transform.position = character.transform.position + (Vector3.down * riseFactor * (PlayerStack.Count));
        NewGuy.GetComponent<BasicCube>().Freeze(character);
        CurrentColour = NewGuy.GetComponent<BasicCube>().getCubeColour();

        CameraManager.IncreaseFOV(1);

    }

    private void RemoveFromStack(GameObject temp)
    {
        PlayerStack.Remove(temp);
        RecalculateHeight();
    }


    //frenzy mode
    private void CalculatePlayerStack()
    {
        CubeColours previousColour = CubeColours.DEFAULT;
        int combo = 0;
        int cursor = 0;
        foreach (GameObject temp in PlayerStack.ToArray())
        {
            CubeColours currentcolour = temp.GetComponent<BasicCube>().getCubeColourEnum();
            //Debug.Log(currentcolour + " = " + previousColour);

            if (currentcolour != previousColour)
            {
                previousColour = currentcolour;
                combo = 1;
                FrenzyCombo = 0;
            }
            else
            {
                combo++;
            }

            // in PlayerStack.GetRange(cursor - combo, combo)

            if (combo == CubeMatchGoal + 1)
            {
                Debug.Log(cursor + "  x " + (cursor - combo + 1));

                for (int x = cursor; x >= cursor - combo + 1; x--)
                {
                    PlayerStack[x].GetComponent<BasicCube>().MarkForScore();
                }
                //PlayerStack.RemoveRange(cursor - combo, combo);
                FrenzyCombo++;
                combo = 0;
            }
            cursor++;
        }
    }

    private void CleanPlayerStack()
    {
        foreach (GameObject temp in PlayerStack.ToArray())
        {
            if (temp.GetComponent<BasicCube>().IsMarked())
            {
                GameManager.Instance.IncreaseScore();
                //Destroy(temp);
                RemoveFromStack(temp);
                CameraManager.DecreaseFOV(1);
                temp.transform.parent = null;
                temp.GetComponent<BasicCube>().poolable = true;
                temp.transform.position = new Vector3(0, 0, -7);
                temp.SetActive(false);
            }
        }
    }

    private void RandomizePlayerStack()
    {
        PlayerStack.Sort((a, b) => Random.Range(-1, 1));
    }

    private void OrderPlayerStack()
    {
        PlayerStack.Sort((a, b) => {
            int c1 = (int) a.GetComponent<BasicCube>().getCubeColourEnum();
            int c2 = (int) b.GetComponent<BasicCube>().getCubeColourEnum();

            if (c1 == c2)
            {
                return 0;
            }
            return c1<c2 ? -1 : 1;
            });
    }

    private void Debuger()
    {
        PlayerStack.ForEach(a=> Debug.Log(a.GetComponent<BasicCube>().getCubeColourEnum()));
    }

    private bool CanScore(CubeColours colour)
    {
        for(int x= PlayerStack.Count - 1 ; x > PlayerStack.Count - 1 - CubeMatchGoal; x--)
        {
            if (PlayerStack[x].GetComponent<BasicCube>().getCubeColourEnum() != colour)
                return false;
        }
        return true;
    }

    //mark for delete

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {/*
            if (PlayerStack.Count >= CubeLimit && !CanScore(other.GetComponent<BasicCube>().getCubeColourEnum()))
            {
                other.GetComponent<BasicCube>().UnFreeze();
                LoseOneCube();
                RecalculateHeight();
            }
            else
            {*/
                StackCubes(other.gameObject);
                CalculatePlayerStack();
                CleanPlayerStack();
                RecalculateHeight();
            //}
        }

        if (other.gameObject.tag == "Boost")
        {
            other.gameObject.GetComponent<Boost>().ActivateBoost();
        }

        if (other.gameObject.tag == "Random")
        {
            //Debug.Log("avadakedavra");
            RandomizePlayerStack();
            CalculatePlayerStack();
            CleanPlayerStack();
            RecalculateHeight();
            //play effect
        }

        if (other.gameObject.tag == "Order")
        {
            OrderPlayerStack();
            CalculatePlayerStack();
            CleanPlayerStack();
            //RecalculateHeight();
            //play effect
        }

        if (other.gameObject.tag == "Obstacle")
        {
            if (PlayerStack.Count == 0)
            {
                //gameover animation
                GameManager.Instance.GameOver();
                PlayerController.Instance.StopPlay();
                GameManager.SetPlayspeed(0);

                return;
            }
            LoseOneCube();
            RecalculateHeight();
        }

        if (other.gameObject.tag == "Finish")
        {

        }

        if (other.gameObject.tag == "Ramp")
        {

        }

        if (other.gameObject.tag == "RandomCube")
        {

        }
    }






    //IEnumerator FrenzyCoroutine()
    //{

    //    //sine ease
    //    for (float speed = GameManager.Playspeed; speed < GameManager.MaxBoostSpeed; speed += 0.1f)
    //    {
    //        GameManager.Playspeed = speed;
    //        yield return new WaitForSeconds(.1f);
    //    }

    //}


}
