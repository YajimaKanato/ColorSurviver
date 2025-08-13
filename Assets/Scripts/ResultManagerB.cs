using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class ResultManagerB : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManagerB.TotalScore;
        UnityroomApiClient.Instance.SendScore(2, ScoreManagerB.TotalScore, ScoreboardWriteMode.Always);
        Debug.Log("B:Score");
    }
}
