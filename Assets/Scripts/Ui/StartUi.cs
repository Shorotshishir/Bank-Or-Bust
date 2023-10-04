using UnityEngine;
using UnityEngine.UIElements;

public class StartUi : MonoBehaviour
{
    [SerializeField] private UiHandler uiHandler;
    private Button about;

    private Button play;

    public void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        play = uiDocument.rootVisualElement.Q("play") as Button;
        about = uiDocument.rootVisualElement.Q("about") as Button;

        play?.RegisterCallback<ClickEvent>(OnPlayClick);
        about?.RegisterCallback<ClickEvent>(OnAboutClick);
    }

    private void OnDisable()
    {
        play?.UnregisterCallback<ClickEvent>(OnPlayClick);
        about?.UnregisterCallback<ClickEvent>(OnAboutClick);
    }

    private void OnAboutClick(ClickEvent evt)
    {
        uiHandler.ShowAboutUi();
    }

    private void OnPlayClick(ClickEvent evt)
    {
        uiHandler.ShowMainUi();
    }
}