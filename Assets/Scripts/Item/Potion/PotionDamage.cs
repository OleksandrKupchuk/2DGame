public class PotionDamage : Potion, IUse {
    public void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanDamage(this);
        EventManager.UsePotionEventHandler(this);
    }
}