using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject crosshair;
    void Start()
    {
        Debug.Log("UIManager Start running!");
        Instantiate(crosshair);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
