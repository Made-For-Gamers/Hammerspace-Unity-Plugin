using System.Collections.Specialized;
using UnityEngine;

public class API_Calls : MonoBehaviour
{
    [SerializeField] string id = "c4de4874-51cb-4dc2-9231-0628c5b0c10b";

    private void Start()
    {
        print(BackPackAPI.GetBackpack(id));
        
    }
}
