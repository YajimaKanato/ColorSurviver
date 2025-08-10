using System.Collections.Generic;
using UnityEngine;
using ColorAttributes;

public class ScoreManager : MonoBehaviour
{
    static Dictionary<ColorAttribute, int> _score;
    public static Dictionary<ColorAttribute, int> Score { get { return _score; } }


    private void Start()
    {
        _score = new Dictionary<ColorAttribute, int>();
    }

    public void AddScore(ColorAttribute color, int score)
    {
        if (!_score.ContainsKey(color))
        {
            _score.Add(color, 0);
        }
        _score[color] += score;
        Debug.Log(color.ToString() + ":" + _score[color]);
    }
}
