using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController characterController;
    public float rotationSpeed = 10f;
    public Transform maincam;

    Vector3 offset; // camera-player offset store karega

    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float acceleration = 10f; // smooth speed transition
    public float currentSpeed;

    void Start()
    {
        offset = maincam.position - transform.position; // 🎯 Offset store
    }

    void Update()
    {

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Smoothly change speed using Lerp
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 endPoint = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = (endPoint - transform.position).normalized;
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction);

       // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1000f * Time.deltaTime);

        // Camera follow with stored offset
        maincam.position = transform.position + offset;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movedir = new Vector3(horizontal, 0, vertical);

        // Movement
        characterController.Move(movedir * currentSpeed * Time.deltaTime);
      

        // Rotation only when moving
        if (movedir != Vector3.zero)
        {
            Quaternion targetRotationz = Quaternion.LookRotation(movedir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationz, rotationSpeed * Time.deltaTime);
        }
    }
}
