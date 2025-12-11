using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Camera mainCamera;
    public float defaultFOV = 60f;
    public float minimumFOV = 25f;
    public Vector3 defaultPosition;

    [Header("Zoom at cursor")]
    [Tooltip("World units moved per scroll notch")]
    public float zoomSpeed = 5f;
    [Tooltip("Clamp camera Y (height)")]
    public float minHeight = 10f;
    public float maxHeight = 100f;
    [Tooltip("Y position of the ground plane used to sample world point under cursor")]
    public float groundY = 0f;

    [Header("Panning")]
    public float panSpeed = 1f;
    public bool cameraSpacePan = true;
    [Tooltip("If true, panning is allowed only when camera is at or below panAllowHeight")]
    public bool panOnlyWhenClose = false;
    public float panAllowHeight = 40f;

    [Header("Optional bounds (XZ)")]
    public bool useBounds;
    public float minX = -13.5f;
    public float maxX = 13.5f;
    public float minZ = -15f;
    public float maxZ = 15f;

    private Vector3 lastMousePos;

    void Start()
    {
        useBounds = true;
        mainCamera.transform.position = defaultPosition;
    }

    void Update()
    {
        if (mainCamera == null) return;

        // Zoom at cursor (position-based)
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0f)
        {
            ZoomAtCursor(scroll);
        }

        // Determine if panning is allowed
        bool allowPan = !panOnlyWhenClose || mainCamera.transform.position.y <= panAllowHeight;

        // Right-mouse drag panning
        if (allowPan)
        {
            if (Input.GetMouseButtonDown(1))
                lastMousePos = Input.mousePosition;

            if (Input.GetMouseButton(1))
            {
                Vector3 delta = Input.mousePosition - lastMousePos;
                if (delta.sqrMagnitude > 0f)
                {
                    Vector3 move;
                    if (cameraSpacePan)
                    {
                        Vector3 right = mainCamera.transform.right;
                        Vector3 forward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
                        // invert so dragging moves the world under the cursor
                        move = (-right * delta.x + -forward * delta.y) * panSpeed * 0.01f;
                    }
                    else
                    {
                        // world X/Z directly
                        move = new Vector3(-delta.x, 0f, -delta.y) * panSpeed;
                    }

                    Vector3 next = mainCamera.transform.position + move;

                    if (useBounds)
                    {
                        next.x = Mathf.Clamp(next.x, minX, maxX);
                        next.z = Mathf.Clamp(next.z, minZ, maxZ);
                    }

                    mainCamera.transform.position = next;
                    lastMousePos = Input.mousePosition;
                }
            }
        }
    }

    private void ZoomAtCursor(float scrollDelta)
    {
        // Zoom IN (towards cursor): preserve world point under cursor
        if (scrollDelta > 0f)
        {
            Vector3 worldBefore = GetWorldPointAtCursor();

            Vector3 camPos = mainCamera.transform.position + mainCamera.transform.forward * scrollDelta * zoomSpeed;
            camPos.y = Mathf.Clamp(camPos.y, minHeight, maxHeight);
            mainCamera.transform.position = camPos;

            Vector3 worldAfter = GetWorldPointAtCursor();

            Vector3 correction = worldBefore - worldAfter;
            correction.y = 0f;
            Vector3 finalPos = mainCamera.transform.position + correction;

            if (useBounds)
            {
                finalPos.x = Mathf.Clamp(finalPos.x, minX, maxX);
                finalPos.z = Mathf.Clamp(finalPos.z, minZ, maxZ);
            }

            mainCamera.transform.position = finalPos;
            return;
        }

        // Zoom OUT (towards defaultPosition): move camera stepwise toward the default overview position
        if (scrollDelta < 0f)
        {
            if (defaultPosition != null)
            {
                Vector3 camPos = mainCamera.transform.position;
                float step = -scrollDelta * zoomSpeed; // scrollDelta is negative when unzooming
                Vector3 next = Vector3.MoveTowards(camPos, defaultPosition, step);

                // clamp height and bounds
                next.y = Mathf.Clamp(next.y, minHeight, maxHeight);
                if (useBounds)
                {
                    next.x = Mathf.Clamp(next.x, minX, maxX);
                    next.z = Mathf.Clamp(next.z, minZ, maxZ);
                }

                mainCamera.transform.position = next;
            }
            else
            {
                // fallback: behave like previous zoom out (preserve cursor point)
                Vector3 worldBefore = GetWorldPointAtCursor();
                Vector3 camPos = mainCamera.transform.position + mainCamera.transform.forward * scrollDelta * zoomSpeed;
                camPos.y = Mathf.Clamp(camPos.y, minHeight, maxHeight);
                mainCamera.transform.position = camPos;
                Vector3 worldAfter = GetWorldPointAtCursor();
                Vector3 correction = worldBefore - worldAfter;
                correction.y = 0f;
                Vector3 finalPos = mainCamera.transform.position + correction;
                if (useBounds)
                {
                    finalPos.x = Mathf.Clamp(finalPos.x, minX, maxX);
                    finalPos.z = Mathf.Clamp(finalPos.z, minZ, maxZ);
                }
                mainCamera.transform.position = finalPos;
            }
        }
    }

    private Vector3 GetWorldPointAtCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0f, groundY, 0f));
        if (ground.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        // fallback: a point in front of the camera
        return mainCamera.transform.position + mainCamera.transform.forward * 10f;
    }
}
