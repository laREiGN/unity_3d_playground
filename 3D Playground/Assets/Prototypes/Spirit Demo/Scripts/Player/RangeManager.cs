using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public SpiritManager spiritManager;

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;

        if (gameObject.activeSelf)
        {
            if (gameObject.tag.Equals("Spirit"))
            {
                SpiritController spirit = gameObject.GetComponentInParent<SpiritController>();
                spiritManager.addSpirit(spirit);
            }
        }
    }
}
