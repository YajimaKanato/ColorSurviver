using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour,IGameControl
{
    [SerializeField] GameObject _pause;

    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;


    private void Start()
    {
        _pause.SetActive(false);
    }

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
                        _pause.SetActive(true);
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
                        _pause.SetActive(false);
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