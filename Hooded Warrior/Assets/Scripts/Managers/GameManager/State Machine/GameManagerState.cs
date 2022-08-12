
public abstract class GameManagerState
{
    protected GameManager _gameManager;
    protected GameManagerFiniteStateMachine _stateMachine;

    public GameManagerState(GameManager gameManager, GameManagerFiniteStateMachine stateMachine)
    {
        _gameManager = gameManager;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void LogicUpdate() { }

    public virtual void Exit() { }

}
