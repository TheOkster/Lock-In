using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUIManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
