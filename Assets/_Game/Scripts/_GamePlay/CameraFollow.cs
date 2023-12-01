using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;


    private Vector3 offset;
    private void OnInit()
    {
        offset = new Vector3(0, 11, -10);
        transform.rotation = Quaternion.Euler(45,0,0);
    }
    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, 1);
    }
}
