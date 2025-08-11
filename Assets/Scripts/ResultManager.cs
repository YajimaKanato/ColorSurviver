using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManager.TotalScore;
    }
}
