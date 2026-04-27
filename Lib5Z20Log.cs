using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using static Lib5Z20.Lib5Z20Enums;
//
namespace Lib5Z20
{
	public class Lib5Z20Log
	{
		public delegate void delegateLogMsg(string _strText);
		#region Variablen
		public  delegateLogMsg EventLog;
		#endregion
		//
		#region Create
		#endregion
		//
		#region Write
		public void Write(string _sInfo)
		{
			if (EventLog != null)
			{
				EventLog(DateTime.Now.ToString("HH:mm:ss:fff") + ": " + _sInfo);
			}
		}
		public void Write(string _sInfo, int _iMsgCount, typeZ20Msg _tMsg)
		{
			if (EventLog == null)
			{
				return;
			}
			//----	Msg Disabled??
			if ((_tMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp0ANet) &&
				(_tMsg.eCmd == Lib5Z20Enums.eNetCmd.Net_Ping))
			{
//				return;
			}
			//
			string sData = DateTime.Now.ToString("HH:mm:ss:fff") + ": ";
			//----	Sondermarkierung für System Error Messages
			if ((_tMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp00Sys) &&
				(_tMsg.eCmd == Lib5Z20Enums.eNetCmd.SysError))
			{
				sData = Environment.NewLine +
						"-------------------------" + Environment.NewLine +
						"ERROR!!!" + Environment.NewLine;
			}
			int iTxCnt = 0;
			byte[] bTemp = new byte[_tMsg.iSize + 16];
			for (int iCnt = 0; iCnt < _tMsg.iSize; iCnt++)
			{
				bTemp[iTxCnt++] = _tMsg.u08Data[iCnt];
			}
			//
			UInt32 u32Grp = (UInt32)_tMsg.eGrp;
			UInt32 u32Cmd = (UInt32)_tMsg.eCmd;
			//
			sData += string.Format(_sInfo + "[{0,5}]>", _iMsgCount);
			//----	Debug Text
			if (_tMsg.eGrp == Lib5Z20Enums.eNetGrp.GrpDebug)
			{
				sData += "Debug:\t";
				sData += string.Format("Tick={0,6}, ", _tMsg.DataU32Get(0));		//	Word '0' Systick
				sData += "UId=0x" + _tMsg.DataU32Get(4).ToString("X8") + ", ";		//	Word '1' Sender UId
				sData += "Nr.=0x" + _tMsg.DataU32Get(8).ToString("X8") + ": ";		//	Word '2' Text
				System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
				sData += enc.GetString(bTemp, 12, _tMsg.iSize);
				EventLog(sData);
				return;
			}
			//
			sData += "Size=" + _tMsg.iSize.ToString("D3") + ", ";
			//
			sData += "[" + u32Grp.ToString("X2") + "." + u32Cmd.ToString("X2") + ".";
			switch(_tMsg.eMode)
			{
				case eNetMode.Req:
				{
					sData += "R";
				}
				break;
				case eNetMode.Cmd:
				{
					sData += "C";
				}
				break;
				case eNetMode.Evt:
				{
					sData += "E";
				}
				break;
				case eNetMode.Ack:
				{
					sData += "A";
				}
				break;
			}
			sData += "." + _tMsg.u16HdrNId.ToString("X4") + "] " +
					 Lib5Z20Enums.GetGrpName(_tMsg.eGrp) + "." + Lib5Z20Enums.GetCmdName(_tMsg.eGrp, _tMsg.eCmd) + ": ";
			//
			if ((_tMsg.eGrp == eNetGrp.Grp00Sys) && (_tMsg.eCmd == eNetCmd.SysState))
			{
				sData += "\tP=" + bTemp[2].ToString("X2") + ", M=" + bTemp[3].ToString("X2");
			}
			//
			EventLog(sData);
			//
			sData = "\t\t\t" + bTemp[0].ToString("X2");
			for (int iCnt = 1; iCnt < _tMsg.iSize; iCnt++)
			{
				sData += ", " + bTemp[iCnt].ToString("X2");
			}
			EventLog(sData);
		}
		#endregion
	}
}
