using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    INGAME,
    MENU,
}


public class GameManager : MonoBehaviour
{
    //[SerializeField] private static float Playspeed = 1F;
    public static float Playspeed = 4F;
    public static float MaxBoostSpeed = 10F;
    public static float NormalSpeed = 4F;

    public TextMeshProUGUI scoreText;
    public Image boostImage;

    public static GameManager Instance;

    private int score = 0;
    public void Awake()
    {
        Instance = this;
    }

    public static void SetPlayspeed(float speed)
    {
        Playspeed = speed;
    }

    public static float GetPlayspeed()
    {
        return Playspeed;
    }

    public void IncreaseScore()
    {
        score += 100;
        scoreText.text = "SCORE: " + score;
    }

    public void GameOver()
    {
        scoreText.text += " GAME OVER";
    }

    public void BoostIconEnable()
    {
        boostImage.enabled = true;
    }

    public void BoostIconDisable()
    {
        boostImage.enabled = false;
    }


#if UNITY_EDITOR

#endif
    
}
