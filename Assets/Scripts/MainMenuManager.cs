using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject instructionsPanel;
    public GameObject mainMenuButtons;
    public Button creditsButton;
    public Button instructionsButton;
    public Button startButton;
    public Button creditsCloseButton;
    public Button instructionsCloseButton;
    public Button quitButton;

    void Start()
    {
        creditsButton.onClick.AddListener(ShowCredits);
        instructionsButton.onClick.AddListener(ShowInstructions);
        startButton.onClick.AddListener(StartNewGame);
        creditsCloseButton.onClick.AddListener(HideCredits);
        instructionsCloseButton.onClick.AddListener(HideInstructions);
        quitButton.onClick.AddListener(QuitGame);
    }
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
            mainMenuButtons.SetActive(false);
        }
    }
    public void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
            mainMenuButtons.SetActive(true);
        }

    }
    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
            mainMenuButtons.SetActive(false);
        }
    }

    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
            mainMenuButtons.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("LowPolyFPS_Lite_Demo");
    }
}
