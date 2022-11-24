using GLTFast;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Hammerspace : MonoBehaviour
{
    VisualElement uiRoot;
    [SerializeField] private VisualTreeAsset uiItem; //scrollview item
    [SerializeField] private GameObject loadAvatar;
    private Label categoryLabel;
    private Label sourceLabel;
    private Label type;
    private Button loadButton;
    private Label cid;

    //Testing
    private string id = "c4de4874-51cb-4dc2-9231-0628c5b0c10b";

    private void OnEnable()
    {
        uiRoot = GetComponent<UIDocument>().rootVisualElement;
        categoryLabel = uiRoot.Q<Label>("categoryLabel");
        sourceLabel = uiRoot.Q<Label>("sourcelabel");
        type = uiRoot.Q<Label>("urlLabel");
        GetBackpack();
    }

    private void LoadObject(string url)
    {
        loadAvatar.GetComponent<LoadAvatar>().LoadTheAvatar(url);
        this.gameObject.SetActive(false);
    }
  
    private void GetBackpack()
    {
        Hammerspace hammerspace = HammerspaceAPI.GetHammerspace(id);
        foreach (BackPackItems item in hammerspace.backpackItems)
        {
            var uiTemplate = uiItem.Instantiate();
            uiTemplate.Q<Label>("categoryLabel").text = item.category;
            uiTemplate.Q<Label>("sourceLabel").text = item.source;
            uiTemplate.Q<Label>("typeLabel").text = item.metadata.type;
            uiTemplate.Q<Label>("bodyTypeLabel").text = item.metadata.bodyType;
            uiTemplate.Q<Label>("fileFormatLabel").text = item.metadata.fileFormat;
            uiTemplate.Q<Label>("cid").text = item.content;
            loadButton = uiTemplate.Q<Button>("load");
            loadButton.clicked += () => LoadObject(item.content);
            uiRoot.Q<VisualElement>("itemLayout").Add(uiTemplate);
        }
    }
}
