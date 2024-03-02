public class PotionArmor : Item {
    Player _player;

    private void Start() {
        _player = FindObjectOfType<Player>();
    }

    public override void Use() {
        _player.Inventory.PlayerAttributes.AddAditionanArmor(this);
        EventManager.UseItemEventHandler(this);
    }
}
