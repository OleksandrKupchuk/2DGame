public class PotionHealth : ItemView {
    private Player _player;

    private void Start() {
        _player = FindAnyObjectByType<Player>();
    }

    //public override void Use() {
    //    _player._healthController.AddHealth(Attributes[0].value);
    //}
}
