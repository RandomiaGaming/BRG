using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject Bomb;

    public string ShootButtonName;

    private Player player;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = transform.parent.gameObject.GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetButton(ShootButtonName))
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
        if (Input.GetButtonUp(ShootButtonName))
        {
            Shoot();
        }
    }
    void Shoot()
    {

        if (player.ProjectileCount > 0)
        {
            player.ProjectileCount--;
            GameObject go = Instantiate(Bomb, transform.position, Quaternion.identity);
            go.GetComponent<BombScript>().Launcher = transform.parent.gameObject.tag;
        }
    }
}