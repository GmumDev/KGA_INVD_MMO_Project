using System.IO;
using UnityEngine;

public class JsonParser: IJsonParser
{
	static JsonParser instance;
	public static JsonParser Instance
	{
		get
		{
			if (instance == null)
				instance = new JsonParser();
			return instance;
		}
	}
	public void SaveData(object data, string path, string fileName)
	{
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(path + fileName, json);
	}
	public T LoadData<T>(string path, string fileName)
	{
		string s = File.ReadAllText(path + fileName);
		T obj = JsonUtility.FromJson<T>(s);
		return obj;
	}
}
