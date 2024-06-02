using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform followTarget;

    private Vector2 _startingPosition;
    private float _startingZ;
    private Vector2 _camMoveSinceStart => (Vector2)cam.transform.position - _startingPosition;
    private float _zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    private float _clippingPlane =>
        (cam.transform.position.z + (_zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float _parallaxFactor => Mathf.Abs(_zDistanceFromTarget) / _clippingPlane;

   
    private void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.position.z;
        StartCoroutine(CamMoveRoutine());
    }

    IEnumerator CamMoveRoutine()
    {
        Vector2 newPosition = _startingPosition + _camMoveSinceStart * _parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
        yield return new WaitForSeconds(1f);
    }
}
