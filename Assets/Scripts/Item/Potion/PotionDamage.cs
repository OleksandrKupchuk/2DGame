public class PotionDamage : Potion {
    public override void Use() {
        _player.Attributes.AddAditionanArmor(this);
    }
}