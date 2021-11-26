using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float targetHeight = 1.7f;
    public float distance = 12.0f;
    public float offsetFromWall = 0.1f;
    public float maxDistance = 20;
    public float minDistance = 0.6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public float yMinLimit = -80;
    public float yMaxLimit = 80;
    public float zoomRate = 40;
    public float rotationDampening = 3.0f;
    public float zoomDampening = 5.0f;
    LayerMask collisionLayers = -1;

    public bool lockToRearOfTarget;
    public bool allowMouseInputX = true;
    public bool allowMouseInputY = true;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    public float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind;

    public GameObject userModel;
    public bool inFirstPerson;

    Rigidbody rigidbody;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        if (rigidbody)
            rigidbody.freezeRotation = true;

        if (lockToRearOfTarget)
            rotateBehind = true;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (inFirstPerson == true)
            {
                minDistance = 10;
                desiredDistance = 15;
                userModel.SetActive(true);
                inFirstPerson = false;
            }
        }

        if (desiredDistance == 10)
        {
            minDistance = 0;
            desiredDistance = 0;
            userModel.SetActive(false);
            inFirstPerson = true;
        }
    }

    void LateUpdate()
    {
        if (!target)
            return;

        if (GUIUtility.hotControl == 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

            }
            else
            {
                if (Input.GetMouseButton(1))
                {
                    if (allowMouseInputX)
                        xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    if (allowMouseInputY)
                        yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                }
            }
        }
        ClampAngle(yDeg);

        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        Vector3 vTargetOffset = new Vector3(0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
        {
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

        transform.rotation = rotation;
        transform.position = position;
    }

    void ClampAngle(float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        yDeg = Mathf.Clamp(angle, -60, 80);
    }
}
