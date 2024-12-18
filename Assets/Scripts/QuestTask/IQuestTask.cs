public interface IQuestTask {
    bool isDone();
    bool IsRewardTaken { get; }
    void GiveReward();
}
