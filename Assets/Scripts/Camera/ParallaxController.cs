using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [Tooltip("Камера, относительно которой считается параллакс. Оставьте пустым для Camera.main.")]
    [SerializeField] private Transform cameraTransform;


    [Header("Параметры параллакса")]
    [Tooltip("Коэффициент параллакса по горизонтали (0..1). Чем меньше — тем медленнее фон движется относительно камеры.")]
    [SerializeField, Range(0f, 1f)] private float parallaxFactorX = 0.3f;


    [Tooltip("Коэффициент параллакса по вертикали (0..1). Чем меньше — тем медленнее фон движется относительно камеры.")]
    [SerializeField, Range(0f, 1f)] private float parallaxFactorY = 0.3f;


    private Vector3 startPosition;
    private Vector3 startCameraPosition;


    private void Start()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
            else
                Debug.LogWarning("ParallaxController: Camera.main не найдена, нужно вручную присвоить камеру.");
        }


        ResetPosition();
    }

    public void ResetPosition()
    {
        startPosition = transform.position;
        startCameraPosition = cameraTransform != null ? cameraTransform.position : Vector3.zero;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;


        Vector3 delta = cameraTransform.position - startCameraPosition;
        Vector3 newPos = startPosition + new Vector3(delta.x * parallaxFactorX, delta.y * parallaxFactorY, 0f);
        transform.position = newPos;
    }
}