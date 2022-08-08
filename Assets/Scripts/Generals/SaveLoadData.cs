using UnityEngine;
using System.Collections;

public static class SaveLoadData {

	public static string khoaBiMat = "3ecode";
    private static string[] banKyTu = new string[]
	{"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c",
	 "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
	 "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C",
	 "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
	 "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", ";", "-", ".",
     ",", "/"};

	private static int[] ViTriPhanTuKhoa(string chuoi, string khoa)
	{
		int slkytu = banKyTu.Length;
		string khoamoi = khoa;
		int ddkhoa = khoa.Length;
		int ddchuoi = chuoi.Length;
		if (ddkhoa > ddchuoi)
		{
			khoamoi = khoa.Substring(0, ddchuoi);
		}
		else if (ddkhoa < ddchuoi)
		{
			int solanlap = (int)(ddchuoi / ddkhoa) - 1;
			int sokytudu = ddchuoi % ddkhoa;
			for (int i = 0; i < solanlap; i++)
			{
				khoamoi += khoa;
			}
			if (sokytudu > 0)
			{
				khoamoi += khoa.Substring(0, sokytudu);
			}
		}
		int[] mvtktkhoa = new int[ddchuoi];
		int d = 0;
		for (int i = 0; i < ddchuoi; i++)
		{
			for (int j = 0; j < slkytu; j++)
			{
				if (khoamoi.Substring(i, 1) == banKyTu[j])
				{
					mvtktkhoa[d] = j;
					d++;
					break;
				}
			}
		}
		return mvtktkhoa;
	}
	private static string MaHoaVigenere(string banro, string khoa)
	{
		string chuoimahoa = "";
		int slkytu = banKyTu.Length;
		int[] vtkhoa = ViTriPhanTuKhoa(banro, khoa);
		//ma hoa theo khoa moi vua tim
		for (int i = 0; i < banro.Length; i++)
		{
			for (int j = 0; j < slkytu; j++)
			{
				if (banro.Substring(i, 1) == banKyTu[j])
				{
					int vtktmahoa = (j + vtkhoa[i]) % slkytu;
					chuoimahoa += banKyTu[vtktmahoa];
					break;
				}
			}
		}
		return chuoimahoa;
	}
    private static string GiaiMaVigenere(string banma, string khoa)
    {
        string chuoigiaima = "";
        int slkytu = banKyTu.Length;
        int[] vtkhoa = ViTriPhanTuKhoa(banma, khoa);
        //ma hoa theo khoa moi vua tim
        for (int i = 0; i < banma.Length; i++)
        {
            for (int j = 0; j < slkytu; j++)
            {
                if (banma.Substring(i, 1) == banKyTu[j])
                {
                    int vtktbanro = j - vtkhoa[i];
                    if (vtktbanro < 0) vtktbanro += slkytu;
                    chuoigiaima += banKyTu[vtktbanro];
                    break;
                }
            }
        }
        return chuoigiaima;
    }

    public static void SaveData(string varName, string data, bool MaHoa)
    {
        PlayerPrefs.SetString(varName, data);
        if (MaHoa) PlayerPrefs.SetString(varName + "mh", MaHoaVigenere(data, khoaBiMat));
        PlayerPrefs.Save();
    }

    public static string LoadData(string varName, bool MaHoa)
    {
        string result = "";
        string data = PlayerPrefs.GetString(varName, "");
        if (MaHoa)
        {
            string dataMh = PlayerPrefs.GetString(varName + "mh", "");
            if (data != "" && dataMh != "")
            {
                if (data == GiaiMaVigenere(dataMh, khoaBiMat))
                    result = data;
            }
        }
        else result = data;
        return result;
    }

	public static void DeleteVar(string varName)
	{
		PlayerPrefs.DeleteKey (varName);
		PlayerPrefs.DeleteKey (varName + "mh");
        PlayerPrefs.Save();
    }

	public static void DeleteAllVar(string varName)
	{
		PlayerPrefs.DeleteAll ();
        PlayerPrefs.Save();
    }
}