using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Serialization;



public class TimelineData : ScriptableObject
{
    [System.Serializable]
    public class TimelineItem
    {
        // These need to be the same as the column headers 
        public int id;
        public string title;
        public string details;
        public int year;
        public string image;
        public string source;
        public bool inPlay = false; // is this item already in play (inHand/onBoard)
        
        
        public override string ToString()
        {
            return $"TimelineItem<[{id}]({title}:{year}, inplay:{inPlay}>";
        }
    }


    public TimelineItem[] _items;
    private List<int> _availableIndices;


    public void SetInitialReferences()
    {
        _availableIndices = new List<int>(_items.Length);
        Debug.Log($"Loaded TimelineData: {_items.Length} items");
        for (int i = 0; i < _items.Length; i++)
        {
            _availableIndices.Add(i);
        }
        _availableIndices.Sort((a, b) => _items[a].year.CompareTo(_items[b].year));
    }


    public TimelineItem GetItem(bool randomItem=true)
    {
        int index = 0;
        if (randomItem)
        {
            index = Random.Range(0, _availableIndices.Count);
        }
        return GetItem(_availableIndices[index]);
    }


    private TimelineItem GetItem(int index)
    {
        if (
            index < 0 || index >= _items.Length ||
            !_availableIndices.Contains(index) ||
            _availableIndices.Count == 0
        )
        {
            return null;
        }
        Debug.Log($"GetItem: {_items[index]} ({AvailableItemCount}/{ItemCount})");
        _availableIndices.Remove(index);
        _items[index].inPlay = true;
        return _items[index];
    }


    public int ItemCount
    {
        get => _items.Length;
    }


    public int AvailableItemCount
    {
        get => _availableIndices.Count;
    }
}

#if UNITY_EDITOR
/// This part of the script can only be used in the editor.
/// It will not be built into the final game.
public class ReimportDataFromCSV : MonoBehaviour
{
    /// <summary>
    /// Menu item to reimport the data from the CSV file
    /// The data is loaded from Assets/Data/TimelineData.csv and saved to Assets/Resources/TimelineData.asset
    /// </summary>
    [MenuItem("TimelineData/reimport")]
    public static void ReimportData()
    {
        string path = "Assets/Data/TimelineData.csv";
        string assetfile = "Assets/Resources/TimelineData.asset";
        TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        TimelineData gm = AssetDatabase.LoadAssetAtPath<TimelineData>(assetfile);
        if (gm == null)
        {
            gm = new TimelineData();
            AssetDatabase.CreateAsset(gm, assetfile);
        }

        gm._items = CSVSerializer.Deserialize<TimelineData.TimelineItem>(data.text);

        EditorUtility.SetDirty(gm);
        AssetDatabase.SaveAssets();
        Debug.Log("Reimported Asset: " + path);
    }
}

#endif