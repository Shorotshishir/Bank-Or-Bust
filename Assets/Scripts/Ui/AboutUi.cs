using UnityEngine;
using UnityEngine.UIElements;

public class AboutUi : MonoBehaviour
{
    [SerializeField] private UiHandler uiHandler;

    private Label appversion;
    private Label buildnumber;

    private Button toStart;

    public void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        toStart = uiDocument.rootVisualElement.Q("to_start") as Button;
        appversion = uiDocument.rootVisualElement.Q<Label>("abt_version");
        buildnumber = uiDocument.rootVisualElement.Q<Label>("abt_build");

        toStart?.RegisterCallback<ClickEvent>(OnToStartClick);
        UpdateAboutUi();
    }

    private void OnDisable()
    {
        toStart?.UnregisterCallback<ClickEvent>(OnToStartClick);
    }

    private void UpdateAboutUi()
    {
        appversion.text = $"Version: {Application.version}";
        buildnumber.text = string.Empty;
    }

    private void OnToStartClick(ClickEvent evt)
    {
        uiHandler.ShowStartUi();
    }
}