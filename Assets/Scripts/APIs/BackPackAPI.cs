using System.IO;
using System.Net;
using Newtonsoft.Json;

/// <summary>
/// Hammerspace (Backpack) API Interface
/// </summary>
/// 
public static class BackPackAPI
{
    public static string baseAPIUrl = "https://backend.metabag.dev/backpack/"; //Base URL

    //End Points
    public static string item = "item/"; //Retrieve a specific backpack item associated to the provided id.
    public static string file = "item/file/"; //Create a new backpack item associated to the backpack of the logged in user by providing the file contents directly as base64.
    public static string owner = "owner/"; //Retrieve the backpack associated to the logged in user.
    public static string login = "auth/login/"; //Login to backpack by signing a nonce with an ethereum wallet.  
    public static string request = "auth/request/"; //Retrieve a nonce for login to backpack and create a backpack for the associated user if not existing.
    public static string authorize = "oauth/authorize/"; //Create an authorization request for OAuth2 authorization code flow.
    public static string activation = "oauth/activation"; //Create a new backpack item associated to the backpack of the logged in user by providing the file contents directly as base64.
    public static string token = "oauth/token"; //Create a token based on refresh token or authorization code (adheres to OAuth2 standard)
    public static string application = "oauth/application"; //Retrieve a client application associated to the provided id.
   
    //API GET
    public static string ApiGet(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        var myjson = reader.ReadToEnd();
        return JsonConvert.SerializeObject(myjson);
    }

    public static string GetBackpack(string id)
    {
        return ApiGet(baseAPIUrl + id);
    }
}
