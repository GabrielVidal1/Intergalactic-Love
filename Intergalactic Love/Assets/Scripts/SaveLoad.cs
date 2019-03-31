using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveLoad
{
    public static void SaveGame()
    {
        Save save = new Save();
        save.discoveredRecipes = JsonUtility.ToJson(GameManager.gm.recipeManager.hasDiscoveredRecipe);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.save");
        bf.Serialize(file, save);
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);

            file.Close();

            GameManager.gm.recipeManager.hasDiscoveredRecipe =
                JsonUtility.FromJson<bool[]>(save.discoveredRecipes);
        }
    }

}

[System.Serializable]
public class Save
{
    public string discoveredRecipes;
}