using System.IO;
using UnityEngine;

public class FoolJsonParser : IJsonParser
{
	static FoolJsonParser instance;
	public static FoolJsonParser Instance
	{
		get
		{
			if (instance == null)
				instance = new FoolJsonParser();
			return instance;
		}
	}
	public T LoadData<T>(string path, string fileName)
	{
		string s = File.ReadAllText(path + fileName);
		T obj = JsonUtility.FromJson<T>(s);
		return obj;
	}

	public void SaveData(object data, string path, string fileName)
	{
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(path + fileName, json);
	}
}
