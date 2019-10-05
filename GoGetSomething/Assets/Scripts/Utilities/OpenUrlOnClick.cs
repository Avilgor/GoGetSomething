using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrlOnClick : MonoBehaviour
{
    [SerializeField] private string _androidUrl;
    [SerializeField] private string _iosUrl;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenUrl);
    }

    private void OpenUrl()
    {
        var correctUrl = "";

#if UNITY_ANDROID

        if (_androidUrl == "")
        {
            Debug.LogError("You must set an Url!");
            return;
        }

        correctUrl = _androidUrl;
    #else
        if (_iosUrl == "")
        {
            Debug.LogError("You must set an Url!");
            return;
        }

        correctUrl = _iosUrl;
#endif

        Application.OpenURL(correctUrl);
    }
}