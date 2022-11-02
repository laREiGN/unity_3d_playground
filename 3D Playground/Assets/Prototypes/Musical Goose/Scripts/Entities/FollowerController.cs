using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{

    [Header ("Required on screen objects")]
    [SerializeField] GameObject followTarget;
    [SerializeField] RadioStationObject radioStationObject;

    [Header ("Follow settings")]
    [SerializeField] int followRange;
    [SerializeField] int tetherRange;

    [Header ("Required mats")]
    [SerializeField] Material green;
    [SerializeField] Material yellow;

    //privates
    private NavMeshAgent followerAgent;
    private MeshRenderer followerMesh;
    private GameObject currentTarget;
    private AudioSource playerAudioSource;
    private PlayerFollowers playerFollowers;

    void Start()
    {
        followerAgent = this.gameObject.GetComponent<NavMeshAgent>();
        followerMesh = this.gameObject.GetComponent<MeshRenderer>();
        playerAudioSource = followTarget.GetComponent<AudioSource>();
        playerFollowers = followTarget.GetComponent<PlayerFollowers>();
        InvokeRepeating("isFollowingCheck", 0, 0.5f);
    }

    void Update()
    {
        if (currentTarget != null) {
            followerAgent.destination = currentTarget.transform.position;
        }
    }

    public void isFollowingCheck()
    {
        float distanceFromTarget = Vector3.Distance(this.transform.position, followTarget.transform.position);

        if (playerAudioSource.isPlaying && playerAudioSource.clip == radioStationObject.findStationByName("Follower Music").radioAudio)
        {
            if (distanceFromTarget < followRange)
            {
                currentTarget = followTarget;
                playerFollowers.addFollower(this.gameObject);
                followerMesh.material = green;
            } else if(distanceFromTarget > tetherRange)
            {
                currentTarget = null;
                playerFollowers.removeFollower(this.gameObject);
                followerMesh.material = yellow;
            }
        }
    }
}
