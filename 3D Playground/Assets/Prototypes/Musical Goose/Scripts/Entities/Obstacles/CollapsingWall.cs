using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingWall : MonoBehaviour
{

    [SerializeField] string radioStationName;
    [SerializeField] int minimumFollowersNeeded;
    [SerializeField] RadioStationObject radioStationObject;
    private bool canCollapse;
    void Update()
    {
        if (canCollapse){
            collapseWall();
        }
    }

    void OnTriggerStay(Collider collidedObject)
    {
        if (collidedObject.tag == "Player" && this.gameObject.activeSelf)
        {
            collapseCheck(collidedObject.gameObject);
        }
    }

    public void collapseCheck(GameObject player){
        AudioSource playerAudio = player.GetComponent<AudioSource>();
        PlayerFollowers playerFollowers = player.GetComponent<PlayerFollowers>();
        if (playerFollowers.amountOfFollowers() >= minimumFollowersNeeded){
            if (playerAudio.isPlaying && playerAudio.clip == radioStationObject.findStationByName(radioStationName).radioAudio){
                canCollapse = true;
            }
        }
    }

    public void collapseWall(){
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, -10f, this.transform.position.z), 0.5f * Time.deltaTime);
        if (this.transform.position.y < -6.1) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
