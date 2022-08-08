using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetCountryPlayer : MonoBehaviour {

    public string nameVarCodeCountry = "CodeCountry";
    void Start()
    {
        string dataCountry = SaveLoadData.LoadData(nameVarCodeCountry, true);
        if (dataCountry == "" || dataCountry == "Null")
            SaveLoadData.SaveData(nameVarCodeCountry, ToCountryCode(Application.systemLanguage), true);
        if (Input.location.isEnabledByUser)
            StartCoroutine(getGeographicalCoordinatesCoroutine());
    }

    private IEnumerator getGeographicalCoordinatesCoroutine()
    {
        Input.location.Start();
        int maximumWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maximumWait > 0)
        {
            yield return new WaitForSeconds(1);
            maximumWait--;
        }
        if (maximumWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Input.location.Stop();
            yield break;
        }
        float latitude = Input.location.lastData.latitude;
        float longitude = Input.location.lastData.longitude;
        //Viet Nam
        //float latitude = 10.83333f;//vi do
        //float longitude = 106.63278f;//kinh do
        Input.location.Stop();
        string link = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=true";
        print(link);
        WWW www = new WWW(link);
        //while (!www.isDone)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //}
        yield return www;
        if (www.error != null) yield break;
        XmlDocument reverseGeocodeResult = new XmlDocument();
        reverseGeocodeResult.LoadXml(www.text);
        if (reverseGeocodeResult.GetElementsByTagName("status").Item(0).ChildNodes.Item(0).Value != "OK") yield break;
        string countryCode = null;
        bool countryFound = false;
        foreach (XmlNode eachAdressComponent in reverseGeocodeResult.GetElementsByTagName("result").Item(0).ChildNodes)
        {
            if (eachAdressComponent.Name == "address_component")
            {
                foreach (XmlNode eachAddressAttribute in eachAdressComponent.ChildNodes)
                {
                    if (eachAddressAttribute.Name == "short_name") countryCode = eachAddressAttribute.FirstChild.Value;
                    if (eachAddressAttribute.Name == "type" && eachAddressAttribute.FirstChild.Value == "country")
                        countryFound = true;
                }
                if (countryFound) break;
            }
        }
        if (countryFound && countryCode != null) SaveLoadData.SaveData(nameVarCodeCountry, countryCode, true);
    }

    private static readonly Dictionary<SystemLanguage, string> COUTRY_CODES = new Dictionary<SystemLanguage, string>()
        {
            { SystemLanguage.Afrikaans, "ZA" },
            { SystemLanguage.Arabic    , "SA" },
            { SystemLanguage.Basque    , "US" },
            { SystemLanguage.Belarusian    , "BY" },
            { SystemLanguage.Bulgarian    , "BJ" },
            { SystemLanguage.Catalan    , "ES" },
            { SystemLanguage.Chinese    , "CN" },
            { SystemLanguage.Czech    , "HK" },
            { SystemLanguage.Danish    , "DK" },
            { SystemLanguage.Dutch    , "BE" },
            { SystemLanguage.English    , "US" },
            { SystemLanguage.Estonian    , "EE" },
            { SystemLanguage.Faroese    , "FU" },
            { SystemLanguage.Finnish    , "FI" },
            { SystemLanguage.French    , "FR" },
            { SystemLanguage.German    , "DE" },
            { SystemLanguage.Greek    , "JR" },
            { SystemLanguage.Hebrew    , "IL" },
            { SystemLanguage.Icelandic    , "IS" },
            { SystemLanguage.Indonesian    , "ID" },
            { SystemLanguage.Italian    , "IT" },
            { SystemLanguage.Japanese    , "JP" },
            { SystemLanguage.Korean    , "KR" },
            { SystemLanguage.Latvian    , "LV" },
            { SystemLanguage.Lithuanian    , "LT" },
            { SystemLanguage.Norwegian    , "NO" },
            { SystemLanguage.Polish    , "PL" },
            { SystemLanguage.Portuguese    , "PT" },
            { SystemLanguage.Romanian    , "RO" },
            { SystemLanguage.Russian    , "RU" },
            { SystemLanguage.SerboCroatian    , "SP" },
            { SystemLanguage.Slovak    , "SK" },
            { SystemLanguage.Slovenian    , "SI" },
            { SystemLanguage.Spanish    , "ES" },
            { SystemLanguage.Swedish    , "SE" },
            { SystemLanguage.Thai    , "TH" },
            { SystemLanguage.Turkish    , "TR" },
            { SystemLanguage.Ukrainian    , "UA" },
            { SystemLanguage.Vietnamese    , "VN" },
            { SystemLanguage.ChineseSimplified    , "CN" },
            { SystemLanguage.ChineseTraditional    , "CN" },
            { SystemLanguage.Hungarian    , "HU" },
            { SystemLanguage.Unknown    , "Null" },
        };

    public static string ToCountryCode(SystemLanguage language)
    {
        string result;
        if (COUTRY_CODES.TryGetValue(language, out result))
        {
            return result;
        }
        else
        {
            return COUTRY_CODES[SystemLanguage.Unknown];
        }
    }
}
