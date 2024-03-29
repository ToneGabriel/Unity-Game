using System;

public abstract class GameManagerState : State
{
    protected GameManager   _gameManager;
    protected Action        _loadData;

    public GameManagerState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}
