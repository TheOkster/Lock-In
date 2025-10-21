using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private CharacterController controller;
    private Animator animator;
    public Camera camera;
    public float maxSpeed = 12f;
    public float gravity = 9.81f * -1;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private AudioListener otherAudioListener;
    public float walkSoundPeriod = 0.4f;
    public float runSoundPeriod = 0.3f;
    private float curStepTime = 0f;

    public Transform cameraTransform;
    private float leftRightAngle = 0f;
    Vector3 velocity;
    bool isGrounded;
    bool jumping = false;
    bool inAir = false;

    Transform chest;
    Quaternion torsoRotation;
    float upDownAngle = 0f;
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    public override void OnNetworkSpawn()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        torsoRotation = chest.localRotation; 
        upDownAngle = cameraTransform.rotation.eulerAngles.x;

        if (!IsOwner)
        {
            otherAudioListener = camera.GetComponent<AudioListener>();
            camera.enabled = false;
            otherAudioListener.enabled = false;
        }
        else
        {
            camera.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsHost || IsServer)
            {
                // Host sends everyone to main menu
                Debug.Log("Host is sending everyone to Main Menu");
                LoadMainMenuClientRpc();
            }
            else
            {
                // Client leaves on their own
                Debug.Log("Client leaving to Main Menu");
                SceneManager.LoadScene("MainMenu");
            }
        }
        if (!IsOwner)
        {
            return;
            //cameraTransform.enabled = false;
        }
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (!isGrounded && jumping) // in air
        {
            inAir = true;
            jumping = false;
        }
        if (isGrounded && inAir) // landed
        {
            inAir = false;
            audioSource.volume = 1f;
            audioSource.PlayOneShot(landSound);
            landSoundServerRpc(audioSource.transform.position);
        }

        // Gravity and jumps
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void jump()
    {
        if (isGrounded && !jumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioSource.volume = 1f;
            audioSource.PlayOneShot(jumpSound);
            jumpSoundServerRpc(audioSource.transform.position);
            jumping = true;
        }
    }

    public void upDownTurn(float turnAngle)
    {
        if (!IsOwner) return;
        Vector3 localAxis = chest.parent.InverseTransformDirection(cameraTransform.right);
        // Create a rotation around that local axis
        float newUpDownAngle = Mathf.Clamp(upDownAngle + turnAngle, topClamp, bottomClamp);
        Quaternion rotation = Quaternion.AngleAxis(newUpDownAngle - upDownAngle, localAxis);
        upDownAngle = newUpDownAngle;
        // Apply rotation relative to parent
        torsoRotation = rotation * torsoRotation;
        chest.localRotation = torsoRotation;
    }

    public void move(Vector3 direction, bool isRunning)
    {
        float speedRatio = isRunning ? 1 : 0.7f;
        controller.Move(cameraTransform.TransformDirection(direction) * maxSpeed * speedRatio * Time.deltaTime);
        audioSource.volume = isRunning ? 1.0f : 0.7f;

        if (direction.magnitude > 0 && isGrounded)
        {
           if (curStepTime > (isRunning ? runSoundPeriod : walkSoundPeriod))
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
            curStepTime = Math.Max(runSoundPeriod, walkSoundPeriod);
        }
    }

    void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepSound);
        walkSoundServerRpc(audioSource.transform.position);
    }

    public void leftRightTurn(float turnAngle)
    {
        leftRightAngle += turnAngle;
        transform.rotation = Quaternion.Euler(0, leftRightAngle, 0f);
    }

    [ServerRpc] void landSoundServerRpc(Vector3 location) 
    {
        landSoundClientRpc(location);
    }

    [ClientRpc] void landSoundClientRpc(Vector3 location) 
    {
        if (IsOwner) return;
        AudioSource.PlayClipAtPoint(landSound, location);
    }

    [ServerRpc] void jumpSoundServerRpc(Vector3 location) 
    {
        jumpSoundClientRpc(location);
    }

    [ClientRpc] void jumpSoundClientRpc(Vector3 location) 
    {
        if (IsOwner) return;
        AudioSource.PlayClipAtPoint(jumpSound, location);
    }

    [ServerRpc] void walkSoundServerRpc(Vector3 location) 
    {
        walkSoundClientRpc(location);
    }

    [ClientRpc] void walkSoundClientRpc(Vector3 location)
    {
        if (IsOwner) return;
        AudioSource.PlayClipAtPoint(footstepSound, location);
    }
    
    [ClientRpc] void LoadMainMenuClientRpc()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
