using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [SerializeField]
    private float maxJumpForce = 70.0f;          //  How long the player can jump
    [SerializeField]
    private float jumpForceIncrement = 0.2f;     //  How fast the player can load the jump power
    [SerializeField]
    private float maxPunchForce = 100.0f;        //  How strong the player cna punch
    [SerializeField]
    private float punchForceIncrement = 0.3f;    //  How fast the player load the punch power

    public bool isCharging { get; set; }                     //  Controls whether the player is charging a power

    //  Variables
    private float m_CurrentJumpForce;
    private float m_CurrentPunchForce;
    private bool isCharged;
    private bool punchSkill;
    private bool jumpSkill;

    //  Components
    private PlayerMovement movement;
    private Animator m_Animator;
    private CharacterController controller;
    private PunchPhysics punch;

    private void Start()
    {
        //  Get the components
        movement = GetComponent<PlayerMovement>();
        m_Animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        punch = GetComponentInChildren<PunchPhysics>();
    }

    private void Update()
    {
        //  Control if the player is already doing any of his powers
        if (!punchSkill)
            JumPower();
        if (!jumpSkill)
            PunchPower();
    }

    private void PunchPower()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButton("Fire1"))
            {
                //  Do the animation to charge the punch power
                m_Animator.SetBool("IsPunchPower", true);
                ChargePunchPower();
                punchSkill = true;
            }
        }
        else
        {
            //  Cancel the power casting of the player
            CancelPowers();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (isCharged)
            {
                m_Animator.SetBool("Punch", true);
                PunchForce();
            }
            m_Animator.SetFloat("PunchPower", 0);
            m_Animator.SetBool("IsPunchPower", false);
            CancelPowers();
        }
    }

    private void ChargePunchPower()
    {
        if (m_CurrentPunchForce >= maxPunchForce)
        {
            //  Punch
            m_Animator.SetFloat("PunchPower", 1);
            isCharged = true;
        }
        else
        {
            //  Charge the power
            isCharging = true;
            m_CurrentPunchForce += punchForceIncrement;
            m_Animator.SetFloat("PunchPower", 0);
            m_Animator.SetBool("Punch", false);
        }
    }

    private void JumPower()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButton("JumpPower"))
            {
                //  Do the animation to charge the jump power
                m_Animator.SetBool("IsJumpPower", true);
                ChargeJumpPower();
                jumpSkill = true;
            }
        }
        else
        {
            //  Cancel the power casting of the player
            CancelPowers();
        }

        if (Input.GetButtonUp("JumpPower"))
        {
            m_Animator.SetFloat("JumpPower", 0);
            m_Animator.SetBool("IsJumpPower", false);
            CancelPowers();
        }
    }

    private void ChargeJumpPower()
    {
        if (m_CurrentJumpForce >= maxJumpForce)
        {
            //  Jump
            m_Animator.SetFloat("JumpPower", 1);
            movement.JumpPower(maxJumpForce);
        }
        else
        {
            //  Charge the power
            isCharging = true;
            m_CurrentJumpForce += jumpForceIncrement;
            m_Animator.SetFloat("JumpPower", 0);
            movement.JumpPower(m_CurrentJumpForce);
        }
    }

    private void CancelPowers()
    {
        //  Make all the variables to default value
        movement.JumpPower(movement.JumpForce);
        m_CurrentJumpForce = 0;
        m_CurrentPunchForce = 0;
        isCharging = false;
        isCharged = false;

        punchSkill = false;
        jumpSkill = false;
    }

    private void PunchForce()
    {
        //  Apply the force of the punch collision
        punch.CollisionForce(transform.forward);
    }
}
