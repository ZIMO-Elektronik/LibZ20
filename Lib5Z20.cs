using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
//
using Lib5XML;
using static Lib5Z20.Lib5Z20Enums;
//
namespace Lib5Z20
{
	public static class LibNetZ20
	{
		#region Delegates
		public delegate void delegateMode(bool _bOnLine);
		public delegate void delegateRxMsg(typeZ20Msg _tMsg);
		//
		public delegate void delegateRxNetPingAck(typeZ20MsgPing _tPingAck);
		public delegate void delegateRxNetOpenAck(typeZ20MsgOpen _tOpenAck);
		public delegate void delegateRxDbgTSE(typeZ20MsgDebugTSE _tDebugTSE);
		#endregion
		#region Enum's
		public enum enumMode : short
		{
			None = 0,
			UDP = 1,
			COM = 2,
		}
		//
		public enum enumLog : short
		{
			None = 0,
			File = 1,
		}
		#endregion
		#region Properties
		public static bool bIsOpen { get; private set; }
		public static UInt16 u16MyNId { get; private set; }
		public static UInt16 u16ZsNId { get; private set; }
		public static enumMode eMode { get; private set; }
		public static IPAddress ipAddr { get; private set; }
		public static IPAddress MyIpAddr { get; private set; }
		public static int iPortRx { get; private set; }
		public static int iPortTx { get; private set; }
		public static bool bAutoConnect { get; private set; }
		public static bool bAutoLoadLocoList { get; private set; }
		public static bool bAutoLoadLocoData { get; private set; }
		public static bool bAutoLoadLocoGUI { get; private set; }
		public static bool bAutoLoadAccessoryList { get; private set; }
		public static bool bAutoLoadModulData { get; private set; }
		//
		public static delegateMode EventConnectChanged;
		//
		public static delegateRxMsg EventRx;
		//
		public static delegateRxNetPingAck EventRxNetPingAck;
		public static delegateRxNetOpenAck EventRxNetOpenAck;
		public static delegateRxDbgTSE EventRxDebugTSE;
		//
		#endregion
		#region Properties, Log
		public static enumMode eLog { get; private set; }
		public static Lib5Z20Log objLog = new Lib5Z20Log();
		public static List<typeZ20Msg> lstMsg = new List<typeZ20Msg>();
		public static int iLogPush = 0;
		public static int iLogPull = 0;
		public static int iLogMax = 1000;
		#endregion
		#region Properties, Statistic
		public static int iCntRxMsg { get; private set; }
		public static int iCntRxByte { get; private set; }
		public static int iCntTxMsg { get; private set; }
		public static int iCntTxByte { get; private set; }
		#endregion
		#region Variables
		//
		private static UdpClient udpZs100Rx;
		private static typeZ20Msg tLastRxMsg = new typeZ20Msg();
		private static byte[] u08RxByte;
		//
		private static DateTime dtLastTx;
		private static IPEndPoint epZs100Tx;
		private static UdpClient udpZs100Tx;
		private static byte[] u08TxByte;
		//
		private static Thread objThreadTick;
		//
		public static Lib5Z20Util objUtil;              //	Hilfsfunktionen
		#region Sub Classes Tx
		public static Lib5Z20Grp00TxSys		NetTxSys;		//	System Command
		public static Lib5Z20Grp01TxAcc		objAccTx;		//	Grp 0x01
		public static Lib5Z20Grp02TxLoco	objLocoTx;		//	Grp 0x02
		public static Lib5Z20Grp06TxTrack	objTrackTx;		//	Grp 0x06: Track Signal Engine
		public static Lib5Z20Grp07TxData	objDataTx;
		public static Lib5Z20Grp08TxInfo	objInfoTx;
		public static Lib5Z20Grp0ATxNet		objNetTx;
		public static Lib5Z20Grp1eTxFile	objFileTx;
		public static Lib5Z20Grp99TxDebug	NetTxDbg;
		#endregion
		//
		#region Sub Classes Rx
		public static Lib5Z20Grp00RxSys		objSysRx;
		public static Lib5Z20Grp01RxAcc		objAccRx;
		public static Lib5Z20Grp02RxLoco	objLocoRx;
		public static Lib5Z20Grp06RxTrack	objTrackRx;
		public static Lib5Z20Grp07RxData	objDataRx;
		public static Lib5Z20Grp08RxInfo	objInfoRx;
		#endregion
		//
		private static Lib5Z20Enums.eNetGrp eWaitGrp;
		private static Lib5Z20Enums.eNetCmd eWaitCmd;
		private static Boolean bWaitMsg;
		private static Boolean bWaitDone;
		#endregion
		#region Create
		static LibNetZ20()
		{
			u08RxByte = new byte[4096];
			u08TxByte = new byte[4096];
			eMode = enumMode.None;
			u16MyNId = 0xC200;
			GetMyIp();                      //	MyIp 0xC200 + Last Ip Byte
			byte[] u08Zs100Ip = new byte[4];
			u08Zs100Ip[0] = 192;
			u08Zs100Ip[1] = 168;
			u08Zs100Ip[2] = 1;
			u08Zs100Ip[3] = 145;
			ipAddr = new IPAddress(u08Zs100Ip);
			iPortRx = 14521;
			iPortTx = 14520;
			//
			objUtil = new Lib5Z20Util();
			//
			NetTxSys = new Lib5Z20Grp00TxSys();         //	Sender	 Grp 0x00, System
			objSysRx = new Lib5Z20Grp00RxSys();			//	Receiver Grp 0x00, System
			//
			objAccTx = new Lib5Z20Grp01TxAcc();            //	Sender	 Grp 0x01, Accessory
			objAccRx = new Lib5Z20Grp01RxAcc();         //	Receiver Grp 0x01, Accessory
			//
			objLocoTx = new Lib5Z20Grp02TxLoco();       //	Sender	 Grp 0x02, Loco Control
			objLocoRx = new Lib5Z20Grp02RxLoco();       //	Receiver Grp 0x02, Loco Control
			//
			objTrackTx = new Lib5Z20Grp06TxTrack();     //	Sender	 Grp 0x06, Track Signal Engine
			objTrackRx = new Lib5Z20Grp06RxTrack();     //	Receiver Grp 0x06, Track Signal Engine
			//
			objDataTx = new Lib5Z20Grp07TxData();       //	Sender	 Grp 0x07, Data
			objDataRx = new Lib5Z20Grp07RxData();       //	Receiver Grp 0x07, Data
			//
			objInfoTx = new Lib5Z20Grp08TxInfo();
			objInfoRx = new Lib5Z20Grp08RxInfo();
			//
			objNetTx = new Lib5Z20Grp0ATxNet();
			//
			objFileTx = new Lib5Z20Grp1eTxFile();
			//
			NetTxDbg = new Lib5Z20Grp99TxDebug();
			EventRx = MsgRx;
		}
		#endregion
		//
		#region Config Load
		public static void ConfigLoad(XmlNode _ndRoot)
		{
			string strMode;
			string strIP;
			int iTemp;
			IPAddress ipTest;
			XmlNode ndConnect = _ndRoot.SelectSingleNode("Connect");
			LibXML.AttributeGetString(ndConnect, "Mode", out strMode);
			LibXML.AttributeGetString(ndConnect, "IP", out strIP);
			if (!IPAddress.TryParse(strIP, out ipTest))
			{
				ipAddr = IPAddress.None;
			}
			ipAddr = ipTest;
			LibXML.AttributeGetInt(ndConnect, "PortRx", out iTemp);
			iPortRx = iTemp;
			LibXML.AttributeGetInt(ndConnect, "PortTx", out iTemp);
			iPortTx = iTemp;
			//
			bool bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoConnect", out bTemp);
			bAutoConnect = bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoLocoList", out bTemp);
			bAutoLoadLocoList = bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoLocoData", out bTemp);
			bAutoLoadLocoData = bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoLocoGUI", out bTemp);
			bAutoLoadLocoGUI = bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoAccessoryList", out bTemp);
			bAutoLoadAccessoryList = bTemp;
			LibXML.AttributeGetBoolean(ndConnect, "AutoModulData", out bTemp);
			bAutoLoadModulData = bTemp;
			LibXML.AttributeGetInt(ndConnect, "LogMax", out iTemp);
			iLogMax = iTemp;
			//
			ConnectTest();
		}
		#endregion
		#region Config Set
		public static void ConfigSet(IPAddress _ipAddr, int _iPortRx, int _iPortTx)
		{
			ipAddr = _ipAddr;
			iPortRx = _iPortRx;
			iPortTx = _iPortTx;
		}
		public static void ConfigSet(IPAddress _ipAddr, int _iPortRx, int _iPortTx, 
									 bool _bConnect, bool _bLoadLocoList, bool _bLoadLocoGUI,
									 bool _bAccessoryList, bool _bModulData)
		{
			ConfigSet(_ipAddr, _iPortRx, _iPortTx);
			bAutoConnect = _bConnect;
			bAutoLoadLocoList = _bLoadLocoList;
			bAutoLoadLocoGUI = _bLoadLocoGUI;
			bAutoLoadAccessoryList = _bAccessoryList;
			bAutoLoadModulData = _bModulData;
		}
		#endregion
		#region Config Save
		public static void ConfigSave(XmlDocument _xDoc, XmlNode _ndRoot)
		{
			_ndRoot.AppendChild(_xDoc.CreateComment("System Connection"));
			XmlNode ndConnect = _xDoc.CreateElement("Connect");
			LibXML.AttributeSetString(_xDoc, ndConnect, "Mode", "Client");
			if(ipAddr != null)
			{
				LibXML.AttributeSetString(_xDoc, ndConnect, "IP", ipAddr.ToString());
			}
			LibXML.AttributeSetInt(_xDoc, ndConnect, "PortRx", iPortRx);
			LibXML.AttributeSetInt(_xDoc, ndConnect, "PortTx", iPortTx);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoConnect", bAutoConnect);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoLocoList", bAutoLoadLocoList);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoLocoData", bAutoLoadLocoData);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoLocoGUI", bAutoLoadLocoGUI);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoAccessoryList", bAutoLoadAccessoryList);
			LibXML.AttributeSetBoolean(_xDoc, ndConnect, "AutoModulData", bAutoLoadModulData);
			LibXML.AttributeSetInt(_xDoc, ndConnect, "LogMax", iLogMax);
			_ndRoot.AppendChild(ndConnect);
		}
		#endregion
		#region Connect Test
		public static bool ConnectTest()
		{
			bool bPingOk = false;
			Ping pinger = null;
			//
			if ((ipAddr == null) || (ipAddr == IPAddress.None))
			{
				return (bPingOk);
			}
			try
			{
				pinger = new Ping();
				PingReply reply = pinger.Send(ipAddr);
				bPingOk = reply.Status == IPStatus.Success;
			}
			catch (PingException)
			{
				// Discard PingExceptions and return false;
			}
			finally
			{
				if (pinger != null)
				{
					pinger.Dispose();
				}
			}
			return (bPingOk);
		}
		#endregion
		#region Connect Open
		public static bool ConnectOpen()
		{
			//	Clear Counters
			iCntRxMsg = 0;
			iCntRxByte = 0;
			iCntTxMsg = 0;
			iCntTxByte = 0;
			//
			objLog.Write("Connecting: " + ipAddr.ToString() + " TX=" + iPortTx + ", RX=" + iPortRx);
			//
			//	Already Open
			if (bIsOpen == true)
			{
				objLog.Write("Allready Open");
				return (true);
			}
			bIsOpen = false;
			if ((iPortRx == 0) || (iPortTx == 0))
			{
				objLog.Write("PortRx=" + iPortRx + ", PortTx=" + iPortTx);
				return (bIsOpen);
			}
			if(!ConnectTest())
			{
				objLog.Write("Ping Test Failed!");
				return (bIsOpen);
			}
			//
			lstMsg.Clear();
			lstMsg.Capacity = iLogMax;
			for(int i = 0; i < iLogMax; i++)
			{
				lstMsg.Add(new typeZ20Msg());
			}
			//
			//	Start Receiver
			try
			{
				udpZs100Rx = new UdpClient(iPortRx);
			}
			catch (Exception e)
			{
				string strError = "UDP Rx Open Error: " + e.ToString();
				objLog.Write(strError);
				Debug.WriteLine(strError);
				return (false);
			}
			udpZs100Rx.DontFragment = true;
			try
			{
				udpZs100Rx.BeginReceive(new AsyncCallback(AsyncRx), null);
			}
			catch (Exception e)
			{
				string strError = "UDP Rx Open Error: " + e.ToString();
				objLog.Write(strError);
				Debug.WriteLine(strError);
				return (false);
			}
			//
			//	Start Transmitter
			if(udpZs100Tx == null)
			{
				udpZs100Tx = new UdpClient(iPortTx);
			}
			try
			{
				epZs100Tx = new IPEndPoint(ipAddr, iPortTx);
				udpZs100Tx.Connect(epZs100Tx);
				bIsOpen = true;
			}
			catch (Exception e)
			{
				string strError = "UDP Tx Open Error: " + e.ToString();
				objLog.Write(strError);
				Debug.WriteLine(strError);
			}
			//
			eMode = enumMode.UDP;
			//UInt32 u32Options = (UInt32)(Lib5Z20Enums.eNetOption.LongMsg | Lib5Z20Enums.eNetOption.MultiList | Lib5Z20Enums.eNetOption.DebugOutput);
			UInt32 u32Options = UInt32.MaxValue;
			objNetTx.PortOpen(false, u32Options, 0x91534102, "ZSA 2.0.0");
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			while (sw.ElapsedMilliseconds < 500)
			{
				Thread.Yield();
				if ((tLastRxMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp0ANet) ||
					(tLastRxMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp1ANet))
				{
					break;
				}
			}
			if ((tLastRxMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp0ANet) ||
				(tLastRxMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp1ANet))
			{
				objThreadTick = new Thread(TickThread);
				objThreadTick.Priority = ThreadPriority.Lowest;
				objThreadTick.Start();
				bIsOpen = true;
				if (EventConnectChanged != null)
				{
					EventConnectChanged(bIsOpen);
				}
			}
			//
			return (bIsOpen);
		}
		#endregion
		#region Connect Close
		public static bool ConnectClose()
		{
			if(!bIsOpen)
			{
				return (true);
			}
			bIsOpen = false;
			try
			{
				objLog.Write("Lib20 Connect Close");
				udpZs100Rx.Client.Shutdown(SocketShutdown.Both);
				udpZs100Rx.Client.Close();
			}
			catch { }
			if (EventConnectChanged != null)
			{
				EventConnectChanged(bIsOpen);
			}
			return (true);
		}
		#endregion
		//
		#region Message Tx
		public static void MsgTx(typeZ20Msg _tMsg)
		{
			dtLastTx = DateTime.Now;
			if(_tMsg.u16HdrNId == 0)
			{
				_tMsg.u16HdrNId = u16MyNId;
			}
			//
			switch (eMode)
			{
				case enumMode.UDP:
				{
					iCntTxMsg++;
					MsgTxUDP(_tMsg);
				}
				break;
				case enumMode.COM:
				{
				}
				break;
			}
			objLog.Write("Tx", iCntTxMsg, _tMsg);
		}
		#endregion
		#region Message Tx with Wait ACK
		public static Boolean MsgTx(Boolean _bWait, typeZ20Msg _tMsg)
		{
			MsgTx(_tMsg);
			Thread.Yield();
			if (!_bWait)
			{
				return (true);
			}
			eWaitGrp = _tMsg.eGrp;
			eWaitCmd = _tMsg.eCmd;
			bWaitMsg = _bWait;
			bWaitDone = false;
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			while (sw.ElapsedMilliseconds < 500)
			{
				Thread.Yield();
				if (bWaitDone)
				{
					eWaitGrp = eNetGrp.GrpNONE;
					eWaitCmd = eNetCmd.NONE;
					return (true);
				}
			}
			Debug.WriteLine(DateTime.Now.ToString("ss:fff") + 
						    " Rx Wait Timeout: " + sw.ElapsedMilliseconds.ToString() + 
							", G=" + ((UInt16)eWaitGrp).ToString("X2") + " C=" + ((UInt16)eWaitCmd).ToString("X2"));
			eWaitGrp = eNetGrp.GrpNONE;
			eWaitCmd = eNetCmd.NONE;
			return (false);
		}
		#endregion
		//
		#region Message Tx UDP
		private static void MsgTxUDP(typeZ20Msg _tMsg)
		{
			if ((bIsOpen == false) || (_tMsg.eGrp == Lib5Z20Enums.eNetGrp.GrpNONE))
			{
				return;
			}
			int iTxCnt = 0;
			//
			u08TxByte[iTxCnt++] = (byte)((_tMsg.iSize >> 0) & 0xFF);
			u08TxByte[iTxCnt++] = (byte)((_tMsg.iSize >> 8) & 0xFF);
			//
			u08TxByte[iTxCnt++] = 0;  //	unused
			u08TxByte[iTxCnt++] = 0;  //	unused
									  //
			u08TxByte[iTxCnt++] = (byte)_tMsg.eGrp;
			if (_tMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp0F)
			{
				u08TxByte[iTxCnt++] = (byte)_tMsg.iDataIdx;
			}
			else
			{
				byte iCmdMode = (byte)(_tMsg.eCmd);
				iCmdMode <<= 2;
				iCmdMode |= (byte)(_tMsg.eMode);
				u08TxByte[iTxCnt++] = iCmdMode;
				u08TxByte[iTxCnt++] = (byte)((u16MyNId >> 0) & 0xFF);
				u08TxByte[iTxCnt++] = (byte)((u16MyNId >> 8) & 0xFF);
			}
			//
			for (int iCnt = 0; iCnt < _tMsg.iSize; iCnt++)
			{
				u08TxByte[iTxCnt++] = _tMsg.u08Data[iCnt];
			}
			//
			iCntTxByte += iTxCnt;
			iCntTxMsg++;
			udpZs100Tx.Send(u08TxByte, _tMsg.iSize + 8);
			Thread.Yield();
		}
		#endregion
		//
		#region Receiver, Async
		/*----------------------------------------------------------------------------------*/
		/*																					*/
		/*			UDP Async Message Receiver												*/
		/*	-	Takes one Message from Local (Windows) UDP Stack							*/
		/*	-	Checks Basic Info if valid													*/
		/*	-	Converts UDP Bytes to Message Type											*/
		/*	-	Saves it to Log Ring-Buffer													*/
		/*	-	Filters Debug Info and writes them to Log									*/
		/*	-	Takes Master Central Unit NId from Ping/Power Message						*/
		/*	-	Checks if someone is Waiting for that Meassage und sets Flag				*/
		/*	-	Detect Datagramm Group and Delegate Message Resolving						*/
		/*	-	Restart Receiver															*/
		/*																					*/
		/*----------------------------------------------------------------------------------*/
		private static void AsyncRx(IAsyncResult aResult)
		{
			bWaitDone = false;
			int iRxLen = 0;
			//	Get Raw UDP Data
			try
			{
				IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, iPortRx);
				u08RxByte = udpZs100Rx.EndReceive(aResult, ref RemoteIpEndPoint);
				iRxLen = u08RxByte.Length;
			}
			catch (Exception e)
			{
				string strError = "RxAsync: " + e.ToString();
				objLog.Write(strError);
				return;
			}
			//
			iCntRxByte += iRxLen;
			//
			if (iRxLen < 8)
			{
				return;				//	Minimum Datagramm Length
			}
			//
			//----	Convert UDP Bytes to logical Z20 Message
			typeZ20Msg tMsg = new typeZ20Msg();
			tMsg.dtTimeStamp = DateTime.Now;       //	Message Time Stamp
			tMsg.iSize = u08RxByte[0];
			tMsg.iSize |= (u08RxByte[1] << 8);
			tMsg.eGrp = (Lib5Z20Enums.eNetGrp)u08RxByte[4];
			tMsg.eCmd = (Lib5Z20Enums.eNetCmd)(u08RxByte[5] >> 2);
			tMsg.eMode = (Lib5Z20Enums.eNetMode)(u08RxByte[5] & 0x03);
			tMsg.u16HdrNId = u08RxByte[6];
			tMsg.u16HdrNId |= (UInt16)(u08RxByte[7] << 8);
			for (int iCnt = 8; iCnt < iRxLen; iCnt++)
			{
				tMsg.u08Data[iCnt - 8] = u08RxByte[iCnt];
			}
			//----	Restart Receiver
			if(tMsg.Equals(tLastRxMsg))
			{
				if (bIsOpen)
				{
					udpZs100Rx.BeginReceive(new AsyncCallback(AsyncRx), null); // <-- this will be our loop
				}
				return;
			}
			//----	Save as now Last Message
			tLastRxMsg.Copy(tMsg);
			//----	Save to Log Ringbuffer
			if(iLogPush >= lstMsg.Count)
			{
				iLogPush = 0;
			}
			if(iLogPush < lstMsg.Count)
			{
				lstMsg[iLogPush++] = tMsg;
			}
			//
			iCntRxMsg++;
			//----	If Debug write to Log
			if(tMsg.eGrp == Lib5Z20Enums.eNetGrp.GrpDebug)
			{
				if (tMsg.eCmd == Lib5Z20Enums.eNetCmd.DebugText)
				{
					string sMsgRx;
					sMsgRx = string.Format("Dbg: Tick=<{0,8}>", tMsg.DataU32Get(0).ToString().PadLeft(8));
					sMsgRx += ", UId=" + tMsg.DataU32Get(4).ToString("X8");
					sMsgRx += "\t\t" + Encoding.Default.GetString(tMsg.u08Data, 12, tMsg.iSize);
					objLog.Write(sMsgRx);
				}
				if (tMsg.eCmd == Lib5Z20Enums.eNetCmd.DebugTSE_Tx)
				{
					if (EventRxDebugTSE != null)
					{
						EventRxDebugTSE(new typeZ20MsgDebugTSE(tMsg));
					}
				}
			}
			else
			{
				objLog.Write("Rx", iCntRxMsg, tMsg);
			}
			//----	Detect Central Unit NId
			if ((tMsg.eGrp == Lib5Z20Enums.eNetGrp.Grp18Info) &&
				(tMsg.eCmd == Lib5Z20Enums.eNetCmd.InfoPower))
			{
				u16ZsNId = tMsg.u16HdrNId;
			}
			//----	Restart Receiver
			if (bIsOpen)
			{
				udpZs100Rx.BeginReceive(new AsyncCallback(AsyncRx), null); // <-- this will be our loop
			}
			//----	Some one Waiting for spezific Message
			if (bWaitMsg)
			{
				if ((tMsg.eGrp == eWaitGrp) && (tMsg.eCmd == eWaitCmd))
				{
					bWaitDone = true;		//	Set Done (very brutal)
				}
			}
			//----	Analyse Z20 Group and Delete to aprobiate Receiver
			switch(tMsg.eGrp)
			{
				case Lib5Z20Enums.eNetGrp.Grp00Sys:
				{
					objSysRx.Lib5Z20RxGrp01(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp01Acc:
				{
					objAccRx.Lib5Z20RxGrp01(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp02Loco:
				case Lib5Z20Enums.eNetGrp.Grp12Loco:
				{
					objLocoRx.Lib5Z20RxGrp02(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp06TSE:
				case Lib5Z20Enums.eNetGrp.Grp16TSE:
				{
					objTrackRx.Lib5Z20RxGrp06(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp07Data:
				{
					objDataRx.Lib5Z20RxGrp07(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp17Data:
				{
					objDataRx.Lib5Z20RxGrp17(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp08Info:
				case Lib5Z20Enums.eNetGrp.Grp18Info:
				{
					objInfoRx.RxGrp08Info(tMsg);
				}
				break;
				case Lib5Z20Enums.eNetGrp.Grp0ANet:
				case Lib5Z20Enums.eNetGrp.Grp1ANet:
				{
					RxGrp0ANet(tMsg);
				}
				break;
			}
			//----	If available delegate to General Receiver (More or less Debuging)
			if (EventRx != null)
			{
				EventRx(tMsg);
			}
			Thread.Yield();		//	Thread Yield, so other can do Events
		}
		#endregion
		#region Rx Net
		public static void RxGrp0ANet(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.Net_Ping:
				{
					if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
						(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
					{
						if (EventRxNetPingAck != null)
						{
							typeZ20MsgPing tPingAck = new typeZ20MsgPing(_tMsg);
							EventRxNetPingAck(tPingAck);
						}
					}
				}
				break;
				case Lib5Z20Enums.eNetCmd.Net_Open:
				{
					if (_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack)
					{
						if (EventRxNetOpenAck != null)
						{
							typeZ20MsgOpen tOpenAck = new typeZ20MsgOpen(_tMsg);
							EventRxNetOpenAck(tOpenAck);
						}
					}
				}
				break;
			}
		}
		#endregion
		#region Rx State Object
		public class RxStateObject
		{
			// Client socket
			public UdpClient RxClient = null;
			public IPEndPoint RxEndPoint = null;
			public int RxPort = 0;
			public RxStateObject(UdpClient _Client, int _iPort)
			{
				RxClient = _Client;
				RxPort = _iPort;
			}
		}
		#endregion
		#region Message Rx
		public static void MsgRx(typeZ20Msg _tMsg)
		{
			if((_tMsg.eGrp == eWaitGrp) &&
				(_tMsg.eCmd == eWaitCmd) &&
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack) &&
				(bWaitMsg))
			{
				bWaitDone = true;
			}
		}
		#endregion
		//
		#region Ticker Thread, Keep Alive
		private static void TickThread()
		{
			while (bIsOpen)
			{
				objNetTx.PingEvtTx(false, 0);
				Thread.Sleep(10000);
				objLog.Write("Ping: " + DateTime.Now.ToLongTimeString());
			}
		}
		#endregion
		//
		//--------------------------------	Hilfsfunktionen
		#region Get My Ip
		private static void GetMyIp()
		{
			byte[] iMyIp = new byte[4];
			u16MyNId = 0xC200;
			IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
			//
			foreach (var ip in ipEntry.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					MyIpAddr = ip;
					int iCnt = 0;
					foreach (byte i in ip.GetAddressBytes())
					{
						iMyIp[iCnt++] = i;
						if (iCnt == 4)
						{
							u16MyNId |= iMyIp[3];
							break;
						}
					}
				}
			}
		}
		#endregion
		#region NIdGrpNameGet
		public static string NIdGrpNameGet(UInt16 _u16GrpNId)
		{
			string strGrpName = string.Empty;
			switch((eDataGrpNId)_u16GrpNId)
			{
				case eDataGrpNId.LocoDCC:
				{
					strGrpName = "Fahrzeuge";
				}
				break;
				case eDataGrpNId.ConsistMin:
				{
					strGrpName = "Traktionen";
				}
				break;
				case eDataGrpNId.LocoSysMin:
				{
					strGrpName = "System Fzg.";
				}
				break;
				case eDataGrpNId.AccDCCsMin:
				{
					strGrpName = "Zubehör DCC";
				}
				break;
				case eDataGrpNId.Zs100Min:
				{
					strGrpName = "MX10";
				}
				break;
				case eDataGrpNId.ZsBooster:
				{
					strGrpName = "Booster";
				}
				break;
				case eDataGrpNId.Zs300Min:
				{
					strGrpName = "MX32/MX33";
				}
				break;
				case eDataGrpNId.Zs900Min:
				{
					strGrpName = "StEin(e)";
				}
				break;
				case eDataGrpNId.Panel:
				{
					strGrpName = "Panel";
				}
				break;
				case eDataGrpNId.Tasks:
				{
					strGrpName = "Task";
				}
				break;
				case eDataGrpNId.SysMx01:
				{
					strGrpName = "MX 1";
				}
				break;
				case eDataGrpNId.SysMx08Min:
				{
					strGrpName = "MX 8";
				}
				break;
				case eDataGrpNId.SysMx09Min:
				{
					strGrpName = "MX 9";
				}
				break;
				case eDataGrpNId.SysMx31:
				{
					strGrpName = "MX 31";
				}
				break;
				//
				case eDataGrpNId.CfgDb:
				{
					strGrpName = "Config Db";
				}
				break;
				//
				case eDataGrpNId.Mx6Snd:
				{
					strGrpName = "Sound Projekte";
				}
				break;
				case eDataGrpNId.Mx6Upd:
				{
					strGrpName = "Decoder Upd";
				}
				break;
				//
				case eDataGrpNId.SysLbl:
				{
					strGrpName = "Sys Labels";
				}
				break;
			}
			return (strGrpName);
		}
		#endregion
	}
}

/*
//	0x00.0x00	State
//	0x00.0x02	Reset
//	0x00.0x04	Power Source
//

//
//------------------------------------------------------------	[0x02]:	Group Loco
//
//	0x02.0x00
//	0x02.0x01
//	0x02.0x02
//	0x02.0x03
//	0x02.0x04
//	0x02.0x05
//	0x02.0x08:	Fx Mode Info
//	0x02.0x09:	Fx Mode Data
//	0x02.0x0C:	Loco Vendor/UId
//	0x02.0x10
//	0x02.0x11
//	0x02.0x12
//	0x02.0x20
//
//------------------------------------------------------------	[0x05]:	Group Train
//
//	0x05.0x01
//	0x05.0x02
//	0x05.0x03
//	0x05.0x04
//	0x05.0x08
//	0x05.0x09
//	0x05.0x0F
//
//------------------------------------------------------------	[0x06]:	Group TSE
//
//	0x06.0x00:	Track Mode
//	0x06.0x02:	Prog Info
//	0x06.0x04:	Prog Clear
//	0x06.0x08:	Prog Read
//	0x06.0x09:	Prog Write
//	0x06.0x0D:	Prog Write 16
//	0x06.0x0E:	Background Reader
//
//	0x06.0x10:	Loco Find	!!! Redirect Req==> Cmd	!!!
//
//	0x06.0x11:	Loco BiDi Logon Ctrl
//	0x06.0x12:	Loco RCN218 Logon UId
//	0x06.0x13:	Loco RCN218 Assign
//	0x06.0x14:	Loco GUI Abfrage
//	0x06.0x1D:	BiDi Raw Data 1
//	0x06.0x1E:	BiDi Raw Data 2
//	0x06.0x1F:	BiDi Raw Data 3
//
//------------------------------------------------------------	Group Data [0x07]
//
//	0x07.0x00
//	0x07.0x01
//	0x07.0x02
//	0x07.0x03
//	0x07.0x04	V 1.x Delete
//	0x07.0x06
//	0x07.0x07
//	0x07.0x08
//	0x07.0x10
//	0x07.0x12
//	0x07.0x15
//	0x07.0x18
//	0x07.0x19
//	0x07.0x1A
//	0x07.0x1F
//	0x07.0x21
//
//------------------------------------------------------------	[0x08]:	Group Info
//
//	0x08.0x00	Power Info
//	0x08.0x02	Master TSE Info
//	0x08.0x04	Loco Info
//	0x08.0x05	BiDi, Loco
//	0x08.0x08	Modul
//	0x08.0x0A	Modul Config
//
//------------------------------------------------------------	[0x0A]:	Group NetWork
//
//	0x0A.0x00
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Ping,			Req),			//	170
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Ping,			Cmd),			//	171
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Ping,			Evt),			//	172
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Ping,			Ack),			//	173
//	0x0A.0x02
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_LatencyTest,	Cmd),			//	174
//	0x0A.0x06	Port Open
{NETCMDTABID(Z20_MAJOR_NET,	Z20_NET_Open,			Cmd), 			//	175
//	0x0A.0x07:	Port Close
{NETCMDTABID(Z20_MAJOR_NET,	Z20_NET_Close,			Cmd),			//	176
//	0x0A.0x08
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Sys_Info,		Req),			//	177
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_Sys_Info,		Ack),			//	178
//	0x0A.0x0A:	Interface Option
{NETCMDTABID(Z20_MAJOR_NET,	Z20_Net_Option,			Req),			//	179
{NETCMDTABID(Z20_MAJOR_NET,	Z20_Net_Option,			Cmd),			//	180
//	0x0A.0x0F
{NETCMDTABID(Z20_MAJOR_NET, Z20_Net_Error,			Evt),			//	181
{NETCMDTABID(Z20_MAJOR_NET, Z20_Net_Error,			Ack),			//	182
//	0x0A.0x10
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_RF_Connect,		Cmd),			//	183
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_RF_Connect,		Ack),			//	184
//	0x0A.0x11
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_RF_State,		Cmd),			//	185
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_RF_State,		Evt),			//	186
{NETCMDTABID(Z20_MAJOR_NET, Z20_NET_RF_State,		Ack),			//	187
//
//------------------------------------------------------------	[0x0B]:	Group RCS
//
//	0x0B.0x01:	Einstellungen
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Option,			Cmd),			//	188
//	0x0B.0x01:	Position
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Position,		Req),			//	189
//	0x0B.0x03:	Lock
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Lock,			Cmd),			//	190
//	0x0B.0x05
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Signal,			Req),			//	191
//	0x0B.0x05
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Signal,			Cmd),			//	192
//	0x0B.0x0C
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Route,			Req),			//	193
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Route,			Cmd),			//	194
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Route,			Evt),			//	195
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_Route,			Ack),			//	196
//	0x0B.0x10
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_TableDef,		Req),			//	197
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_TableDef,		Cmd),			//	198
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_TableDef,		Evt),			//	199
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_TableDef,		Ack),			//	200
//	0x0B.0x11
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_FieldIcon,		Req),			//	201
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_FieldIcon,		Cmd),			//	202
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_FieldIcon,		Evt),			//	203
{NETCMDTABID(Z20_MAJOR_RCS,	Z20_RCS_FieldIcon,		Ack),			//	204
//
//------------------------------------------------------------	[0x0C]:	Group Task
//
//	0x0C.0x00	State
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_State,			Req),			//	205
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_State,			Cmd),			//	206
//	0x0C.0x02	Create via NId
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateNId,		Req),			//	207
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateNId,		Cmd),			//	208
//	0x0C.0x03	Create via Hash
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateHash,		Req),			//	209
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateHash,		Cmd),			//	210
//	0x0C.0x03	Create via Hash
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateInitExit,	Req),			//	211
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CreateInitExit,	Cmd),			//	212
//	0x0C.0x04	Options
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CoreData,		Req),			//	213
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CoreData,		Cmd),			//	214
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_CoreData,		Ack),			//	215
//	0x0C.0x07	Delete
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_Delete,			Req),			//	216
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_Delete,			Cmd),			//	217
//	0x0C.0x08	Simpel Task Command Edit
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_StepCmd,		Req),			//	218
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_StepCmd,		Cmd),			//	219
{NETCMDTABID(Z20_MAJOR_ZPS, Z20_ZPS_StepCmd,		Ack),			//	220
//
//-------------------	Group 0x0E: File Control
//	0x0E.0x00	State
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_State,		Req),			//	221
//	0x0E.0x04	Open
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Open,			Req),			//	222
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Open,			Cmd),			//	223
//	0x0E.0x07	Info
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Info,			Req),			//	224
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Info,			Cmd),			//	225
//	0x0E.0x07	Close
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Close,		Req),			//	226
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_Close,		Cmd),			//	227
//	0x0E.0x11	Data Init
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_DataInit,		Req),			//	228
{NETCMDTABID(Z20_MAJOR_FILE, Z20_File_DataInit,		Cmd),			//	229
//
//------------------------------------------------------------	[0x11]:	Group xAcc
//
//	0x11.0x05:	Modul Data
{NETCMDTABID(Z20_MAJOR_xACC, Z20_ACC_Data,			Req),			//	230
//
//------------------------------------------------------------	[0x12]:	Group xLoco
//
//	0x12.0x00:	Track Mode
{NETCMDTABID(Z20_MAJOR_xLOCO, Z20_LOCO_State,		Req),			//	231
//
//	0x12.0x11:	Actual Loco Stack
{NETCMDTABID(Z20_MAJOR_xLOCO, Z20_LOCO_Stack,		Req),			//	232
//
//------------------------------------------------------------	[0x16]:	Group xTSE
//
//	0x16.0x00:	Track Mode
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_Mode,			Req),		//	233
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_Mode,			Cmd),		//	234
//	0x16.0x04:	Prog Clear
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_ProgClear,		Cmd),		//	235
//	0x16.0x08:	Prog Read
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_ProgRd,		Cmd),		//	236
//	0x16.0x09:	Prog Write
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_ProgWr,		Cmd),		//	237
//	0x16.0x11:	BiDi 'Logon Enable'
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_LogonEnable,	Cmd),		//	238
//	0x16.0x13:	BiDi 'Logon Assign'
{NETCMDTABID(Z20_MAJOR_xTSE,	Z20_xTSE_LogonAssign,	Cmd),		//	239
//
//------------------------------------------------------------	[0x17]:	Group xData
//
//	0x17.0x01:	Get Object By Idx with Base Data
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataItemListIdx,	Req),		//	240
//
//	0x17.0x01:	Get Object By Idx with Base Data
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataItemListNId,	Req),		//	241
//	0x17.0x08:	Get Object Value eXtended
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataValueX,		Req),		//	242
//	0x17.0x10
{NETCMDTABID(Z20_MAJOR_xDATA, Z20_xDataNameX,			Req),		//	243
{NETCMDTABID(Z20_MAJOR_xDATA, Z20_xDataNameX,			Cmd),		//	244
//
//	0x17.0x19:	Speed Tab
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataSpeedTab,		Req),		//	245
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataSpeedTab,		Cmd),		//	246
//	0x17.0x27:	Object GUI 0
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataLocoGUI0,		Req),		//	247
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataLocoGUI0,		Cmd),		//	248
//	0x17.0x28:	Object GUI 1
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataLocoGUI1,		Req),		//	249
{NETCMDTABID(Z20_MAJOR_xDATA,	Z20_xDataLocoGUI1,		Cmd),		//	250
//
//------------------------------------------------------------	[0x18]:	Group xInfo
//
//	0x18.0x00:	Power State
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_xInfo_Power,		Req),		//	251
//	0x18.0x08	Modul
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_Data,			Req),		//	252
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_Data,			Cmd),		//	253
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_Data,			Evt),		//	254
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_Data,			Ack),		//	255
//	0x18.0x0A	Modul
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_ConfigVal,		Req),		//	256
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_ConfigVal,		Cmd),		//	257
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_ConfigVal,		Evt),		//	258
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_Info_ConfigVal,		Ack),		//	259
//	0x18.0x0C:	Info Docu
{NETCMDTABID(Z20_MAJOR_xINFO,	Z20_xInfo_ConfigDocu,	Req),		//	260
//
//------------------------------------------------------------	[0x1A]:	Group xNet
//
//	0x1A.0x06	Port Open
{NETCMDTABID(Z20_MAJOR_xNET,	Z20_NET_Open,			Cmd), 		//	261
{NETCMDTABID(Z20_MAJOR_xNET,	Z20_NET_Close,			Cmd), 		//	262
//	0x1A.0x0E	System Info
{NETCMDTABID(Z20_MAJOR_xNET,	Z20_xNet_SysInfo,		Req), 		//	263
//
//------------------------------------------------------------	[0x1C]:	Group xZPS
//
//	0x1C.0x02	Script Modify
{NETCMDTABID(Z20_MAJOR_xZPS,	Z20_xZPS_Modify,		Req), 		//	264
{NETCMDTABID(Z20_MAJOR_xZPS,	Z20_xZPS_Modify,		Cmd), 		//	265
//
//------------------------------------------------------------	[0x1E]:	Group xFile
//
//	0x1E.0x02	File Info
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileInfo,			Req), 		//	266
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileInfo,			Cmd), 		//	267
//
//	0x1E.0x04	File Open
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileOpen,			Req), 		//	268
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileOpen,			Cmd), 		//	269
//
//	0x1E.0x05	File Close
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileClose,			Req), 		//	270
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileClose,			Cmd), 		//	271
//
//	0x1E.0x08	File Data
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileData,			Req), 		//	272
{NETCMDTABID(Z20_MAJOR_xFile,	Z20_xFileData,			Cmd), 		//	273
//
//	0x2F.0x08	File Data
{NETCMDTABID(Z20_MAJOR_xDebug,	Z20_xDebug_BiDiCfg,		Req), 		//	274
{NETCMDTABID(Z20_MAJOR_xDebug,	Z20_xDebug_BiDiCfg,		Cmd), 		//	275
*/