public class Potion : Item {
    public override void Use() {
        EventManager.UseItemEventHandler(this);
    }
}
