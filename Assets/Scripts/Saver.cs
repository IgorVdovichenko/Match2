using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Saver<T>
{
	private string saveFileName = "save.dat";
	private string fileName;
	private FileStream fileStream;
	private BinaryFormatter formatter = new BinaryFormatter();

	public Saver()
	{
		fileName = string.Format("{0}/{1}", Application.persistentDataPath, saveFileName);
		Debug.Log(fileName);
	}

	public void SaveData(T data)
	{
		fileStream = new FileStream(fileName, FileMode.Create);
		formatter.Serialize(fileStream, data);
		fileStream.Close();
	}

	public T GetData()
	{
		T data = default(T);
		FileInfo fileInfo = new FileInfo(fileName);
		if(fileInfo.Exists)
		{
			fileStream = new FileStream(fileName, FileMode.Open);
			data = (T)formatter.Deserialize(fileStream);
			fileStream.Close();
		}
		return data;
	}
}
