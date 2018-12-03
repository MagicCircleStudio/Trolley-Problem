using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class Network : MonoBehaviour {

	/// <summary>
	/// Query the statistic page.
	/// <para/>
	/// Usage:
	/// <para/>
	/// queryPage("finish", kills); 第几个完成游戏的人|杀人数排名
	/// <para/>
	/// queryPage("update_level", level, choice); 该关卡选择第一个选项的百分比
	/// </summary>
	/// <param name="func">Function to do</param>
	/// <param name="var1">Param 1</param>
	/// <param name="var2">Param 2</param>
	/// <returns></returns>
	public static string queryPage(string func, string var1, string var2 = "") {
		try {
			WebClient MyWebClient = new WebClient();
			MyWebClient.Credentials = CredentialCache.DefaultCredentials;
			Stream pageData = MyWebClient.OpenRead("http://144.202.107.141/trolley-problem.php?func=" + func + "&var1=" + var1 + "&var2=" + var2);
			StreamReader sr = new StreamReader(pageData);
			return sr.ReadToEnd();
		} catch (Exception) {
			return "error";
		}
	}

	private void Start() {
		// Debug.Log(queryPage("finish", "3"));
	}

}