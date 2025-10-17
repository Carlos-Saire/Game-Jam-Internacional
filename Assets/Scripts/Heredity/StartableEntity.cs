using UnityEngine;

public abstract class StartableEntity : MonoBehaviour
{
    protected bool isStartGame = false;

    protected virtual void OnEnable()
    {
        GameManager.OnGameStarted += StartGame;
    }
    protected virtual void OnDisable()
    {
        GameManager.OnGameStarted -= StartGame;
    }
    protected virtual void StartGame()
    {
        isStartGame = true;
    }
}
