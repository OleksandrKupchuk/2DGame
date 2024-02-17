public class PotionHealthRegeneration : Potion, IUse {
    public void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanHealthRegeneration(this);
        EventManager.UsePotionEventHandler(this);
    }
}
