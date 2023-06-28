using System;
using Newtonsoft.Json;
using UnityEngine;

namespace LeaderboardApp.Scripts.Services.ConfigLoader
{
    public class ConfigResourcesLoaderService : IConfigService
    {
        public T Load<T>(string path)
        {
            TextAsset data = Resources.Load<TextAsset>(path);
            
            if (string.IsNullOrEmpty(data.text))
                throw new Exception("JSON File have not exist.");
            
            var result = JsonConvert.DeserializeObject<T>(data.text);
            if (result == null)
                throw new Exception("JsonUtility failed to set the data.");
            
            return result;
        }
    }
}