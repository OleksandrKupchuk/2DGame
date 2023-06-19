public class PotionHealthRegeneration : Potion, IUse {
    public override void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanHealthRegeneration(this);
        EventManager.UsePotionEventHandler(this);
    }
}