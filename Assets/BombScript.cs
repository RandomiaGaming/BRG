using UnityEngine;
public class BombScript : MonoBehaviour
{
    [HideInInspector]
    public string Launcher;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.name != Launcher)
        {
            collision.GetComponent<Player>().StunnedTime = 5;
        }
    }
}