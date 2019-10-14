using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Place_Calculator : MonoBehaviour
{
    public Tilemap Ground_TM;
    private List<Vector3Int> Unsafe_Positions = new List<Vector3Int>();
    private List<Vector3> Path_Positions = new List<Vector3>();
    private List<Player> All_Players = new List<Player>();
    private void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        All_Players = new List<Player>();
        foreach (GameObject Player in Players)
        {
            All_Players.Add(Player.GetComponent<Player>());
        }
        Unsafe_Positions = new List<Vector3Int>();
        for (int x = Ground_TM.cellBounds.xMin; x < Ground_TM.cellBounds.xMax; x++)
        {
            for (int y = Ground_TM.cellBounds.yMin; y < Ground_TM.cellBounds.yMax; y++)
            {
                if (Ground_TM.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    Unsafe_Positions.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        Unsafe_Positions.Add(Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0)));
        Unsafe_Positions.Add(Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0)) + new Vector3Int(1, 0, 0));
        Unsafe_Positions.Add(Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0)) + new Vector3Int(2, 0, 0));
        Unsafe_Positions.Add(Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0)) + new Vector3Int(-1, 0, 0));
        Path_Positions = new List<Vector3>(PathFind());
    }
    private void Update()
    {
        foreach (Player Current_Player in All_Players)
        {
            int CP_Best_Match = 0;
            float CP_Best_Distance = Vector3.Distance(Current_Player.transform.position, Path_Positions[0]);
            for (int i = 1; i < Path_Positions.Count; i++)
            {
                if (Vector3.Distance(Current_Player.transform.position, Path_Positions[i]) < CP_Best_Distance)
                {
                    CP_Best_Match = i;
                }
            }
            Current_Player.Place = 1;
            foreach (Player Target_Player in All_Players)
            {
                if (Target_Player.Lap > Current_Player.Lap)
                {
                    Current_Player.Place++;
                }
                else if (Target_Player.Lap == Current_Player.Lap)
                {
                    int TP_Best_Match = 0;
                    float TP_Best_Distance = Vector3.Distance(Target_Player.transform.position, Path_Positions[0]);
                    for (int i = 1; i < Path_Positions.Count; i++)
                    {
                        if (Vector3.Distance(Target_Player.transform.position, Path_Positions[i]) < TP_Best_Distance)
                        {
                            TP_Best_Match = i;
                        }
                    }
                    if (TP_Best_Match > CP_Best_Match)
                    {
                        Current_Player.Place++;
                    }
                }
            }
        }
    }
    private List<Vector3> PathFind()
    {
        Vector3Int Start = Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0) + new Vector3Int(0, 1, 0));
        Vector3Int End = Ground_TM.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0) + new Vector3Int(0, -1, 0));

        List<Node> Visited = new List<Node>();
        List<Node> Check_This_Sycle = new List<Node>() { new Node(Start, null) };
        while (true)
        {
            foreach (Node Check_Node in Check_This_Sycle)
            {
                foreach (Node Neighbor in GetNeighbors(Visited, Check_Node))
                {
                    Visited.Add(Neighbor);
                    Check_This_Sycle.Add(Neighbor);
                }
            }
            foreach (Node node in Visited)
            {
                if (node.Position == End)
                {
                    List<Node> Node_Output = new List<Node>();
                    Node current = node;
                    while (current.Parent != null)
                    {
                        Node_Output.Add(current);
                        current = current.Parent;
                    }
                    Node_Output.Add(new Node(Start, null));
                    Node_Output.Reverse();
                    List<Vector3> Vector_Output = new List<Vector3>();
                    foreach (Node Output_Node in Node_Output)
                    {
                        Vector_Output.Add(Output_Node.Position);
                    }
                    return Vector_Output;
                }
            }
        }
    }
    private List<Node> GetNeighbors(List<Node> Visited, Node Check_Node)
    {
        List<Node> Neighbors = new List<Node>();
        Vector3Int Offset = new Vector3Int(0, 1, 0);
        if (Valid(Check_Node.Position + Offset, Visited))
        {
            Neighbors.Add(new Node(Check_Node.Position + Offset, Check_Node));
        }
        Offset = new Vector3Int(0, -1, 0);
        if (Valid(Check_Node.Position + Offset, Visited))
        {
            Neighbors.Add(new Node(Check_Node.Position + Offset, Check_Node));
        }
        Offset = new Vector3Int(1, 0, 0);
        if (Valid(Check_Node.Position + Offset, Visited))
        {
            Neighbors.Add(new Node(Check_Node.Position + Offset, Check_Node));
        }
        Offset = new Vector3Int(-1, 0, 0);
        if (Valid(Check_Node.Position + Offset, Visited))
        {
            Neighbors.Add(new Node(Check_Node.Position + Offset, Check_Node));
        }
        return Neighbors;
    }
    private bool Valid(Vector3Int Position, List<Node> Visited_Nodes)
    {
        foreach (Vector3Int Unsafe_Position in Unsafe_Positions)
        {
            if (Position == Unsafe_Position)
            {
                return false;
            }
        }
        foreach (Node Visited_Node in Visited_Nodes)
        {
            if (Position == Visited_Node.Position)
            {
                return false;
            }
        }
        return true;
    }
    private class Node
    {
        public Vector3Int Position;
        public Node Parent;
        public Node(Vector3Int _pos, Node _Parent)
        {
            Position = _pos;
            Parent = _Parent;
        }
    }
}
