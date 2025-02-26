using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameListener{}

public interface IStartGameListener : IGameListener
{
    public void OnStartGame();
}

public interface IPauseGameListener : IGameListener
{
    public void OnPauseGame();
}

public interface IResumeGameListener : IGameListener
{
    public void OnResumeGame();
}

public interface IFinishGameListener : IGameListener
{
    public void OnFinishGame();
}


public enum GameState
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
    Finished = 3
}
public class GameStateController : MonoBehaviour
{
    private GameState _currentState;

    public static GameStateController Instance;
    private List<IGameListener> gameListeners = new List<IGameListener>();

    private void Awake()
    {
        Instance = this;
        FindAllGameListeners();
    }
    
    public void StartGame()
    {
        if (_currentState != GameState.MainMenu && _currentState != GameState.Finished)
        {
            return;
        }
        _currentState = GameState.Playing;
        foreach (var gameListener in gameListeners)
        {
            if (gameListener is IStartGameListener startGameListener)
            {
                startGameListener.OnStartGame();
            }
        }
    }

    public void ResumeGame()
    {
        if (_currentState != GameState.Paused)
        {
            return;
        }
        _currentState = GameState.Playing;
        foreach (var gameListener in gameListeners)
        {
            if (gameListener is IResumeGameListener resumeGameListener)
            {
                resumeGameListener.OnResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        if (_currentState != GameState.Playing)
        {
            return;
        }
        _currentState = GameState.Paused;
        foreach (var gameListener in gameListeners)
        {
            if (gameListener is IPauseGameListener pauseGameListener)
            {
                pauseGameListener.OnPauseGame();
            }
        }
    }
    
    public void FinishGame()
    {
        if (_currentState != GameState.Playing)
        {
            return;
        }
        _currentState = GameState.Finished;
        
        foreach (var gameListener in gameListeners)
        {
            if (gameListener is IFinishGameListener finishGameListener)
            {
                finishGameListener.OnFinishGame();
            }
        }
    }
    
    private void FindAllGameListeners()
    {
        List<IGameListener> gameListeners = FindObjectsOfType<MonoBehaviour>().OfType<IGameListener>().ToList();

        this.gameListeners = gameListeners;
    }
    
}
