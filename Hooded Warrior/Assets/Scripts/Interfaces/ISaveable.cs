
public interface ISaveable
{
    object CaptureState();

    void RestoreState(ref object state);
}
