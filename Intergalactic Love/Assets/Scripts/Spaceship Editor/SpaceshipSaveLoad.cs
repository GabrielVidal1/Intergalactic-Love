using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SpaceshipSaveLoad : MonoBehaviour
{
    public SpaceshipPart mainSpaceship;

    public SerializedSpaceshipPart savedVersion;

    [SerializeField] private Transform shipParent;
    [SerializeField] private SpaceshipPart cockpitPrefab;

    public void SaveSpaceship()
    {
        savedVersion = mainSpaceship.SerializePart();

        savedVersion.position = new SerializedSpaceshipPart.SerializedVector3(Vector3.zero);
        savedVersion.rotation = new SerializedSpaceshipPart.SerializedQuaternion(Quaternion.identity);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/spaceship.data");
        bf.Serialize(file, savedVersion);

        Debug.Log("Save at: " + Application.persistentDataPath + "/spaceship.data");

    }

    public void LoadSpaceship()
    {
        if (File.Exists(Application.persistentDataPath + "/spaceship.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/spaceship.data", FileMode.Open);
            savedVersion = (SerializedSpaceshipPart)bf.Deserialize(file);

            file.Close();

            mainSpaceship = savedVersion.LoadMain(shipParent);

            Debug.Log("Loading complete");
        }
        else
        {
            LoadFirstTime();
        }
    }

    public void LoadFirstTime()
    {
        mainSpaceship = Instantiate(cockpitPrefab, shipParent);
        mainSpaceship.Initialize();
        mainSpaceship.transform.localPosition = Vector3.zero;
    }

    [System.Serializable]
    public class SerializedSpaceshipPart
    {
        public SerializedSpaceshipPart[] parts;

        public SerializedVector3 position;

        [System.Serializable]
        public class SerializedVector3
        {
            public float x;
            public float y;
            public float z;

            public SerializedVector3(Vector3 vector)
            {
                x = vector.x;
                y = vector.y;
                z = vector.z;
            }

            public Vector3 Deserialize()
            { return new Vector3(x, y, z); }
        }

        public SerializedQuaternion rotation;

        [System.Serializable]
        public class SerializedQuaternion
        {
            public float x;
            public float y;
            public float z;
            public float w;

            public SerializedQuaternion(Quaternion quaternion)
            {
                x = quaternion.x;
                y = quaternion.y;
                z = quaternion.z;
                w = quaternion.w;
            }

            public Quaternion Deserialize()
            {
                return new Quaternion(x, y, z, w);
            }
        }

        public int partIndex;

        public SpaceshipPart LoadMain(Transform parent)
        {
            SpaceshipPart thisPart = Instantiate(GameManager.gm.itemManager.parts[partIndex]);

            thisPart.Initialize();
            thisPart.SetPosition(position.Deserialize(), true);
            thisPart.SetRotation(rotation.Deserialize());

            thisPart.transform.parent = parent;
            thisPart.transform.localPosition = Vector3.zero;

            foreach (SerializedSpaceshipPart serializedSpaceshipPart in parts)
            {
                SpaceshipPart childPart = serializedSpaceshipPart.Load();
                thisPart.AddPart(childPart);
            }

            return thisPart;
        }

        public SpaceshipPart Load()
        {
            SpaceshipPart thisPart = Instantiate(GameManager.gm.itemManager.parts[partIndex]);

            thisPart.Initialize();
            thisPart.SetPosition(position.Deserialize(), true);
            thisPart.SetRotation(rotation.Deserialize());

            foreach (SerializedSpaceshipPart serializedSpaceshipPart in parts)
            {
                SpaceshipPart childPart = serializedSpaceshipPart.Load();
                thisPart.AddPart(childPart);
            }

            return thisPart;
        }

    }
}

