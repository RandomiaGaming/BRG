using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    public Player Stats_Target;
    [Space]
    public List<Image> Bomb_Images;
    public Text Place_Text;
    public Text Lap_Text;

    void Update()
    {
        for (int i = 0; i < Bomb_Images.Count; i++)
        {
            if (Stats_Target.Bomb_Count > i)
            {
                Bomb_Images[i].color = Color.red;
            }
            else
            {
                Bomb_Images[i].color = Color.white;
            }
        }
        Lap_Text.text = Stats_Target.Lap.ToString() + " of 3 laps!";
        if (Stats_Target.Place == 1)
        {
            Place_Text.text = "1st!";
        }else if (Stats_Target.Place == 2)
        {
            Place_Text.text = "2nd!";
        }
        else if (Stats_Target.Place == 3)
        {
            Place_Text.text = "3rd!";
        }
        else
        {
            Place_Text.text = Stats_Target.Place.ToString() + "th!";
        }
    }
}
