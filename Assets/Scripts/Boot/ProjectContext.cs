public class ProjectContext : Singleton<ProjectContext> {
    public Player Player { get; private set; }

    public void Init() {
        PlayerSpawner.OnPlayerSpawned += SetPlayer;
    }

    private void SetPlayer(Player player) {
        Player = player;
    }

    private void OnDestroy() {
        PlayerSpawner.OnPlayerSpawned -= SetPlayer;
    }
}
