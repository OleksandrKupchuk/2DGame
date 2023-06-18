public class PotionArmor : Potion, IUse {
    public void Use() {
        _player.Attributes.AddAditionanArmor(this);
        EventManager.UsePotionEventHandler(this);
    }
}