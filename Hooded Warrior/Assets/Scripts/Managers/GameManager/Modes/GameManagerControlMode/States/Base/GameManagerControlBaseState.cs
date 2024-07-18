using System;

public abstract class GameManagerControlBaseState : State
{
    protected GameManagerControlMode    _gameManagerControlMode;
    protected Action                    _loadData;

    public GameManagerControlBaseState(GameManagerControlMode mode)
    {
        _gameManagerControlMode = mode;
    }
}
