using UnityEngine;
using unityroom.Api;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SlimeorBat.Slime)
        {
            UnityroomApiClient.Instance.SendScore(1, ScoreManager.TotalScore, ScoreboardWriteMode.Always);
        }
        else if (SlimeorBat.Bat)
        {
            UnityroomApiClient.Instance.SendScore(2, ScoreManager.TotalScore, ScoreboardWriteMode.Always);
        }
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManager.TotalScore;
    }
}
