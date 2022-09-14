using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class GeoCodeReverse
{
    [SerializeField]
    private string StartAddress;
    [SerializeField]
    private string EndAddress;

    public IEnumerator googleReverseGeoCoding(string LgLat, bool first)
    {
        Debug.Log(LgLat);
        using (UnityWebRequest www = UnityWebRequest.Get("https://maps.googleapis.com/maps/api/geocode/json?" + LgLat + "&key=AIzaSyCzqEhbt3hAHW0dHv7gzAs6XD8bq_pIlXE"))
        {

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {


                ReverseDecode reversedCode = JsonConvert.DeserializeObject<ReverseDecode>(www.downloadHandler.text);

                if (first)
                {
                    StartAddress = String.Format("{0}, {1}", reversedCode.results[0].address_components[1].long_name, reversedCode.results[0].address_components[2].long_name);
                }
                if (!first)
                {
                    EndAddress = String.Format("{0}, {1}", reversedCode.results[0].address_components[1].long_name, reversedCode.results[0].address_components[2].long_name);
                }



            }
        }
        yield return true;
    }
}
