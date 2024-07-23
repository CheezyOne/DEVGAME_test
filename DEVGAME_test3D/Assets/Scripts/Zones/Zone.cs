using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject Player;
    private protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            GiveEffect();
        }
    }
    private protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            LiftEffect();
        }
    }
    public virtual void LiftEffect()
    {

    }
    public virtual void GiveEffect()
    {

    }
}
