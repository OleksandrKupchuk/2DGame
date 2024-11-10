public interface IAttribureController {
    void AddItemAttributes(Item item);
    void AddIntegerAttributes(Item item);
    void AddPercentAttributes(Item item);
    void SubstractIntegerAttributes(Item item);
    void SubstractPercentAttributes(Item item);
    void AddTemporaryAttribute(Item item);
}
