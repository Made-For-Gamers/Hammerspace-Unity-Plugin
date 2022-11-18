[System.Serializable]

public class Hammerspace
{
    public string id;
    public string owner;
    public string createdAt;
    public string updatedAt;
    public BackPackItems[] backpackItems;
}

public class BackPackItems
{
    public string id;
    public string content;
    public string source;
    public string category;
    public MetaData metadata;
    public string createdAt;
    public string updatedAt;
}

public class MetaData
{
    public string type;
    public string source;
    public string bodyType;
    public string fileFormat;
}


