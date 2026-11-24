public interface IEvent
{
    bool IsConditionMet();
    void Execute();
}
