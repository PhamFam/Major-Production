﻿using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;

    private void Start()
    {
        //null should be used to check validity of the token
        if (PlayerObjectives == null) return;
        PlayerObjectives.ForEach(objective => objective.ProgressQuest("initialize"));
        //set the current objective
        CurrentObjective = PlayerObjectives[0];
        //activate the currentobjective
        CurrentObjective.ProgressQuest("start");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public void ProgressChain(Object[] args)
    {
        if (args[0] != CurrentObjective)
            return;
        ProgressChain();
    }
    /// <summary>
    /// 
    /// </summary>
    public virtual void ProgressChain()
    {
        Debug.Log("chain progress");
        if (CurrentObjective == null)
            return;
        if (PlayerObjectives.Count <= 0)
            Destroy(gameObject);

        //Set Next Objective
        PlayerObjectives.Remove(CurrentObjective);
        if (PlayerObjectives.Count != 0)
        {
            CurrentObjective = PlayerObjectives[0];
            CurrentObjective.Status = ObjectiveStatus.Active;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public void ProgressObjective(Object[] args)
    {
        var sender = args[0];
        CurrentObjective.ProgressQuest(sender as Item);
    }
}