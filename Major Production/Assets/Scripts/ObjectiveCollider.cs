﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollider : MonoBehaviour {
    public Objective Obj;
    private void OnEnable()
    {
        this.tag = "Objective";
    }
    private void Start()
    {
        this.name = Obj.Title;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Obj.ParentScript.CurrentObjective.name == Obj.name)
            Obj.OnReach();
    }
}
