using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjectionAnimator : MonoBehaviour
{
    public Matrix4x4 _originalProjection;
    Camera _cam;

    [SerializeField] private MirroredDisplayDebuff _mirroredDisplayDebuff;
    private bool _isAnimating;
    private Coroutine _animationCoroutine;

    void Start()
    {
        _cam = GetComponent<Camera>();
        _originalProjection = _cam.projectionMatrix;
    }

    private void OnEnable()
    {
        _mirroredDisplayDebuff.OnApply.AddListener(StartAnimation);
        _mirroredDisplayDebuff.OnRestore.AddListener(StopAnimation);
    }
    
    private void OnDisable()
    {
        _mirroredDisplayDebuff.OnApply.RemoveListener(StartAnimation);
        _mirroredDisplayDebuff.OnRestore.RemoveListener(StopAnimation);
    }
    
    private void StartAnimation()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        _animationCoroutine = StartCoroutine(AnimateDistortion());
    }
    
    private void StopAnimation()
    {
        _isAnimating = false;
    }

    IEnumerator AnimateDistortion()
    {
        _isAnimating = true;
        while (_isAnimating)
        {
            Matrix4x4 p = _originalProjection;
            p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.01F;
            p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.01F;
            _cam.projectionMatrix = p;
            yield return null;
        }
    }
}
