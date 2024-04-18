public interface ICondition
{
    public Condition condition  { get; }
    public System.Type state    { get; }

    public delegate bool Condition();
}
