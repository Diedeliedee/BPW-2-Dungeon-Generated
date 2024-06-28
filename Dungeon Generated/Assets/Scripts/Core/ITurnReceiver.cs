using Joeri.Tools.Utilities;

public interface ITurnReceiver
{
    public EventWrapper onTurnEnd { get; }

    public void OnTurnStart();

    public void DuringTurn();
}
