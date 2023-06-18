public class PotionHealthRegeneration : Potion, IUse {
    public void Use() {
        _player.Attributes.AddAditionanHealthRegeneration(this);
        EventManager.UsePotionEventHandler(this);
    }
}