using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour,IGameEnd
{
    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;
    

    // Update is called once per frame
    void Update()
    {
        if (!_isGameClear&&!_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Objectを継承したGameObjectを指定することですべてのオブジェクトを取得することができる
                var pause = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
                foreach (var obj in pause)
                {
                    if (!_isPause)
                    {
                        obj.GetComponent<IPause>()?.Pause();
                    }
                    else
                    {
                        obj.GetComponent<IPause>()?.Resume();
                    }
                }
                _isPause = !_isPause;
                if (_isPause)
                {
                    Debug.Log("Pause");
                }
                else
                {
                    Debug.Log("Resume");
                }
            }
        }
    }

    public void GameClear()
    {
        _isGameClear = true;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}