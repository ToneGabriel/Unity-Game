using System;

public abstract class GameManagerControlBaseState : State
{
    protected GameManagerControlMode    _controlMode;
    protected Action                    _loadData;

    public GameManagerControlBaseState(GameManagerControlMode mode)
    {
        _controlMode = mode;
    }
}
