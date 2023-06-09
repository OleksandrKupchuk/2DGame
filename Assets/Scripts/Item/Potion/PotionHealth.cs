public class PotionHealth : Potion {
    public override void Use() {
        _player.AddHealth(Value);
    }
}