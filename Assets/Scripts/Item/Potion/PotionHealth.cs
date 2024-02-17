public class PotionHealth : Potion, IUse {
    public void Use() {
        _player.AddHealth(Value);
    }
}