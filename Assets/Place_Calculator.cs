﻿using System.Collections.Generic;
using UnityEngine;
public class Place_Calculator : MonoBehaviour
{
    private List<Player> All_Players = new List<Player>();
    private List<Checkpoint> All_Checkpoins = new List<Checkpoint>();
    private void Start()
    {
        All_Players = new List<Player>();
        foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player"))
        {
            All_Players.Add(Player.GetComponent<Player>());
        }
        All_Checkpoins = new List<Checkpoint>();
        foreach (GameObject Checkpoint in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            All_Checkpoins.Add(Checkpoint.GetComponent<Checkpoint>());
        }
        int Current_ID = 0;
        List<Checkpoint> Sorted_Checkpoints = new List<Checkpoint>();
        while (All_Checkpoins.Count > 0)
        {
            for (int i = 0; i < All_Checkpoins.Count; i++)
            {
                if (All_Checkpoins[i].Checkpoint_ID == Current_ID)
                {
                    Sorted_Checkpoints.Add(All_Checkpoins[i]);
                    All_Checkpoins.RemoveAt(i);
                    Current_ID++;
                    break;
                }
            }
        }
        All_Checkpoins = new List<Checkpoint>(Sorted_Checkpoints);
    }
    private void Update()
    {
        foreach (Player Current_Player in All_Players)
        {
            if (Current_Player.Checkpoint < All_Checkpoins.Count - 1)
            {
                Current_Player.Next_Checkpoint_Distance = Vector3.Distance(Current_Player.transform.position, All_Checkpoins[Current_Player.Checkpoint + 1].transform.position);
            }
            else
            {
                Current_Player.Next_Checkpoint_Distance = Vector3.Distance(Current_Player.transform.position, All_Checkpoins[0].transform.position);
            }
        }
        foreach (Player Current_Player in All_Players)
        {
            Current_Player.Place = 1;
            foreach (Player Comparing_Player in All_Players)
            {
                if (Current_Player.Lap < Comparing_Player.Lap)
                {
                    Current_Player.Place++;
                }
                else if (Current_Player.Lap == Comparing_Player.Lap)
                {
                    if (Current_Player.Checkpoint < Comparing_Player.Checkpoint)
                    {
                        Current_Player.Place++;
                    }
                    else if (Current_Player.Checkpoint == Comparing_Player.Checkpoint)
                    {
                        if (Current_Player.Next_Checkpoint_Distance > Comparing_Player.Next_Checkpoint_Distance)
                        {
                            Current_Player.Place++;
                        }
                        else if (Current_Player.Next_Checkpoint_Distance == Comparing_Player.Next_Checkpoint_Distance)
                        {
                            if (Current_Player.Player_ID < Comparing_Player.Player_ID)
                            {
                                Current_Player.Place++;
                            }
                        }
                    }
                }
            }
        }
    }
}