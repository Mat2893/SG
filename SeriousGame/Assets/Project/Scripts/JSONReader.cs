using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JSONReader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoadJson();
	}

    // Classe représentant le modèle de chaque texte d'information
    public class JSONText {
        public string ID;
        public string content;
    }

    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("infos.json"))
        {
            string json = r.ReadToEnd();

            // Lit l'ensemble du fichier JSON et stocke des données dans une liste
            List<JSONText> infos = JsonConvert.DeserializeObject<List<JSONText>>(json);

            // Sauvegarde chaque texte dans une variable des PlayerPrefs
            if (infos != null)
            {
                foreach (var jsontext in infos)
                {
                    PlayerPrefs.SetString(jsontext.ID, jsontext.content);
                    Debug.Log(PlayerPrefs.GetString(jsontext.ID, "erreur"));
                }
            }
        }
    }
}
