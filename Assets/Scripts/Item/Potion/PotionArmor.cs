public class PotionArmor : Potion, IUse {
    public void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanArmor(this);
        EventManager.UsePotionEventHandler(this);
    }
}
