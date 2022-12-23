using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControll : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "Pause":
                pauseMenu.SetActive(true);
                break;
            
            case "Back":
                pauseMenu.SetActive(false);
                break;
            
            case "Restart":
                SceneManager.LoadScene("Game");
                break;
            
            case "ExitToMenu":
                SceneManager.LoadScene("Menu");
                break;
            
            case "Lvl1":
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Level", 0);
                SceneManager.LoadScene("Game");
                break;
            case "Lvl2":
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Level", 1);
                SceneManager.LoadScene("Game");
                break;
            case "ExitTheGame":
                Application.Quit();
                break;
        }
    }
}
