public class PotionArmor : Potion, IUse {
    public override void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanArmor(this);
        EventManager.UsePotionEventHandler(this);
    }
}