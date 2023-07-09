using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]List<Checkpoint> checkpoints = new();
    [SerializeField] PlayerScript player;
    int currentID;

    private void Awake()
    {
        player.manager = this;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].manager = this;
        }
    }

    public void ChangeCheckpoint(Checkpoint checkpoint)
    {
        int newIndex = checkpoints.IndexOf(checkpoint);
        if (newIndex > currentID) currentID = newIndex;

        if(currentID == checkpoints.Count - 1)
        {
            Debug.Log("win");
            //win
        }
    }

    public void Respawn()
    {
        player.transform.position = checkpoints[currentID].transform.position;
        player.gameObject.SetActive(true);
    }
}
