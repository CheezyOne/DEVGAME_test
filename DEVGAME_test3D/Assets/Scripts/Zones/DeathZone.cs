
public class DeathZone : Zone
{
    override public void GiveEffect()
    {
        Player.GetComponent<PlayerHealth>().DieWithInvicibility();
    }
}
