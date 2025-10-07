using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = 9.81f * -2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float stepPeriod = 0.75f;
    public float curStepTime = 0f;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving = false;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground cechk
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;

        if (isMoving)
        {
            if (curStepTime > stepPeriod)
            {
                PlayFootstep();
                curStepTime = 0f;
            }
            else
            {
                curStepTime += Time.deltaTime;
            }
        }
        else
        {
            curStepTime = 0f;
        }
    }

    void PlayFootstep()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        int footstepInd = UnityEngine.Random.Range(0, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[footstepInd]);
    }
}
