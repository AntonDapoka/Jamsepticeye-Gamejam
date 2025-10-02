using UnityEngine;
using System.Collections;
using System;

namespace CustomCamera
{
    [Flags]
    public enum Direction
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = 3
    }

    public class FollowCamera2D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float dampTime = 0.15f;
        [SerializeField] private Direction followType = Direction.Horizontal;
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float cameraCenterX = 0.5f;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float cameraCenterY = 0.5f;
        [SerializeField] private Direction boundType = Direction.None;
        [SerializeField] private float leftBound = 0;
        [SerializeField] private float rightBound = 0;
        [SerializeField] private float upperBound = 0;
        [SerializeField] private float lowerBound = 0;
        [SerializeField] private Direction deadZoneType = Direction.None;
        [SerializeField] private bool hardDeadZone = false;
        [SerializeField] private float leftDeadBound = 0;
        [SerializeField] private float rightDeadBound = 0;
        [SerializeField] private float upperDeadBound = 0;
        [SerializeField] private float lowerDeadBound = 0;

        private Camera camera;
        private Vector3 velocity = Vector3.zero;
        private float vertExtent;
        private float horzExtent;
        private Vector3 tempVec = Vector3.one;
        private bool isBoundHorizontal;
        private bool isBoundVertical;
        private bool isFollowHorizontal;
        private bool isFollowVertical;
        private bool isDeadZoneHorizontal;
        private bool isDeadZoneVertical;
        private Vector3 deltaCenterVec;

        private void Start()
        {
            camera = GetComponent<Camera>();
            vertExtent = camera.orthographicSize;
            horzExtent = vertExtent * Screen.width / Screen.height;
            deltaCenterVec = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0))
                - camera.ViewportToWorldPoint(new Vector3(cameraCenterX, cameraCenterY, 0));


            isFollowHorizontal = (followType & Direction.Horizontal) == Direction.Horizontal;
            isFollowVertical = (followType & Direction.Vertical) == Direction.Vertical;
            isBoundHorizontal = (boundType & Direction.Horizontal) == Direction.Horizontal;
            isBoundVertical = (boundType & Direction.Vertical) == Direction.Vertical;

            isDeadZoneHorizontal = ((deadZoneType & Direction.Horizontal) == Direction.Horizontal) && isFollowHorizontal;
            isDeadZoneVertical = ((deadZoneType & Direction.Vertical) == Direction.Vertical) && isFollowVertical;
            tempVec = Vector3.one;
        }

        private void LateUpdate()
        {
            if (target)
            {
                Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(cameraCenterX, cameraCenterY, 0));

                if (!isFollowHorizontal)
                {
                    delta.x = 0;
                }
                if (!isFollowVertical)
                {
                    delta.y = 0;
                }
                Vector3 destination = transform.position + delta;

                if (!hardDeadZone)
                {
                    tempVec = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
                }
                else
                {
                    tempVec.Set(transform.position.x, transform.position.y, transform.position.z);
                }

                if (isDeadZoneHorizontal)
                {
                    if (delta.x > rightDeadBound)
                    {
                        tempVec.x = target.position.x - rightDeadBound + deltaCenterVec.x;
                    }
                    if (delta.x < -leftDeadBound)
                    {
                        tempVec.x = target.position.x + leftDeadBound + deltaCenterVec.x;
                    }
                }
                if (isDeadZoneVertical)
                {
                    if (delta.y > upperDeadBound)
                    {
                        tempVec.y = target.position.y - upperDeadBound + deltaCenterVec.y;
                    }
                    if (delta.y < -lowerDeadBound)
                    {
                        tempVec.y = target.position.y + lowerDeadBound + deltaCenterVec.y;
                    }
                }

                if (isBoundHorizontal)
                {
                    tempVec.x = Mathf.Clamp(tempVec.x, leftBound + horzExtent, rightBound - horzExtent);
                }

                if (isBoundVertical)
                {
                    tempVec.y = Mathf.Clamp(tempVec.y, lowerBound + vertExtent, upperBound - vertExtent);
                }

                tempVec.z = transform.position.z;
                transform.position = tempVec;
            }
        }
    }

}