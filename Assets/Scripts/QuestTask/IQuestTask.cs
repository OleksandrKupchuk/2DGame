public interface IQuestTask {
    void Init(Player player);
    bool IsDone();
    bool IsRewardTaken { get; }
    void GiveReward();
}
