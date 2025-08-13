using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class ResultManagerS : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManagerS.TotalScore;
        UnityroomApiClient.Instance.SendScore(1, ScoreManagerS.TotalScore, ScoreboardWriteMode.Always);
        Debug.Log("S:Score");
    }
}
