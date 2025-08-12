using UnityEngine;
using UnityEngine.UI;

public class ResultManagerS : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManagerS.TotalScore;
        Debug.Log("S:Score");
    }
}
