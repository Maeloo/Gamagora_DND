﻿using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        Character player = col.gameObject.GetComponent<Character>();
        if (player != null)
        {
            player.setUnlimiedStamina();
            GetComponentInParent<Bonus>().Release();
        }
    }
}
