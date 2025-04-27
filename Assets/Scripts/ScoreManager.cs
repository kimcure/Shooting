using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager: MonoBehaviour
{
    public int score { get; private set; }
    public UIManager uiManager;

    public void AddScore(int value)
    {
        score += value;
        uiManager.UpdateScore(score);
    }
}
