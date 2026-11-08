using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject optionMenuObject;

    private void Start()
    {
        optionMenuObject.SetActive(false);
    }

    public void OptionButton()
    {
        mainMenuObject.SetActive(false);
        optionMenuObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        mainMenuObject.SetActive(true);
        optionMenuObject.SetActive(false);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
