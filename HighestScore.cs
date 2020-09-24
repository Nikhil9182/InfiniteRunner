using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScore : MonoBehaviour
{
    public static float highestscore = ScoreTextScript.coinAmount;
    public Text text;
    private void Start()
    {
        if (highestscore <= ScoreTextScript.coinAmount)
        {
            highestscore = ScoreTextScript.coinAmount;
        }
        text.text = "Highest Score " + highestscore.ToString();
    }
}
