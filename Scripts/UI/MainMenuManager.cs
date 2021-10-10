using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Exposed
    #endregion

    #region Unity API
    #endregion

    #region Main Methods

    public void LoadScene(string scene)
	{
        SceneManager.LoadScene(scene);
	}

    public void QuitGame()
	{
        Application.Quit();
	}

    #endregion

    #region Privates
    #endregion
}
