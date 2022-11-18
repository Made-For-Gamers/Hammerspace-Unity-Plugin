using GLTFast.FakeSchema;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Hammerspace : MonoBehaviour
{
    VisualElement uiRoot;
    [SerializeField] private VisualTreeAsset uiItem; //scrollview item
    private Label categoryLabel;
    private Label sourceLabel;
    private Label type;

    //Testing
   private string id = "c4de4874-51cb-4dc2-9231-0628c5b0c10b";

    private void Start()
    {
        uiRoot = GetComponent<UIDocument>().rootVisualElement;
        categoryLabel = uiRoot.Q<Label>("name");
        sourceLabel = uiRoot.Q<Label>("sourcelabel");
        type = uiRoot.Q<Label>("urlLabel");
        GetBackpack();
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
            uiRoot.Q<VisualElement>("itemLayout").Add(uiTemplate);
        }
    }
}
