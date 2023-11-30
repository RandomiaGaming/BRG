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
        if (collision.gameObject.tag == "PlayerController")
        {
            PlayerController Collided_PlayerController = collision.GetComponent<PlayerController>();
            if (Checkpoint_ID == 0 && Collided_PlayerController.Last_Touched_Checkpoint == Max_ID)
            {
                Collided_PlayerController.Last_Touched_Checkpoint = Checkpoint_ID;
                Collided_PlayerController.Lap++;
            }
            else if (Collided_PlayerController.Last_Touched_Checkpoint + 1 == Checkpoint_ID)
            {
                Collided_PlayerController.Last_Touched_Checkpoint = Checkpoint_ID;
            }
            else if (Checkpoint_ID == Max_ID && Collided_PlayerController.Last_Touched_Checkpoint == 0)
            {
                Collided_PlayerController.Last_Touched_Checkpoint = Checkpoint_ID;
                Collided_PlayerController.Lap--;
            }
            else if (Collided_PlayerController.Last_Touched_Checkpoint - 1 == Checkpoint_ID)
            {
                Collided_PlayerController.Last_Touched_Checkpoint = Checkpoint_ID;
            }
        }
    }
}
