using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_Camera : MonoBehaviour
{
    private List<Transform> PlayerTransforms;
    private Vector3 PanVelecity;
    private Camera cam;
    public float ZoomSpeed;
    public float ZoomBuffer = 2;
    public float MinZoom;
    private void Start()
    {
        cam = GetComponent<Camera>();
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        PlayerTransforms = new List<Transform>();
        foreach (GameObject Player in Players)
        {
            PlayerTransforms.Add(Player.transform);
        }
    }
    void LateUpdate()
    {
        Move();
        Zoom();
    }
    void Zoom()
    {
        List<float> PossibleZooms = new List<float>();
        foreach (Transform PlayerTransform in PlayerTransforms)
        {
            PossibleZooms.Add(Mathf.Abs(transform.position.y - PlayerTransform.position.y) + ZoomBuffer);
            PossibleZooms.Add((Mathf.Abs(transform.position.x - PlayerTransform.position.x) + ZoomBuffer) / cam.aspect);
        }
        float TargetZoom = PossibleZooms[0];
        foreach (float PossibleZoom in PossibleZooms)
        {
            if (PossibleZoom > TargetZoom)
            {
                TargetZoom = PossibleZoom;
            }
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, TargetZoom, Time.deltaTime * ZoomSpeed);
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinZoom, float.MaxValue);
    }
    void Move()
    {
        Bounds bounds = new Bounds(PlayerTransforms[0].transform.position, Vector3.zero);
        foreach (Transform Player in PlayerTransforms)
        {
            bounds.Encapsulate(Player.position);
        }
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(bounds.center.x, bounds.center.y, -10), ref PanVelecity, Vector3.Distance(transform.position, new Vector3(bounds.center.x, bounds.center.y, -10)) * 0.1f);
    }
}