using System.Text;
using System.IO;                                              // File Mode: İşletim sisteminin dosyayı nasıl açacağı
using System.Collections;                                     // Create: new file oluşturur. file varsa overwrites
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavingSystem : MonoBehaviour
{
    public void Save(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        Debug.Log("saving to:" + path);

        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            Transform playerTransform = GetplayerTransform();
            byte[] buffer = SerializeVector(playerTransform.position);
            stream.Write(buffer, 0, buffer.Length);
        }
    }

    public void Load(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        Debug.Log("loading from:" + GetPathFromSaveFile(saveFile));

        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            Transform playerTransform = GetplayerTransform();
            playerTransform.position = DeserializeVector(buffer);

            //Encoding.UTF8.GetString(buffer);
        }

    }
    private Transform GetplayerTransform()
    {
        return GameObject.FindWithTag("Player").transform;
    }
    private byte[] SerializeVector(Vector3 vector)
    {
        byte[] vectorBytes = new byte[3 * 4]; //bi float 4 byte olduğu için
        BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
        BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
        BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
        return vectorBytes;
    }
    private Vector3 DeserializeVector(byte[] buffer)
    {
        Vector3 vector = new Vector3();
        vector.x= BitConverter.ToSingle(buffer, 0); //ToSingle (byte[], int32) okur. 4 byte'a kadar okur kısaca.
        vector.y= BitConverter.ToSingle(buffer, 4);
        vector.z= BitConverter.ToSingle(buffer, 8);
        return vector;
    }


    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
