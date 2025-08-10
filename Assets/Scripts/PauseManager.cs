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
                //Object���p������GameObject���w�肷�邱�Ƃł��ׂẴI�u�W�F�N�g���擾���邱�Ƃ��ł���
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