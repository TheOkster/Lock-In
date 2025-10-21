using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject creditsButton;
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
            creditsButton.SetActive(false);
        }
    }
    public void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
            creditsButton.SetActive(true);
        }
            
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("LowPolyFPS_Lite_Demo");
    }
}
