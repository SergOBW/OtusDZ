using System.Collections.Generic;
using System.Linq;
using ShootEmUp;
using UnityEngine;

public interface IUpdate
{
    public void CustomUpdate();
}

public interface IFixedUpdate
{
    public void CustomFixedUpdate();
}

public class UpdateController : MonoBehaviour, IPauseGameListener, IResumeGameListener, IStartGameListener, IFinishGameListener
{
    public static UpdateController Instance;
    
    private List<IUpdate> _updates;
    private List<IFixedUpdate> _fixedUpdates;

    private bool _isGameplayRunning;
    private void Awake()
    {
        Instance = this;
        
        List<IUpdate> updates = FindObjectsOfType<MonoBehaviour>().OfType<IUpdate>().ToList();;
        _updates = updates;
        
        List<IFixedUpdate> fixedUpdates = FindObjectsOfType<MonoBehaviour>().OfType<IFixedUpdate>().ToList();
        _fixedUpdates = fixedUpdates;
    }

    private void Update()
    {
        if (!_isGameplayRunning)
        {
            return;
        }
        foreach (var iUpdate in _updates)
        {
            iUpdate.CustomUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (!_isGameplayRunning)
        {
            return;
        }
        foreach (var iFixedUpdate in _fixedUpdates)
        {
            iFixedUpdate.CustomFixedUpdate();
        }
    }

    public void OnPauseGame()
    {
        _isGameplayRunning = false;
    }

    public void OnResumeGame()
    {
        _isGameplayRunning = true;
    }

    public void OnStartGame()
    {
        _isGameplayRunning = true;
    }

    public void OnFinishGame()
    {
        _isGameplayRunning = false;
    }

    public void AddNewListener<T>(T newListener)
    {
        if (newListener is IUpdate newUpdateListener)
        {
            _updates.Add(newUpdateListener);
        }
        
        if (newListener is IFixedUpdate newFixedUpdate)
        {
            _fixedUpdates.Add(newFixedUpdate);
        }
    }

    public void RemoveListener<T>(T newListener)
    {
        if (newListener is IUpdate newUpdateListener)
        {
            _updates.Remove(newUpdateListener);
        }
        
        if (newListener is IFixedUpdate newFixedUpdate)
        {
            _fixedUpdates.Remove(newFixedUpdate);
        }
    }
}
