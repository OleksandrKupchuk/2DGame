public class PotionSpeed : Potion, IUse {
    public override void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanSpeed(this);
        EventManager.UsePotionEventHandler(this);
    }
}