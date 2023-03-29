using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PropertyController : MonoBehaviour
{

    public static Dictionary<string, string> data = new Dictionary<string, string>();

    public static void ReadProperty(string path)
    {
        foreach (string line in File.ReadAllLines(path))
        {
            // Ignore comment lines and empty lines
            if (line.StartsWith(";") || string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            int equalsIndex = line.IndexOf('=');

            // Ignore lines without an equals sign
            if (equalsIndex < 0)
            {
                continue;
            }

            string key = line.Substring(0, equalsIndex).Trim();
            string value = line.Substring(equalsIndex + 1).Trim();

            // Add the key-value pair to the dictionary
            data[key] = value;
        }
    }

    public static void WriteProperty(string path, string key, string value)
    {
        ReadProperty(path);

        data[key] = value;

        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (KeyValuePair<string, string> pair in data)
            {
                writer.WriteLine($"{pair.Key}={pair.Value}");
            }
        }

    }

    public static string GetValueOfKey(string path, string key)
    {
        string value = null;

        // Read all lines from the INI file
        string[] lines = File.ReadAllLines(path);

        // Loop through each line
        foreach (string line in lines)
        {
            // Ignore comment lines and empty lines
            if (line.StartsWith(";") || string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            int equalsIndex = line.IndexOf('=');

            // Ignore lines without an equals sign
            if (equalsIndex < 0)
            {
                continue;
            }

            string lineKey = line.Substring(0, equalsIndex).Trim();
            string lineValue = line.Substring(equalsIndex + 1).Trim();

            // Check if the key matches
            if (lineKey == key)
            {
                value = lineValue;
                break;
            }
        }

        return value;
    }
}