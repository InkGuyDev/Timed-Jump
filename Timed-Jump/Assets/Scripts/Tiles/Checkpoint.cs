using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private int id;
    private bool gotCheckpoint = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!gotCheckpoint)
            {
                gotCheckpoint = true;
                GameManager.instance.ChangeCheckpoint(transform);
            }
        }
    }

    public int GetId() => id;
}
