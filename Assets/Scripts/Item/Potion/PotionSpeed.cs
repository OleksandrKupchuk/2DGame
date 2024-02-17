public class PotionSpeed : Potion, IUse {
    public void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanSpeed(this);
        EventManager.UsePotionEventHandler(this);
    }
}