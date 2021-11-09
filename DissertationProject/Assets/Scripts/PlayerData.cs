using System;
[System.Serializable]
public class PlayerData
{
    public byte[] guidAsBytes;
    //public string guidAsString;

    public PlayerData(Guid guid)
    {
        guidAsBytes = guid.ToByteArray();
        //guidAsString = guid.ToString();
    }
}
