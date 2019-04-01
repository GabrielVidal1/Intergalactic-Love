using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public static class SaveLoad
{
    public static void SaveGame()
    {
        Save save = new Save();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.save");
        bf.Serialize(file, save);

        Debug.Log("Save at: " + Application.persistentDataPath + "/save.save");
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);

            file.Close();

            save.Load();
        }
    }

}

[System.Serializable]
public class Save
{
    public string discoveredRecipes;

    public string playerInventory;

    public Save()
    {
        StringBuilder sb = new StringBuilder();

        foreach (bool item in GameManager.gm.recipeManager.hasDiscoveredRecipe)
            sb.Append(item ? "1" : "0");

        discoveredRecipes = sb.ToString();
        Debug.Log("Discorved Recipe: " + discoveredRecipes);

        playerInventory = "";

    }

    public void Load()
    {
        Debug.Log(discoveredRecipes);

        for (int i = 0; i < discoveredRecipes.Length; i++)
        {
            GameManager.gm.recipeManager.hasDiscoveredRecipe[i] =
                discoveredRecipes[i].Equals('1');
        }
    }
}