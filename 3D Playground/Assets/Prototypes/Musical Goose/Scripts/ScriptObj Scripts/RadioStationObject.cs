using System;
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

    [SerializeField]
    public RadioStationEntry[] radioStations;
    private RadioStationEntry[] _radioStationsChangeCheck;

    [SerializeField]
    [HideInInspector] 
    public bool hasChanged = false;

    public void discoverEntry(RadioStationEntry entry){
        entry.hasBeenDiscovered = true;
        hasChanged = true;
        ValueChanged();
    }

    public RadioStationObject.RadioStationEntry findStationByFrequency(float frequency)
    {
        foreach (RadioStationObject.RadioStationEntry stationEntry in radioStations)
        {
            if (stationEntry.xCoordinateAsFrequency.Equals(frequency))
            {
                return stationEntry;
            }
        }
        return null;
    }

    public RadioStationObject.RadioStationEntry findStationByName(string name)
    {
        foreach (RadioStationObject.RadioStationEntry stationEntry in radioStations)
        {
        if (stationEntry.radioStationName.Equals(name))
        {
            return stationEntry;
        }
        }
        return null;
    }

    public event Action ValueChanged = delegate { };
}
