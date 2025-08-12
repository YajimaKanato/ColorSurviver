using UnityEngine;
using UnityEngine.UI;

public class ResultManagerB : MonoBehaviour
{
    Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "Score : " + ScoreManagerB.TotalScore;
        Debug.Log("B:Score");
    }
}
