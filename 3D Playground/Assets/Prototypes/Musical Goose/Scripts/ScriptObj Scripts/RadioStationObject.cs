using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "radioStationList.asset", menuName = "Scriptable Objects/Radio Station List")]
public class RadioStationObject : ScriptableObject
{
    [System.Serializable]
    public class RadioStationEntry
    {
        public string radioStationName;
        [Range (-230, 230)]
        public float xCoordinateAsFrequency;
        public AudioClip radioAudio;
        public bool hasBeenDiscovered; 
    }

    public RadioStationEntry[] radioStations;  
}
