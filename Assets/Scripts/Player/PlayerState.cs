public class PlayerState
{
    public enum State
    {
        FreeMovement,
        Cutscene,
        Dialogue
    }

    private State _currentState;

    public PlayerState(State state)
    {
        _currentState = state;
    }

    public void SetState(State state)
    {
        _currentState = state;
    }
    
    public bool CanMove()
    {
        return _currentState == State.FreeMovement;
    }
    
}