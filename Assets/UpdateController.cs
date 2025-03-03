using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUpdate
{
    public void CustomUpdate();
}

public interface IFixedUpdate
{
    public void CustomFixedUpdate();
}

public class UpdateController : MonoBehaviour, IPauseGameListener, IResumeGameListener, IStartGameListener, IFinishGameListener, IMainMenuListener
{
    public static UpdateController Instance;
    
    private List<IUpdate> _updates;
    private List<IFixedUpdate> _fixedUpdates;

    private bool _isUpdateRunning;
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
        if (!_isUpdateRunning)
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
        if (!_isUpdateRunning)
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
        _isUpdateRunning = false;
    }

    public void OnResumeGame()
    {
        _isUpdateRunning = true;
    }

    public void OnStartGame()
    {
        _isUpdateRunning = true;
    }

    public void OnFinishGame()
    {
        _isUpdateRunning = false;
    }
    
    public void OnMainMenu()
    {
        _isUpdateRunning = false;
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
