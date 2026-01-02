using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void OnAcceptCustomer()
    {
        DayManager.Instance.AcceptCustomer();
    }

    public void OnRefuseCustomer()
    {
        DayManager.Instance.RefuseCustomer();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
