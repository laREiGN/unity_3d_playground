using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioController : MonoBehaviour
{

    [Header ("Required onscreen elements")]
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] GameObject bottomScreen;
    [SerializeField] GameObject onScreenPointer;

    [Header ("Required seperate elements")]
    [SerializeField] RadioStationObject radioStationList;
    [SerializeField] RectTransform radioStationLocation;
    [SerializeField] AudioSource playerAudioSource;
    private RadioStationObject.RadioStationEntry[] radioStations;
    [HideInInspector] public List<GameObject> radioStationsCreated;

    void Awake()
    {
        statusText.SetText("Not Playing...");
        radioStations = radioStationList.radioStations;
        prepareRadioStations();
    }

    void Update(){
        if (radioStationList.hasChanged){
            prepareRadioStations();
            radioStationList.hasChanged = false;
        }
    }

    public void playOrPauseAudio(){
        if (playerAudioSource.clip != null) {
            if (playerAudioSource.isPlaying) {
                playerAudioSource.Pause();
            } else {
                playerAudioSource.Play();
            }
        }
    }

    public void spoolForward()
    {
        float currentLocation = onScreenPointer.transform.localPosition.x;
        float closestValue = getClosestRadioStation(true, currentLocation);

        spoolRadio(onScreenPointer, closestValue);
    }

    public void spoolBackward()
    {
        float currentLocation = onScreenPointer.transform.localPosition.x;
        float closestValue = getClosestRadioStation(false, currentLocation);

        spoolRadio(onScreenPointer, closestValue);
    }

    public void spoolRadio(GameObject gameObject, float xTarget){
        gameObject.transform.localPosition = new Vector3(xTarget, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        RadioStationObject.RadioStationEntry radioStation = radioStationList.findStationByFrequency(xTarget);
        if (radioStation != null){
            statusText.SetText(radioStation.radioStationName);
            playerAudioSource.Stop();
            playerAudioSource.clip = radioStation.radioAudio;
            playerAudioSource.Play();
        } else {
            statusText.SetText("Not Playing...");
        }
    }

    private void prepareRadioStations()
    {
        foreach (Transform child in bottomScreen.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var thisStation in radioStations)
        {
            GameObject thisStationLocation = Instantiate(
                radioStationLocation.gameObject,
                bottomScreen.transform.position - new Vector3(0, 0, 0),
                Quaternion.identity);

            RectTransform pointerRect = thisStationLocation.GetComponent<RectTransform>();
            pointerRect.SetParent(bottomScreen.transform);
            pointerRect.localPosition = new Vector3(thisStation.xCoordinateAsFrequency, 0, 0);
            pointerRect.offsetMin = new Vector2(pointerRect.offsetMin.x, radioStationLocation.offsetMin.y);
            pointerRect.offsetMax = new Vector2(pointerRect.offsetMax.x, radioStationLocation.offsetMax.y);
            pointerRect.localScale = new Vector3(1, 1, 1);

            if (thisStation.hasBeenDiscovered){
                thisStationLocation.SetActive(true);
            } else {
                thisStationLocation.SetActive(false);
            }
            radioStationsCreated.Add(thisStationLocation);
        }
    }

    private float getClosestRadioStation(bool greaterThan, float searchValue){
        float minDistance = float.MaxValue;
        float closestValue = searchValue;

        for (int i = 0; i < radioStations.Length; i++){
            if (radioStations[i].hasBeenDiscovered){
                if (greaterThan){
                    if (radioStations[i].xCoordinateAsFrequency > searchValue){
                        var currentDistance = Mathf.Abs(radioStations[i].xCoordinateAsFrequency - searchValue);
                        if (currentDistance < minDistance){
                            minDistance = currentDistance;
                            closestValue = radioStations[i].xCoordinateAsFrequency;
                        }
                    }
                } else {
                    if (radioStations[i].xCoordinateAsFrequency < searchValue) {
                        var currentDistance = Mathf.Abs(radioStations[i].xCoordinateAsFrequency - searchValue);
                        if (currentDistance < minDistance){
                            minDistance = currentDistance;
                            closestValue = radioStations[i].xCoordinateAsFrequency;
                        }
                    }
                }
            }
        }

        return closestValue;
    }
}
