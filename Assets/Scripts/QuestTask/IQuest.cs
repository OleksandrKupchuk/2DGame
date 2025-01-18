public interface IQuest {
    bool IsDone();
    bool IsRewardTaken { get; }
    void GiveReward();
}
