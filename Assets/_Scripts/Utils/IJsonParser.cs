using UnityEngine;

public interface IJsonParser
{
	void SaveData(object data, string path, string fileName);
	T LoadData<T>(string path, string fileName);
}
