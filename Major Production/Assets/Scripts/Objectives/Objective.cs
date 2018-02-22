﻿using System;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     Controls the Status of the Current Objective
/// </summary>
public enum ObjectiveStatus
{
    None = 0,
    Inactive = 1,
    Active = 2,
    Complete = 3
}

[CreateAssetMenu]
public class Objective : ScriptableObject
{
    [SerializeField] private string _title;

    [Multiline] [SerializeField] private  string _description;

    [SerializeField] private int _currentAmount;

    [SerializeField] private int _requiredAmount;

    [SerializeField] private Item _requiredItem;

    [SerializeField] private GameEventArgs QuestStart;

    [SerializeField] private GameEventArgs QuestChange;

    [SerializeField] private GameEventArgs QuestComplete;

    [SerializeField] private UnityEvent actionsOnComplete;

    public string Description
    {
        get { return _description; }
    }

    public int CurrentAmount
    {
        get { return _currentAmount; }
    }

    public int RequiredAmount
    {
        get { return _requiredAmount; }
    }

    public ObjectiveStatus Status { get; set; }

    /// <summary>
    ///     move this objective forward in its current state
    ///     None->Inactive, Inactive-> Active, Active-> Active, Active -> Complete
    ///     Invoke the questchange event everytime we changestate
    ///     invoke the questend when going from active -> complete    ///
    /// </summary>
    /// <param>
    ///     the item we are using to progress this quest
    ///     <name>item</name>
    /// </param>
    public void ProgressQuest(params object[] args)
    {
        if (args[0] == null)
            return;

        Debug.Log("Quest Progress: " + _title + " " + args[0]);

        var valids = new object[] {_requiredItem, "initialize", "start"};

        if (!valids.Contains(args[0]))
            return;

        switch (Status)
        {
            case ObjectiveStatus.None:
                ChangeState(ObjectiveStatus.Inactive);
                break;
            case ObjectiveStatus.Inactive:
                ChangeState(ObjectiveStatus.Active);
                break;
            case ObjectiveStatus.Active:
                _currentAmount++;
                ChangeState(AmountCheck() ? ObjectiveStatus.Complete : ObjectiveStatus.Active);
                break;
            case ObjectiveStatus.Complete:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    ///     Successful valid state change raise events and invoke unity events
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(ObjectiveStatus state)
    {
        Status = state;
        QuestChange.Raise(this);
        switch (Status)
        {
            case ObjectiveStatus.None:
                break;
            case ObjectiveStatus.Inactive:
                QuestStart.Raise(this);
                break;
            case ObjectiveStatus.Active:
                break;
            case ObjectiveStatus.Complete:
                QuestComplete.Raise(this);
                actionsOnComplete.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    ///     Check if Goal is reached
    /// </summary>
    /// <returns></returns>
    private bool AmountCheck()
    {
        return CurrentAmount >= RequiredAmount;
    }
}