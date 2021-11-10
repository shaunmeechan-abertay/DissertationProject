//This class will handle creating and managing the GUID (Unique ID)

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.IO;


//GUID for the interface
[Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4")]
interface IGUIDInterface
{
    void generateGUID();
}

[Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8")]
public class GUIDHelper : MonoBehaviour, IGUIDInterface
{
    public Guid uid;
    // Start is called before the first frame update
    void Start()
    {
        //We need to check if a file exists that already has the UID (TODO: THIS)
        //If we don't find one generate
        if(File.Exists(Application.persistentDataPath + "/player.agf"))
        {
            //Do something
            Debug.Log("Found file...");
            PlayerData loadedData = SaveSystem.LoadUID();
            uid = new Guid(loadedData.guidAsBytes);
            Debug.Log("Loaded UID is: " + uid);
        }
        else
        {
            Debug.Log("Didn't find file...");
            //Generate Guid
            uid = Guid.NewGuid();
            if(uid == Guid.Empty)
            {
                Debug.LogError("ERROR: Failed to generate a UID!");
            }
            else
            {
                print("UID: " + uid);
                SaveSystem.SaveUID(uid);
            }
        }
    }

    public string getUIDAsString()
    {
        return uid.ToString();
    }

    public void generateGUID() { }

}
