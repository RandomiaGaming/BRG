using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnishLine : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCrossed(collision);
        }
    }

    void PlayerCrossed(Collider2D player)
    {
        player.GetComponent<Player>().Lap++;
    }
}