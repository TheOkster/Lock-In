using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseMovement : NetworkBehaviour
{
    public float mouseSensitivity = 600f;
    public GameObject playerBody;
    public GameObject gunBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock cursor and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        // Mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float yRotation = mouseX;
        playerBody.GetComponent<PlayerMovement>().leftRightTurn(yRotation);

        // Shooting
        bool shoot = Input.GetKey(KeyCode.Mouse0);
        if (shoot&&!EventSystem.current.IsPointerOverGameObject())
        {
            gunBody.GetComponent<Weapon>().PressShoot();
        }

        // Movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        playerBody.GetComponent<PlayerMovement>().move(direction, isRunning);

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            playerBody.GetComponent<PlayerMovement>().jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Unlock cursor and making it visible
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float xRotation = -mouseY;
        playerBody.GetComponent<PlayerMovement>().upDownTurn(xRotation);
    }
}
