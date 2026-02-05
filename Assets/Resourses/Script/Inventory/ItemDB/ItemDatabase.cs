using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class ItemDatabase
{
    private static Dictionary<int, ItemConfig> _database = new Dictionary<int, ItemConfig>();
    private static bool _isLoaded = false;
    
    public static void Load()
    {
        if (_isLoaded) return;
        
        string path = Path.Combine(Application.streamingAssetsPath, "item_configs.json");

        if (!File.Exists(path))
        {
            Debug.LogError($"[ItemDatabase] Файл не найден по пути: {path}");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(path);
            ItemConfigList list = JsonUtility.FromJson<ItemConfigList>(jsonContent);

            foreach (var config in list.items)
            {
                _database[config.id] = config;
            }

            _isLoaded = true;
            Debug.Log($"[ItemDatabase] База загружена! Предметов: {_database.Count}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ItemDatabase] Ошибка парсинга: {e.Message}");
        }
    }

    public static ItemConfig GetConfig(int id)
    {
        if (!_isLoaded) Load();
        return _database.TryGetValue(id, out var config) ? config : null;
    }
    
    [Serializable]
    public class ItemConfig
    {
        public int id;
        public string displayName;
        public int maxStack;
    }

    [Serializable]
    private class ItemConfigList
    {
        public ItemConfig[] items;
    }
}