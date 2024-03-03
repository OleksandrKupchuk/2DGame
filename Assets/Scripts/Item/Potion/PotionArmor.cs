public class PotionArmor : Item {
    public override void Use() {
        EventManager.UseItemEventHandler(this);
    }
}
