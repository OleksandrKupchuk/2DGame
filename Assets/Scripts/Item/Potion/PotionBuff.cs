public class PotionBuff : Item {
    private Player _player;
    private IndicatorPanel _indicatorPanel;

    private void Start() {
        _player = FindObjectOfType<Player>();
        _indicatorPanel = FindObjectOfType<IndicatorPanel>();
    }

    public override void Use() {
        base.Use();
        _indicatorPanel.ShowBuffIndacator(this);
    }
}
