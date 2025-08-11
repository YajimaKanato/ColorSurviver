using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour,IGameControl
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
                        var script = obj.GetComponents<IPause>();
                        if(script != null)
                        {
                            foreach (var obj2 in script)
                            {
                                obj2.Pause();
                            }
                        }
                    }
                    else
                    {
                        var script = obj.GetComponents<IPause>();
                        if(script != null)
                        {
                            foreach (var obj2 in script)
                            {
                                obj2.Resume();
                            }
                        }
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

    public void GameStart()
    {

    }
}