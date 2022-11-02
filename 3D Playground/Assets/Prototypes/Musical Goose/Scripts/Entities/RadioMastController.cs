using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMastController : MonoBehaviour
{

    [Header ("Mast Settings")]
    [SerializeField] RadioStationObject radioStationObject;
    [SerializeField] float radioStationFrequency;

    void OnTriggerEnter(Collider collidedObject){
        if (collidedObject.tag == "Player")
        {
            activateFrequency();
        }
    }

    void activateFrequency(){
        foreach (RadioStationObject.RadioStationEntry stationEntry in radioStationObject.radioStations)
        {
            if (stationEntry.xCoordinateAsFrequency.Equals(radioStationFrequency))
            {
                radioStationObject.discoverEntry(stationEntry);
            }
        }
    }
}
