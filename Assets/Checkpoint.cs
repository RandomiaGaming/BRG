using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Checkpoint_ID = 0;
    private static int Max_ID = 0;
    private void Start()
    {
        if (Checkpoint_ID > Max_ID)
        {
            Max_ID = Checkpoint_ID;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player Collided_Player = collision.GetComponent<Player>();
            if (Checkpoint_ID == 0 && Collided_Player.Last_Touched_Checkpoint == Max_ID)
            {
                Collided_Player.Last_Touched_Checkpoint = Checkpoint_ID;
                Collided_Player.Lap++;
            }
            else if (Collided_Player.Last_Touched_Checkpoint + 1 == Checkpoint_ID)
            {
                Collided_Player.Last_Touched_Checkpoint = Checkpoint_ID;
            }
            else if (Checkpoint_ID == Max_ID && Collided_Player.Last_Touched_Checkpoint == 0)
            {
                Collided_Player.Last_Touched_Checkpoint = Checkpoint_ID;
                Collided_Player.Lap--;
            }
            else if (Collided_Player.Last_Touched_Checkpoint - 1 == Checkpoint_ID)
            {
                Collided_Player.Last_Touched_Checkpoint = Checkpoint_ID;
            }
        }
    }
}
