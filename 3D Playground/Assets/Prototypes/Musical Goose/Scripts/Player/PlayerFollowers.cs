using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowers : MonoBehaviour
{
    [SerializeField] List<GameObject> followersActive;

    public void addFollower(GameObject follower){
        if (!followersActive.Contains(follower))
        {
            followersActive.Add(follower);
        }
    }

    public void removeFollower(GameObject follower){
        if (followersActive.Contains(follower))
        {
            followersActive.Remove(follower);
        }
    }

    public int amountOfFollowers(){
        return followersActive.Count;
    }
}
