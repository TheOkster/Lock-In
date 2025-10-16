using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public NetworkVariable<int> health = new NetworkVariable<int>();
    public int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update() { }

    public void Damage(int damage_dealt)
    {
        health.Value -= damage_dealt;
        healthSlider.value = health.Value;
    }
}
