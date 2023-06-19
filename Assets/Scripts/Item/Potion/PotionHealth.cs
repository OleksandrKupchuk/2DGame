public class PotionHealth : Potion, IUse {
    public override void Use() {
        _player.AddHealth(Value);
    }
}