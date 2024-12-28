public class ProjectContext : Singleton<ProjectContext> {
    public QuestSystem QuestSystem { get; private set; }
    public Player Player { get; private set; }

    public void Init() {
        QuestSystem = new QuestSystem();
        PlayerSpawner.OnPlyerSpawned += SetPlayer;
    }

    private void SetPlayer(Player player) {
        Player = player;
    }

    private void OnDestroy() {
        PlayerSpawner.OnPlyerSpawned -= SetPlayer;
    }
}
