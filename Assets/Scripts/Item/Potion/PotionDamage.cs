public class PotionDamage : Potion, IUse {
    public override void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanDamage(this);
        EventManager.UsePotionEventHandler(this);
    }
}