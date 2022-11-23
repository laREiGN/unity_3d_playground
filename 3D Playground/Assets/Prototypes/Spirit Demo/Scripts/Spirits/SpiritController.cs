using UnityEngine;
using UnityEngine.AI;

public class SpiritController : MonoBehaviour
{
    [Header ("Spirit")]
    public GameObject spirit;
    public NavMeshAgent spiritAgent;
    [HideInInspector] public bool isFollowing;
    private SpiritManager isFollowingWho;

    [Header ("Spirit Body")]
    public GameObject spiritBody;


    void Start()
    {
        spirit.SetActive(true);
        spiritBody.SetActive(false);
    }

    private void Update()
    {
        if (isFollowing && isFollowingWho != null)
        {
            spiritAgent.SetDestination(isFollowingWho.transform.position);
        }
    }

    public void spiritCollected(SpiritManager collectedBy)
    {
        spiritBody.SetActive(true);
        isFollowing = true;
        isFollowingWho = collectedBy;

    }

    public void bodyCollected()
    {
        isFollowingWho.removeSpirit(this);

        spirit.SetActive(false);
        isFollowing = false;
        isFollowingWho = null;
    }

}
