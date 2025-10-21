using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }
    public void HideCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("LowPolyFPS_Lite_Demo");
    }
}
