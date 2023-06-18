public class PotionSpeed : Potion, IUse {
    public void Use() {
        _player.Attributes.AddAditionanSpeed(this);
        EventManager.UsePotionEventHandler(this);
    }
}