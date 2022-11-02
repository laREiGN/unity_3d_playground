using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    int layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Ground");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, 100f, layerMask);
        if (hasHit && !mouseOverUI())
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    public bool mouseOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
