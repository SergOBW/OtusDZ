using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

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

public interface IMainMenuListener : IGameListener
{
    public void OnMainMenu();
}


public enum GameState
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
    Finished = 3
}
public class GameStateController : IStartable
{
    private GameState _currentState;
    
    private List<IGameListener> _gameListeners = new List<IGameListener>();

    public void StartDispatch(IEnumerable<IGameListener> gameListeners)
    {
        _gameListeners.AddRange(gameListeners);
    }
    
    public void StopDispatch(IEnumerable<IGameListener> gameListeners)
    {
        foreach (var gameListener in gameListeners)
        {
            _gameListeners.Remove(gameListener);
        }
    }
    
    public void Start()
    {
        MainMenu();
    }
    
    public void StartGame()
    {
        if (_currentState != GameState.MainMenu && _currentState != GameState.Finished)
        {
            return;
        }
        _currentState = GameState.Playing;
        foreach (var gameListener in _gameListeners)
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
        foreach (var gameListener in _gameListeners)
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
        foreach (var gameListener in _gameListeners)
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
        
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IFinishGameListener finishGameListener)
            {
                finishGameListener.OnFinishGame();
            }
        }
    }
    
    public void MainMenu()
    {
        if (_currentState != GameState.Finished && _currentState != GameState.MainMenu)
        {
            return;
        }
        _currentState = GameState.MainMenu;
        
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IMainMenuListener mainMenuListener)
            {
                mainMenuListener.OnMainMenu();
            }
        }
    }
}
