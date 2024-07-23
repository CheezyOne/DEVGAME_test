using UnityEngine;

public class SlowZone : Zone
{
    [SerializeField] private float _speedDecrease;
    override public void GiveEffect()
    {
        Player.GetComponent<PlayerMovement>().DecreaseSpeed(_speedDecrease);
    }
    public override void LiftEffect()
    {
        Player.GetComponent<PlayerMovement>().IncreaseSpeed(_speedDecrease);
    }
}
