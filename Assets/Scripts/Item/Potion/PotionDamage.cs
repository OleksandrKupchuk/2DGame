public class PotionDamage : Potion, IUse {
    public void Use() {
        _player.Attributes.AddAditionanDamage(this);
        EventManager.UsePotionEventHandler(this);
    }
}