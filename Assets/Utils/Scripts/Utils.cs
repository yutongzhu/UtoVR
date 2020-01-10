﻿using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using System.Collections;


/// <summary>
/// Utils
/// </summary>
public class Utils
{
	/// <summary>
	/// Base64编码
	/// </summary>
	/// <param name="codeType"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string Base64Encode( string codeType, string value )
	{
		string ret = string.Empty;
		Encoding encoding = Encoding.Default;
		try
		{
			if( !string.IsNullOrEmpty( codeType ) )
				encoding = Encoding.GetEncoding( codeType );
			ret = Convert.ToBase64String( encoding.GetBytes( value ) );
		}
		catch
		{
		}
		return ret;
	}



	/// <summary>
	/// 字符串如果操过指定长度则将超出的部分用指定字符串代替
	/// </summary>
	/// <param name="p_SrcString">要检查的字符串</param>
	/// <param name="p_Length">指定长度</param>
	/// <param name="p_TailString">用于替换的字符串</param>
	/// <returns>截取后的字符串</returns>
	public static string GetSubString( string p_SrcString, int p_Length, string p_TailString )
	{
		return GetSubString( p_SrcString, 0, p_Length, p_TailString );
	}

	/// <summary>
	/// 取指定长度的字符串
	/// </summary>
	/// <param name="p_SrcString">要检查的字符串</param>
	/// <param name="p_StartIndex">起始位置</param>
	/// <param name="p_Length">指定长度</param>
	/// <param name="p_TailString">用于替换的字符串</param>
	/// <returns>截取后的字符串</returns>
	public static string GetSubString( string p_SrcString, int p_StartIndex, int p_Length, string p_TailString )
	{
		string myResult = p_SrcString;

		Byte[] bComments = Encoding.UTF8.GetBytes( p_SrcString );
		foreach( char c in Encoding.UTF8.GetChars( bComments ) )
		{    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
			if( ( c > '\u0800' && c < '\u4e00' ) || ( c > '\xAC00' && c < '\xD7A3' ) )
			{
				//if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
				//当截取的起始位置超出字段串长度时
				if( p_StartIndex >= p_SrcString.Length )
					return "";
				else
					return p_SrcString.Substring( p_StartIndex, p_Length + p_StartIndex > p_SrcString.Length ? p_SrcString.Length - p_StartIndex : p_Length ) + ( p_Length + p_StartIndex > p_SrcString.Length ? string.Empty : p_TailString );
			}
		}

		if( p_Length >= 0 )
		{
			byte[] bsSrcString = Encoding.Default.GetBytes( p_SrcString );

			//当字符串长度大于起始位置
			if( bsSrcString.Length > p_StartIndex )
			{
				int p_EndIndex = bsSrcString.Length;

				//当要截取的长度在字符串的有效长度范围内
				if( bsSrcString.Length > ( p_StartIndex + p_Length ) )
				{
					p_EndIndex = p_Length + p_StartIndex;
				} else
				{   //当不在有效范围内时,只取到字符串的结尾

					p_Length = bsSrcString.Length - p_StartIndex;
					p_TailString = "";
				}

				int nRealLength = p_Length;
				int[] anResultFlag = new int[ p_Length ];
				byte[] bsResult = null;

				int nFlag = 0;
				for( int i = p_StartIndex; i < p_EndIndex; i++ )
				{
					if( bsSrcString[ i ] > 127 )
					{
						nFlag++;
						if( nFlag == 3 )
							nFlag = 1;
					} else
						nFlag = 0;

					anResultFlag[ i ] = nFlag;
				}

				if( ( bsSrcString[ p_EndIndex - 1 ] > 127 ) && ( anResultFlag[ p_Length - 1 ] == 1 ) )
					nRealLength = p_Length + 1;

				bsResult = new byte[ nRealLength ];

				Array.Copy( bsSrcString, p_StartIndex, bsResult, 0, nRealLength );

				myResult = Encoding.Default.GetString( bsResult );
				myResult = myResult + p_TailString;
			}
		}

		return myResult;
	}


	// 获取安卓电量和时间
	string _time = string.Empty;
	string _battery = string.Empty;

	void Start()
	{
		//		StartCoroutine(UpdataTime());
		//		StartCoroutine("UpdataBattery");
		//		StartCoroutine (DisplayWarning (_time));
	}

	IEnumerator UpdataTime()
	{
		DateTime now = DateTime.Now;
		_time = string.Format("{0}:{1}", now.Hour, now.Minute);
		yield return new WaitForSeconds(60f - now.Second);
		while (true)
		{
			now = DateTime.Now;
			_time = string.Format("{0}:{1}", now.Hour, now.Minute);
			yield return new WaitForSeconds(60f);
		}
	}

	IEnumerator UpdataBattery()
	{
		while (true)
		{
			_battery = GetBatteryLevel().ToString();
			yield return new WaitForSeconds(300f);
		}
	}

	int GetBatteryLevel()
	{
		try
		{
			string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
			return int.Parse(CapacityString);
		}
		catch (Exception e)
		{
			Debug.Log("Failed to read battery power; " + e.Message);
		}
		return -1;
	}


}