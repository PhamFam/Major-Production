﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;


public class ItemInteractionBehaviour : MonoBehaviour
{
    public float SpoilerTime;
    public Item Item;
    public GameEventArgs ItemPickedUp, ItemPickedUp1, ItemPickedUp2, ItemPickedUp3;
    private void Update()
    {
        var timer = 0f;
        timer += Time.time;
        if (SpoilerTime <= 0)
            return;
        if (timer > SpoilerTime)
            ItemObjectPooler.s_instance.Destroy(gameObject);
    }

    void SwitchCheck(InventoryText player)
    {
        switch (Item.ItemType)
        {
            case ItemType.None:
                break;
            case ItemType.Wood:
                player.woodAmount++;
                player.WoodAmounttext.text = player.woodAmount.ToString();
                var tweenItWood = player.WoodAmounttext.GetComponent<TweenScaleBehaviour>();
                tweenItWood.TweenScale();
                break;
            case ItemType.Stone:
                player.stoneAmount++;
                player.StoneAmounttext.text = player.stoneAmount.ToString();
                var tweenItStone = player.StoneAmounttext.GetComponent<TweenScaleBehaviour>();
                tweenItStone.TweenScale();
                break;
            case ItemType.Metal:
                player.metalAmount++;
                player.MetalAmounttext.text = player.metalAmount.ToString();
                var tweenItMetal = player.MetalAmounttext.GetComponent<TweenScaleBehaviour>();
                tweenItMetal.TweenScale();
                break;
            case ItemType.Goop:
                player.goopAmount++;
                player.GoopAmounttext.text = player.goopAmount.ToString();
                var tweenItGoop = player.GoopAmounttext.GetComponent<TweenScaleBehaviour>();
                tweenItGoop.TweenScale();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "P1":
                {
                    ItemPickedUp.Raise(Item, this.gameObject);
                    var ib = other.gameObject.GetComponent<IStorageable>();
                    var player = other.gameObject.transform.parent.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<InventoryText>();
                    SwitchCheck(player);
                    ib.AddToInventory(Item, gameObject);
                    ItemObjectPooler.s_instance.Destroy(gameObject);
                    break;
                }
            case "P2":
                {
                    ItemPickedUp1.Raise(Item, this.gameObject);
                    var ib1 = other.gameObject.GetComponent<IStorageable>();
                    var player = other.gameObject.transform.parent.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<InventoryText>();
                    SwitchCheck(player);
                    ib1.AddToInventory(Item, gameObject);
                    ItemObjectPooler.s_instance.Destroy(gameObject);
                    break;
                }
            case "P3":
                {
                    ItemPickedUp2.Raise(Item, this.gameObject);
                    var ib2 = other.gameObject.GetComponent<IStorageable>();
                    var player = other.gameObject.transform.parent.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<InventoryText>();
                    SwitchCheck(player);
                    ib2.AddToInventory(Item, gameObject);
                    ItemObjectPooler.s_instance.Destroy(gameObject);
                    break;
                }
            case "P4":
                {
                    ItemPickedUp3.Raise(Item, this.gameObject);
                    var ib3 = other.gameObject.GetComponent<IStorageable>();
                    var player = other.gameObject.transform.parent.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<InventoryText>();
                    SwitchCheck(player);
                    ib3.AddToInventory(Item, gameObject);
                    ItemObjectPooler.s_instance.Destroy(gameObject);
                    break;
                }
        }

        if (!other.gameObject.GetComponent<MeshCollider>())
            return;
        this.GetComponent<Rigidbody>().drag = 50f;
    }
}

