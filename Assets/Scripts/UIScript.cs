using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hostButton.onClick.AddListener(HostButtonOnClick);
        clientButton.onClick.AddListener(ClientButtonOnClick);

        var c = GetComponent<Canvas>();
        if (!c) return;
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.targetDisplay = 0; // Display 1 in the inspector
        var g = GetComponent<CanvasGroup>();
        if (g) g.alpha = 1f;
        gameObject.SetActive(true);
        Debug.Log($"[UI] Display:{c.targetDisplay} Res:{Screen.width}x{Screen.height}");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void HostButtonOnClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void ClientButtonOnClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
