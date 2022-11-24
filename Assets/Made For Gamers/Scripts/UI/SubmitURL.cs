using GLTFast;
using UnityEngine;
using UnityEngine.UIElements;

public class SubmitURL : MonoBehaviour
{
    private Button submit;
    private TextField url;
    [SerializeField] private GameObject modelLoader;

    void Start()
    {
        VisualElement uiRoot = GetComponent<UIDocument>().rootVisualElement;
        submit = uiRoot.Q<Button>("submit");
        url = uiRoot.Q<TextField>("url-input");
        submit.clicked += () => LoadAvatar();
    }

    private void LoadAvatar()
    {
        modelLoader.GetComponent<LoadAvatar>().LoadTheAvatar(url.value);
    }




}
