using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(
        10,  // Default value
        NetworkVariableReadPermission.Everyone,  // All can read
        NetworkVariableWritePermission.Server    // Only server writes
    );
    public Slider healthSlider;
    private int maxHealth = 10;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            health.Value = maxHealth;
        }
        health.OnValueChanged += changeValue;
    }

    void changeValue(int oldValue, int newValue)
    {
        healthSlider.value = newValue;
    }

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update() { }

    [ServerRpc(RequireOwnership = false)] public void damageServerRpc(int damageDealt)
    {
        if (!IsServer) return;
        health.Value -= damageDealt;
    }
}
