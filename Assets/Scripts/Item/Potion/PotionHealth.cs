public class PotionHealth : ItemUsable {
    private Player _player;

    private void Start() {
        _player = FindAnyObjectByType<Player>();
    }

    public override void Use() {
        _player.AddHealth(Attributes[0].value);
    }
}
