using ColorAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerB : MonoBehaviour
{
    static int _totalScore = 0;
    public static int TotalScore { get { return _totalScore; } }
    [SerializeField] Text _scoreText;

    static Dictionary<ColorAttribute, int> _score;
    public static Dictionary<ColorAttribute, int> Score { get { return _score; } }

    private void Start()
    {
        _score = new Dictionary<ColorAttribute, int>();
        _totalScore = 0;
        _scoreText.text = "Score : " + _totalScore;
    }

    private void Update()
    {
        _scoreText.text = "Score : " + _totalScore;
    }

    public void AddScore(ColorAttribute color, int score)
    {
        if (!_score.ContainsKey(color))
        {
            _score.Add(color, 0);
        }
        _score[color] += score;
        Debug.Log(color.ToString() + ":" + _score[color]);
        _totalScore += score;
    }
}
