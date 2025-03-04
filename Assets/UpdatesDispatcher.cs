using System.Collections.Generic;
using VContainer.Unity;

public interface IUpdate
{
    public void CustomUpdate();
}

public interface IFixedUpdate
{
    public void CustomFixedUpdate();
}

public class UpdatesDispatcher : ITickable,IFixedTickable, IPauseGameListener, IResumeGameListener, IStartGameListener, IFinishGameListener, IMainMenuListener
{
    private List<IUpdate> _updates = new List<IUpdate>();
    private List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();

    private bool _canTick;

    public void StartDispatch(IEnumerable<IUpdate> updates, IEnumerable<IFixedUpdate> fixedUpdates)
    {
        _updates.AddRange(updates);
        _fixedUpdates.AddRange(fixedUpdates);
    }
    
    public void StopDispatch(IEnumerable<IUpdate> updates, IEnumerable<IFixedUpdate> fixedUpdates)
    {
        foreach (var update in updates)
        {
            _updates.Remove(update);
        }
        
        foreach (var fixedUpdate in fixedUpdates)
        {
            _fixedUpdates.Remove(fixedUpdate);
        }
    }
    
    public void Tick()
    {
        if (!_canTick)
        {
            return;
        }
        foreach (var iUpdate in _updates)
        {
            iUpdate.CustomUpdate();
        }
    }

    public void FixedTick()
    {
        if (!_canTick)
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
        _canTick = false;
    }

    public void OnResumeGame()
    {
        _canTick = true;
    }

    public void OnStartGame()
    {
        _canTick = true;
    }

    public void OnFinishGame()
    {
        _canTick = false;
    }
    
    public void OnMainMenu()
    {
        _canTick = false;
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
