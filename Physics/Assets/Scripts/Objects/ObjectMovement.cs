using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;      //  Speed of the player
    [SerializeField]
    private float jumpForce = 10.0f; //  Force of the jump

    private Rigidbody m_Rigidbody;
    private Vector3 m_MovementInput;
    private Vector3 m_PlayerMovement;

    private void Start()
    {
        //  Get the Rigibody component
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        m_MovementInput.Set(horizontalMovement, 0.0f, verticalMovement);

        Jump();

        m_PlayerMovement = m_MovementInput * speed;

        m_Rigidbody.MovePosition(transform.position + m_PlayerMovement * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_Rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
