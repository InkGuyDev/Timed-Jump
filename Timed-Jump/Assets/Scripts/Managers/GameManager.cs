using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI checkPointText;
    [SerializeField] private GameObject player;
    public Transform currentCheckpoint;
    private float timer;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        player.GetComponent<TimeChangePlayer>().UpdateTimeState();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateCheckpointText();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2") + " s";
    }

    private void UpdateCheckpointText() => checkPointText.text = "Checkpoint: " + currentCheckpoint.transform.gameObject.GetComponent<Checkpoint>().GetId();

    public void ChangeCheckpoint(Transform newCheckpoint) => currentCheckpoint = newCheckpoint;

    public Transform GetCurretnCheckpoint() => currentCheckpoint;

}
