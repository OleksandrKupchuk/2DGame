public class PotionHealth : Item {
    private Player _player;

    private void Start() {
        _player = FindAnyObjectByType<Player>();
    }

    public override void Use() {
        base.Use();
        _player.AddHealth(Attributes[0].value);
    }
}
