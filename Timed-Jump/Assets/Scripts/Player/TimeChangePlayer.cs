using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class TimeChangePlayer : MonoBehaviour
{

    [SerializeField] private GameObject timePast;
    [SerializeField] private GameObject timePresent;
    [SerializeField] private GameObject timeFuture;
    [SerializeField] private TextMeshProUGUI currentTimeText;

    // PAST = 1 || PRESENT = 2 || FUTURE = 3
    private int currentTime = 2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CurrentTimeState();
        UpdateTimeState();
        ChangeTimeText();
    }

    private void ChangeTimeState(int newTimeState) => currentTime = newTimeState;

    private void UpdateTimeObjects(bool past, bool present, bool future)
    {
        timePast.SetActive(past);
        timePresent.SetActive(present);
        timeFuture.SetActive(future);
    }
    public void UpdateTimeState()
    {
        switch (currentTime)
        {
            case 1: UpdateTimeObjects(true, false, false); break;
            case 2: UpdateTimeObjects(false, true, false); break;
            case 3: UpdateTimeObjects(false, false, true); break;
        }
    }
    private void CurrentTimeState()
    {
        bool travelForward = Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K);
        bool travelBackward = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);

        if (travelForward) if (currentTime < 3) currentTime++; 
        if (travelBackward) if (currentTime > 1) currentTime--;

        ChangeTimeState(currentTime);

    }

    private void ChangeTimeText()
    {
        switch (currentTime)
        {
            case 1: currentTimeText.text = "Current Time: Past"; break;
            case 2: currentTimeText.text = "Current Time: Present"; break;
            case 3: currentTimeText.text = "Current Time: Future"; break;
        }
    }
}
