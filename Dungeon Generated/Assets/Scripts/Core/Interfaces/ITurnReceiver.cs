using Joeri.Tools.Utilities;

public interface ITurnReceiver
{
    public EventWrapper endTurnCallback { get; }

    public void OnTurnStart();

    public void DuringTurn();
}
