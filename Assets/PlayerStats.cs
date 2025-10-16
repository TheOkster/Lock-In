using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    private Slider healthSlider;
    public NetworkVariable<int> health = new NetworkVariable<int>();
    private int maxHealth = 10;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health.Value = maxHealth;
    }

    void Start()
    {
        healthSlider = FindFirstObjectByType<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update() { }

    public void Damage(int damage_dealt)
    {
        health.Value -= damage_dealt;
        healthSlider.value = health.Value;
    }
}
