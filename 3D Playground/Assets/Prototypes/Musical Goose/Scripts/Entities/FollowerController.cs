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
    [SerializeField] Material red;

    //privates
    private NavMeshAgent followerAgent;
    private MeshRenderer followerMesh;
    private GameObject currentTarget;
    private AudioSource playerAudioSource;
    private PlayerFollowers playerFollowers;

    private float followerSpeed;
    private int _followRange;
    private int _tetherRange;
    private bool isRunning;

    void Start()
    {
        followerAgent = this.gameObject.GetComponent<NavMeshAgent>();
        followerMesh = this.gameObject.GetComponent<MeshRenderer>();
        playerAudioSource = followTarget.GetComponent<AudioSource>();
        playerFollowers = followTarget.GetComponent<PlayerFollowers>();

        followerSpeed = followerAgent.speed;
        _followRange = followRange;
        _tetherRange = tetherRange;

        InvokeRepeating("isFollowingCheck", 0, 0.5f);
    }

    void Update()
    {
        if (!isRunning)
        {
            if (currentTarget != null) {
                followerAgent.destination = currentTarget.transform.position;
            } else {
                followerAgent.destination = this.transform.position;
            }
        }
    }

    public void isFollowingCheck()
    {
        float distanceFromTarget = Vector3.Distance(this.transform.position, followTarget.transform.position);

        if (playerAudioSource.isPlaying)
        {
            // FOLLOWER MUSIC
            if (playerAudioSource.clip == radioStationObject.findStationByName("Follower Music").radioAudio)
            {
                isRunning = false;
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
            } else if (playerAudioSource.clip == radioStationObject.findStationByName("Rock Music").radioAudio){
                // SCARY, ROCK MUSIC
                if (distanceFromTarget < (followRange)) {
                    Vector3 runTo = transform.position + ((transform.position - followTarget.transform.position + new Vector3(Random.Range(-12, 12), 0, Random.Range(-15, 12)) * 10f));
                    followerAgent.speed = Random.Range(18f, 24f);
                    followerAgent.SetDestination(runTo);
                    isRunning = true;
                    followerMesh.material = red;
                }
            }
            // SLEEPY MUSIC
            if (!isRunning && playerAudioSource.clip == radioStationObject.findStationByName("Sleepy Music").radioAudio)
            {
                followerAgent.speed = followerSpeed / 5;
            } else {
                followerAgent.speed = followerSpeed;
            }

            // FOCUS MUSIC
            if (playerAudioSource.clip == radioStationObject.findStationByName("Sleepy Music").radioAudio)
            {
                followRange = followRange * 2;
                tetherRange = tetherRange * 2;
            } else {
                followRange = _followRange;
                tetherRange = _tetherRange;
            }
        }
    }
}
