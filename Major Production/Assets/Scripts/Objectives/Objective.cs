﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
public enum ObjectiveType
{
    None = 0,
    Gather = 1,
    Reach = 2
}

public enum ObjectiveStatus
{
    None = 0,
    Inactive = 1,
    Active = 2,
    Complete = 3
}

public enum ActionOnReach
{
    MarkComplete = 0,
    AddModififer = 1,
    PlayAudio = 2,
    PlayAnimation = 3,
}
[CreateAssetMenu]
public class Objective : ScriptableObject
{
    public string Title;
    [Multiline]
    public string Description;
    public ObjectiveType MissionType;
    public Item RequiredItem;
    public int CurrentAmount = 0;
    public int RequiredAmount = 5;
    public ObjectiveStatus Status;
    public GameObject Target;
    public List<ActionOnReach> ActionsOnReach;
    public GameObject PlayerForStat;
    //public AudioSource testAudio;
    //public AudioClip TestAudioClip;
    //public GatherObjectiveBehaviour GatherParentScript { get; set; }
    //public ReachObjectiveBehaviour ReachParentScript { get; set; }
    public GameEventArgs QuestStarted;
    public GameEventArgs QuestEnded;
    public GameEventArgs QuestChange;


    public void OnReach(Objective CurrentObj)
    {
        if (CurrentObj.ActionsOnReach.Contains(ActionOnReach.MarkComplete))
        {
            //Set To Complete
            CurrentObj.Status = ObjectiveStatus.Complete;
        }

        if (CurrentObj.ActionsOnReach.Contains(ActionOnReach.PlayAudio))
        {
            //Play Audio
        }

        if (CurrentObj.ActionsOnReach.Contains(ActionOnReach.AddModififer))
        {
            //Add Modifier
        }

        if (CurrentObj.ActionsOnReach.Contains(ActionOnReach.PlayAnimation))
        {
            //Play Animation
        }
    }
}
