using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private SaveableEntity[] _saveableEntities;     // Reference to ALL saveable entities
    private BinaryFormatter _formatter;                              // binary formatter
    private string _gameSavePath;                                    // file save path

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        _gameSavePath = Application.persistentDataPath + "/MarioGameSave.txt";
        _formatter = new BinaryFormatter();
    }

    public void Save()
    {
        var state = new Dictionary<string, object>();
        CaptureState(state);
        SaveFile(state);
    }

    public void Load()
    {
        var state = LoadFile();                                      // Load file and restore data
        RestoreState(state);
    }

    private void SaveFile(object state)
    {
        FileStream stream = new FileStream(_gameSavePath, FileMode.Create);
        _formatter.Serialize(stream, state);
        stream.Close();
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(_gameSavePath))
            return new Dictionary<string, object>();

        FileStream stream = new FileStream(_gameSavePath, FileMode.Open);
        var data = (Dictionary<string, object>)_formatter.Deserialize(stream);
        stream.Close();

        return data;
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in _saveableEntities)
            state.Add(saveable.gameObject.name, saveable.CaptureState());
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in _saveableEntities)
            if (state.TryGetValue(saveable.gameObject.name, out object value))
                saveable.RestoreState(ref value);
    }



}
