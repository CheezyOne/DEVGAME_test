using System;

public class PlayerHealth : Health
{
    public static Action onPlayerDeath;
    public bool IsInvincible = false;
    public override void Die()
    {
        if (IsInvincible)
        {
            return;
        }
        onPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
    public void DieWithInvicibility()
    {
        onPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
