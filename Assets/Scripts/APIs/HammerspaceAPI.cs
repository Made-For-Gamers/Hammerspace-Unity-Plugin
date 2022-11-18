using Newtonsoft.Json;

/// Hammerspace API Calls

public static class HammerspaceAPI
{    
    //Get all user items from their Hammerspace
    public static Hammerspace GetHammerspace(string id)
    {     
        Hammerspace hammerspace = JsonConvert.DeserializeObject<Hammerspace>(APIHelper.ApiGet(APIHelper.baseAPIUrl + id));
        return hammerspace;
    }
}
