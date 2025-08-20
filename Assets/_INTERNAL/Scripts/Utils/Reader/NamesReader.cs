using System.Collections.Generic;
using UnityEngine;

namespace Utils.Reader
{
    public static class NamesReader
    {
        public static List<string> LoadFromTextAsset(TextAsset file)
        {
            List<string> result = new();
            if (file == null) return result;

            string[] lines = file.text.Split(new[] {'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                result.Add(line.Trim());
            }
            return result;
        }
    }
}