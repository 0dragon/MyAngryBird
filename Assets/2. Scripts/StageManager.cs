using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int currentStage;
    public int maxBirds;
    private int currentBirds;
    public Text birdCountText;

    void Start()
    {
        SetStage(currentStage);
    }

    public void SetStage(int stage)
    {
        currentStage = stage;
        switch (currentStage)
        {
            case 1:
                maxBirds = 5;
                break;
            case 2:
                maxBirds = 4;
                break;
            case 3:
                maxBirds = 3;
                break;
            default:
                maxBirds = 5;
                break;
        }
        currentBirds = maxBirds;
        UpdateBirdCountText();
        Debug.Log("현재 스테이지: " + currentStage + "발사 가능한 새의 수: " + maxBirds);
    }

    public void DecreaseBirdCount()
    {
        if (currentBirds > 0)
        {
            currentBirds--;
            UpdateBirdCountText();
        }
    }

    void UpdateBirdCountText()
    {
        birdCountText.text = "birdCount: " + currentBirds;
    }
}
