using UnityEngine;

public class UIElementController : MonoBehaviour
{

    [Header ("UI Elements")]
    [SerializeField] GameObject expandContainer;
    [SerializeField] GameObject collapseableContainer;


    public void expandControls()
    {
        if (!collapseableContainer.activeSelf)
        {
            collapseableContainer.SetActive(!collapseableContainer.activeSelf);
            expandContainer.SetActive(!expandContainer.activeSelf);
        }
    }
    public void collapseControls()
    {
        if (collapseableContainer.activeSelf)
        {
            collapseableContainer.SetActive(!collapseableContainer.activeSelf);
            expandContainer.SetActive(!expandContainer.activeSelf);   
        }
    }
}
