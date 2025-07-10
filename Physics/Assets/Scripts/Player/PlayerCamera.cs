using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera movementCamera;
    [SerializeField]
    private Transform cameraLookAt; //  Target which the camer will look at and follow
    [SerializeField]
    private Transform cameraTarget; //  Target that moves with the player and the camera follows the position

    [SerializeField]
    private float xSensitivity = 300f;
    [SerializeField]
    private float ySensitivity = 200f;
    [SerializeField]
    private float minY = -80f;
    [SerializeField]
    private float maxY = 80f;

    private float xAxis;
    private float yAxis;
    private float currentX = 0f;
    private float currentY = 0f;

    PlayerPowers powers;

    private void Start()
    {
        //  Lock and hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //  Get the powers component
        powers = GetComponent<PlayerPowers>();
    }

    private void FixedUpdate()
    {
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");

        currentX += xAxis * xSensitivity * Time.fixedDeltaTime;
        currentY -= yAxis * ySensitivity * Time.fixedDeltaTime;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // Apply rotation to the camera look-at target
        cameraLookAt.rotation = Quaternion.Euler(currentY, currentX, 0f);
    }

    private void Update()
    {
        //  Follow the position of the player with the target object
        cameraLookAt.position = Vector3.MoveTowards(cameraLookAt.position, cameraTarget.position, 10.0f);

        if (powers.isCharging)
        {
            //  Rotate the player with the camera
            float axisCamera = Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, axisCamera, 0), 20 * Time.deltaTime);
        }
    }
}
