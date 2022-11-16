using System;
using System.Collections.Specialized;

[System.Serializable]

public class BackPack 
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
    public string metadata;
    public string createdAt;
    public string updateAt;
}
