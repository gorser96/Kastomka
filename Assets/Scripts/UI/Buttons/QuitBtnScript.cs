using UnityEngine;

public class QuitBtnScript : MonoBehaviour
{
    public void OnClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
