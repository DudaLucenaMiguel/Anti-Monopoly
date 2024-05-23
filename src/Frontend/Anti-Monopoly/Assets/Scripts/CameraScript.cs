using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    private Vector3 _offset;
    public float smoothTime;
    private Vector3 _currentvelocity = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        var targetPosition = player.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentvelocity, smoothTime);
    }
}
