using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool enableBounds;
    [SerializeField] private Vector2 boundPosition;
    [SerializeField] private Vector2 boundSize;
    [SerializeField] private bool enableSmoothing;
    [Range(0, 0.1f)]
    [SerializeField] private float smoothSpeed = 0.05f;
    [SerializeField] private bool enableOffsets;
    [SerializeField] private Vector2 offset;

    private Camera cam;
    private float camVerticalSize;
    private float camHorizontalSize;

    private float LeftBound { get { return boundPosition.x - boundSize.x; } }
    private float RightBound { get { return boundPosition.x + boundSize.x; } }
    private float BottomBound { get { return boundPosition.y - boundSize.y; } }
    private float TopBound { get { return boundPosition.y + boundSize.y; } }

    private float LeftPosition { get { return transform.position.x - camHorizontalSize; } }
    private float RightPosition { get { return transform.position.x + camHorizontalSize; } }
    private float BottomPosition { get { return transform.position.y - camVerticalSize; } }
    private float TopPosition { get { return transform.position.y + camVerticalSize; } }

    private void Start()
    {
        cam = GetComponent<Camera>();
        camVerticalSize = cam.orthographicSize;
        camHorizontalSize = camVerticalSize * (1920f / 1080f);
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (enableOffsets) targetPos += new Vector3(offset.x, offset.y, 0);
        if (enableBounds) targetPos = StickWithinBounds(targetPos);
        if (enableSmoothing) targetPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = targetPos;
    }

    private Vector3 StickWithinBounds(Vector3 targetPos)
    {
        float x = Mathf.Clamp(targetPos.x, LeftBound + camHorizontalSize, RightBound - camHorizontalSize);
        float y = Mathf.Clamp(targetPos.y, BottomBound + camVerticalSize, TopBound - camVerticalSize);
        return new Vector3(x, y, targetPos.z);
        //if (LeftPosition < LeftBound)
        //{
        //    transform.position = new Vector3(LeftBound + camHorizontalSize, transform.position.y, -10f);
        //}
        //else if (RightPosition > RightBound)
        //{
        //    transform.position = new Vector3(RightBound - camHorizontalSize, transform.position.y, -10f);
        //}
        //else if (BottomPosition < BottomBound)
        //{
        //    transform.position = new Vector3(transform.position.x, BottomBound + camVerticalSize, -10f);
        //}
        //else if (TopPosition > TopBound)
        //{
        //    transform.position = new Vector3(transform.position.x, TopBound - camVerticalSize, -10f);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(LeftBound, TopBound, 0), new Vector3(LeftBound, BottomBound, 0));
        Gizmos.DrawLine(new Vector3(LeftBound, BottomBound, 0), new Vector3(RightBound, BottomBound, 0));
        Gizmos.DrawLine(new Vector3(RightBound, BottomBound, 0), new Vector3(RightBound, TopBound, 0));
        Gizmos.DrawLine(new Vector3(RightBound, TopBound, 0), new Vector3(LeftBound, TopBound, 0));
    }
}
