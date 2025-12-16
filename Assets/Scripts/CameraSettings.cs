using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Camera mainCamera;
    public float defaultFOV = 60f;
    public float minimumFOV = 25f;
    public Vector3 defaultPosition;

    public float zoomSpeed = 5f;

    public float minHeight = 10f;
    public float maxHeight = 100f;

    public float groundY = 0f;

    public float panSpeed = 1f;
    public bool cameraSpacePan = true;

    public bool panOnlyWhenClose = false;
    public float panAllowHeight = 40f;

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

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0f)
        {
            ZoomAtCursor(scroll);
        }

        bool allowPan = !panOnlyWhenClose || mainCamera.transform.position.y <= panAllowHeight;

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
                        move = (-right * delta.x + -forward * delta.y) * panSpeed * 0.01f;
                    }
                    else
                    {
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

        if (scrollDelta < 0f)
        {
            if (defaultPosition != null)
            {
                Vector3 camPos = mainCamera.transform.position;
                float step = -scrollDelta * zoomSpeed; 
                Vector3 next = Vector3.MoveTowards(camPos, defaultPosition, step);

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

        return mainCamera.transform.position + mainCamera.transform.forward * 10f;
    }
}
