using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    [HideInInspector] public List<SpiritController> spiritList = new List<SpiritController>();

    public void addSpirit(SpiritController spirit)
    {
        spiritList.Add(spirit);
        spirit.spiritCollected(this);
    }

    public void removeSpirit(SpiritController spirit)
    {
        spiritList.Remove(spirit);
    }
}
