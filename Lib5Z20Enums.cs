using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20Enums
	{
		public static uint uGrpMax = 0x2F;
		public static uint uCmdMax = 0x3F;
		#region Net Mode
		public enum eNetMode
		{
			Req = 0x00,
			Cmd = 0x01,
			Evt = 0x02,
			Ack = 0x03,
		};
		#endregion
		#region Net Group
		public enum eNetGrp : byte
		{
			Grp00Sys = 0x00,            //	System
			Grp01Acc = 0x01,            //	Accessory
			Grp02Loco = 0x02,           //	Loco Control
			Grp03 = 0x03,               //	Frei
			Grp04RCS = 0x04,            //	Rail Control Center
			Grp05Train = 0x05,
			Grp06TSE = 0x06,            //	Track Control
			Grp07Data = 0x07,           //	Data
			Grp08Info = 0x08,           //	Info
			Grp09 = 0x09,
			Grp0ANet = 0x0A,            //	NetWork
			Grp0B = 0x0B,
			Grp0C = 0x0C,
			Grp0D = 0x0D,
			Grp0EFileCtrl = 0x0E,
			Grp0F = 0x0F,
			//
			Grp11Acc = 0x11,            //	Accessory
			Grp12Loco = 0x12,			//	eXtended:	Loco
			Grp16TSE = 0x16,			//	eXtended:	TSE
			Grp17Data = 0x17,			//	eXtended:	Data
			Grp18Info = 0x18,			//	eXtended:	Info
			Grp1ANet = 0x1A,			//	eXtended:	Net Management
			//
			Grp1E_File = 0x1E,			//	eXtended:	File
			//
			GrpDebug = 0x2F,
			GrpUpdate = 0xF8,
			GrpNONE = 0xFF,
		};
		#endregion
		#region Net Command
		public enum eNetCmd : byte
		{
			SysState = 0x00,			//	System
			SysReset = 0x02,            //	System Config
			SysPwrMode = 0x04,          //	System Power Mode
			SysError = 0x10,            //	Fehler Meldung
			//
			AccState = 0x00,            //	Accessory:	State
			AccMode = 0x01,             //	Accessory:	Mode
			AccGPIO = 0x02,             //	Accessory:	GPIO
			AccPin4 = 0x04,				//	Accessory:	Pin, 4 Byte Form
			AccData = 0x05,             //	Accessory:	Data
			AccPin6 = 0x06,				//	Accessory:	Pin, 6 Byte Form
			AccAutoSpeed = 0x07,		//	Automatik HLU
			AccLimitCmd = 0x09,			//	Multi Limit Command (Peter)
			AccSignalCtrl = 0x0A,		//	Signal Steuerung
			//
			LocoState = 0x00,           //	Loco Control
			LocoMode = 0x01,            //	Loco Control
			LocoSpeed = 0x02,           //	Loco Control
			LocoFxGPIO = 0x03,
			LocoFxNum = 0x04,			//	Loco Function Control
			LocoSxNum = 0x05,           //	Loco Spezial Features
			LocoFxInfo = 0x08,			//	Loco Function Info
			LocoFxData = 0x09,          //	Loco Function Data
			LocoActiv = 0x10,           //	Fahrzeug 'Aktiv'
			LocoStack = 0x11,			//	Loco Stack
			LocoBiDi = 0x18,            //	BiDi Info
										//
			TrainPartList = 0x01,		//	Zug: Fahrzeug Liste
			TrainPartFind = 0x02,		//	Zug: Fahrzeug Suchen
			TrainOwnerSet = 0x03,           //	Zug: 'Eigentümer'
			TrainOwnerDel = 0x04,           //	Zug: 'Eigentümer'
			TrainPartSet = 0x08,        //	Zug: Fahrzeug festlegen
			TrainPartCfg = 0x09,       //	Zug: Fahrzeug Config
			TrainPartDel = 0x0F,         //	Zug: Fahrzeug Löschen
										 //
			RCS_Position = 0x01,        //	Position
			RCS_Lock = 0x02,            //	Objekt Sperre
			RCS_Speed = 0x04,           //	Speed Limit
			RCS_Signal = 0x05,          //	Signal
			RCS_Shunting = 0x05,        //	Rangierfahrt
			RCS_Route = 0x0C,           //	Fahrstraße
			RCS_Table = 0x10,           //	Tisch
			RCS_Icon = 0x11,            //	Icon
										//
			CfgDate = 0x01,
			//----	[0x06]:	TSE
			TSE_Mode = 0x00,            //	TSE Mode
			TSE_Info = 0x01,            //	TSE Info
			TSE_ValClr = 0x04,			//	TSE Values Clear
			TSE_ValRd = 0x08,           //	TSE Read
			TSE_ValWr = 0x09,           //	TSE Write
			TSE_ValRdX = 0x0A,          //	TSE Read  eXtended (32Bit Num)
			TSE_ValWrX = 0x0B,          //	TSE Write eXtended (32Bit Num)
			TSE_ValWr16 = 0x0D,         //	TSE Write
			TSE_TaskRd = 0x0E,			//	TSE Task Read
			TSE_Find = 0x10,            //	TSE Find
			TSE_RCN218Ctrl = 0x11,      //	RCN 218 Control
			TSE_RCN218UId  = 0x12,      //	RCN 218 Decoder UId
			TSE_RCN218Assign = 0x13,    //	RCN 218 Assign
			TSE_ZSysGuiCtrl = 0x14,		//	Decoder GUI Control
			TSE_ZSysGuiInfo = 0x15,		//	Decoder GUI Info's
			TSE_ZSysGuiLog = 0x16,		//	Decoder GUI BiDi Datagramms
			TSE_BiDi0 = 0x1D,           //	TSE BiDi Raw Data Channel 0
			TSE_BiDi1 = 0x1E,           //	TSE BiDi Raw Data Channel 1
			TSE_BiDiC = 0x1F,           //	TSE BiDi Raw Data Control
			//
			//----	[0x07]:	Data
			GroupCount = 0x00,          //	Data Group
			ItemIdx = 0x01,             //	Object Index
			ItemNId = 0x02,             //	Object NId
			DataCRC32 = 0x03,           //	Data CRC
			DataFlag = 0x04,            //	Data Flag
			DataValue = 0x08,           //	Data Value
			DataName = 0x10,            //	Data Name
			DataImage = 0x12,           //	Data Image
			DataFxMode = 0x14,          //	Data FxCfg
			DataFxData = 0x15,          //	Data FxCfg
			DataSpeedItem = 0x18,       //	Data Speed
			DataSpeedTab = 0x19,        //	Data Speed Table
			DataSave = 0x1A,			//	Data Save
			DataDel = 0x1F,				//	Data Item Delete
			DataFx_ALL = 0x20,          //	Data FxCfg
			//
			DataName21 = 0x21,          //	PC Only:	Data Name
			DataObjGUI0 = 0x27,         //	PC Only:	Object GUI 0
			DataObjGUI1 = 0x28,         //	PC Only:	Object GUI 1
			//
			//----	[0x08]:	Info
			InfoPower = 0x00,           //	Info:   Power
			InfoTSE = 0x02,				//	Info:   TSE
			InfoLoco = 0x04,			//	Info:   Loco
			InfoBiDi = 0x05,            //	Info:   RailCom
			InfoZACK = 0x06,            //	Info:   ZACK
			InfoData = 0x08,			//	Info:   Modul
			InfoBinTagValue = 0x0A,		//	Info:   Config
			InfoBinTagDocu = 0x0C,		//	Info:   Docu
			InfoPowerX = 0x20,          //	Info:   PowerX
			//
			//----	[0x0A]:	Network
			Net_Ping = 0x00,            //	Ping
			Net_Open = 0x06,            //	Simpel Open
			Net_Close = 0x07,
			Net_Info = 0x08,            //	Modul Infos
			Net_Option = 0x0A,          //	Interface Option
			Net_Error = 0x0F,           //	Interface Error
			Net_Dbg = 0x10,             //	Debug Commands
										//
										//	File Control
			FileInfo = 0x02,            //	File 'Info'
			FileOpen = 0x04,            //	File 'Open'
			FileClose = 0x05,           //	File 'Close'
			FileData = 0x08,           //	File 'Data'
			FileNand = 0x0A,           //	File 'NAND Data'

			//	Debug
			DebugText = 0x01,			//	Text
			DebugTSE_Tx = 0x10,         //	TSE Tx
			DebugBiDiConfig = 0x20,		//	Debug: BiDi Config
			DebugBiDiError = 0x21,      //	Debug: BiDi Fehler
			DebugBiDiInit = 0x22,       //	Debug: BiDi Init
			DebugBiDiData = 0x23,       //	Debug: BiDi Data
			DebugBiDiCalc = 0x24,       //	Debug: BiDi Calc
			DebugBiDiResult = 0x25,     //	Debug: BiDi Result
										//	Update
			UpdateInit = 0x01,          //	Update 'Init'
			UpdateData = 0x02,          //	Update 'Data'
			UpdateExit = 0x03,          //	Update 'Exit'
										//
			NONE = 0xFF,
		};
		#endregion
		//
		#region Net Group
		public enum eNetPin6Type : byte
		{
			Zs900Block = 0x01,
			Zs900Limit = 0x02,
			Zs900TrackI = 0x08,
			Zs900PinOut = 0x10,
			Zs900PinInp = 0x20,
			//
			ZsServoPinOut = 0x10,
			ZsServoStep = 0x12,
			ZsServoStepTime = 0x13,
			ZsServoPosition = 0x14,
			ZsServoPosTime = 0x15,
			ZsServoPinInp = 0x20,
		}
		#endregion
		#region Net NId Groups
		public enum eDataGrpIdx
		{
			LocoDCC = 0,
			LocoMM2 = 1,
			LocoMfx = 2,
			LocoSys = 3,
			Consist = 4,
			AccDCC = 5,
			AccMx8 = 6,
			AccMx9 = 7,
			AccZs9 = 8,
			Zs100Master = 9,
			CabZs3 = 10,
			Panels = 11,
			Mx6Upd = 12,
			Mx6Snd = 13,
			None = 0xFFFF,
		};
		public enum eDataGrpNId
		{
			LocoDCC = 0x0000,
			LocoDCCMin = 0x0001,
			LocoDCCMax = 0x2EFF,
			LocoMM2Min = 0x2800,
			LocoMM2Max = 0x28FF,
			LocoSysMin = 0x3E00,
			LocoSysMax = 0x3FFF,
			//
			ConsistMin = 0x2F00,
			ConsistMax = 0x2FFF,
			//
			AccDCCsMin = 0x3000,
			AccDCCsMax = 0x31FF,
			AccDCCxMin = 0x3200,
			AccDCCxMax = 0x39FF,
			AccMM1Min = 0x3A00,
			AccMM1Max = 0x3DFF,
			//
			SysMx01 = 0x5000,
			SysMx08Min = 0x5040,
			SysMx08Max = 0x507F,
			SysMx09Min = 0x5080,
			SysMx09Max = 0x50FF,
			SysMx31 = 0x50D0,
			SysCSA = 0x50C0,
			Panel = 0x6000,
			Tasks = 0x6100,
			CfgDb = 0x6400,
			Mx6Upd = 0x6500,
			Mx6Snd = 0x6600,
			SysSnd = 0x6700,
			SysLbl = 0x7E00,
			//----------------
			LocoMfxMin = 0x8000,
			LocoMfxMax = 0xBFFF,
			//----------------
			Zs100Min = 0xC000,
			Zs100Max = 0xC0FF,
			Zs100MiWi = 0xC100,
			Z20PC = 0xC200,
			//
			Zs300 = 0xC300,
			Zs300Min = 0xC300,
			Zs300Max = 0xC3FF,
			//
			Zs300MiWi = 0xC400,
			//
			XNet1 = 0xC500,
			XNet2 = 0xC520,
			ZsBooster = 0xCF00,
			ZsBoosterMin = 0xCF00,
			ZsBoosterMax = 0xCFFF,
			//
			Zs900Min = 0xD000,
			Zs900Max = 0xDFFF,
			//---------------
			PanelGBS = 0xE000,
			MaxLegalNId = 0xEFFF,
			None = 0xFFFF,
		};
		#endregion
		#region Track Format
		public enum enumTrkFmt
		{
			Unknown = 0,
			DCC = 1,
			MM1 = 2,
			MM2 = 3,
			SX = 4,
			mfx = 5,
			Sys = 7,
		}
		#endregion
		#region Speed Steps
		public enum enumTrkSteps
		{
			Unknown = 0,
			Max0014 = 1,
			Max0027 = 2,
			Max0028 = 3,
			Max0128 = 4,
			Max1024 = 5,
		}
		#endregion
		#region BiDi Direction
		public enum enumBiDiFwdRev
		{
			Unknown = 0,
			Fwd = 1,
			Rev = 2,
		}
		public enum enumBiDiEastWest
		{
			Unknown = 0,
			East = 1,
			West = 2,
		}
		#endregion
		#region Fx Data Items [0x15]
		public enum enumFxDataItem
		{
			None = 0,
			Addr = 0x01,
			Value = 0x02,
			Time1 = 0x05,
			Time2 = 0x06,
			Icon = 0x10,
			Sound = 0x11,
		}
		#endregion
		//
		#region Net NId Ranges
		public enum eNetZ20NId
		{
			LocoDccMin = 0x0000,
			LocoDccMax = 0x27FF,
			LocoMM2Min = 0x2800,    //	256 MM2 Locos
			LocoMM2Max = 0x28FF,
			ConsistMin = 0x2F00,    //	Consist's,	First NId
			ConsistMax = 0x2FFF,    //	Max. 256,	Last NId ... Low Byte ==> Consist Number
			LocoSysMin = 0x3E00,    //	System Locos
			LocoSysMax = 0x3FFF,    //

			AccDccMin = 0x3000,
			AccDccMax = 0x31FF,     //	512 DCC Simpel Decoder
									//
			AccMX8Min = 0x5040,     //	MX8
			AccMX8Max = 0x507F,
			AccMX9Min = 0x5080,     //	MX9
			AccMX9Max = 0x50BF,
			//
			//----	None Devices
			//
			PanelMin = 0x6000,
			PanelMax = 0x60FF,
			//
			RouteMin = 0x6100,		//	Routes = Fahrwege
			RouteMax = 0x63FF,		//	Sequenz of Switch Commands
			//
			SndMx6Min = 0x6600,
			SndMx6Max = 0x66FF,
			//
			LocoMfxMin = 0x8000,	//	Märklin mfx Fahrzeuge, max 16384
			LocoMfxMax = 0xBFFF,    //
			//
			//----	New (Z20) System Objects
			//
			Zs100Min = 0xC000,      //	Zentralen (MX10)
			Zs100Max = 0xC0FF,
			//
			LanMin = 0xC200,		//	NId for LAN/WiFi Devices, Low Byte == Low Byte Ip
			LanMax = 0xC2FF,
			//
			Zs300Min = 0xC300,      //	Fahrpulte (MX32)
			Zs300Max = 0xC3FF,
			//
			AccZs9Min = 0xD000,
			AccZs9Max = 0xDFFF,
			//
			ObjectsMin = 0xE000,	//	Special Objects
			ObjectsMax = 0xEFFF,
			//
			FilesMin = 0xF000,		//	File Transfer Handles
			FilesMax = 0xFFFF,		//	Special Command Set!!!
			//
			None = 0xFFFF,
		};
		#endregion
		#region Net Options
		public enum eNetOption
		{
			None = 0x00,
			LongMsg = (1 << 0),
			MultiList = (8 << 2),
			DebugOutput = (1 << 24),
		};
		#endregion
		//
		#region Modul Info
		public enum eModulInfo
		{
			VersionHW = 1,          //	Hardware Version
			VersionSW = 2,          //	Software Version
			BuildDate = 3,			//	Software Build Date
			BuildTime = 4,			//	Software Build Time
			ClockDate = 5,
			ClockTime = 6,
			//
			MiWiBuildHW = 8,
			MiWiBuildSW = 9,
			MiWiChannel = 10,
			MiWiBuildTxCnt = 11,
			//
			ModulNum = 20,			//	Modul Nummer
			ModulType = 100,		//	Modul Art
			ModulRunTime = 200,		//	Laufzeit
			//
			XDateTime = 10,
			//
			PinOutMax = 0x100,
			PinInpMax = 0x101,
			PinTrkMax = 0x102,
			//
			ActivCfgTrk = 0x0201,
			ActivCfgPnt = 0x0202,
			ActivCfgSig = 0x0203,
			//
			ModulNumX = 0x8014,
			//
			ModulAddOn0 = 0x0210,
			ModulAddOn1 = 0x0220,
			ModulWireCnt = 0x0300,
			ModulWireA01 = 0x0301,
			//
			DisplayNIdPin = 0xD000,
			//
			None = 0xFFFF,
		};
		#endregion
		//
		#region BiDi Types
		public enum eNetBiDiType
		{
			Speed = 0x0100,
			TiltCurve = 0x0101,
			Config = 0x0200,
			QoS = 0x0300,
			Level = 0x0400,
			EastWest = 0x0800,
			TrackVolt = 0x1000,
			Alarm = 0x1100,
		};
		#endregion
		//
		#region GUI Types
		public enum eNetTypeGUI
		{
			Basic = 0x2000,             //	Fahrzeug GUI
			FxGrp0 = 0x2001,            //	Fahrzeug FxGrp0
			FxGrp1 = 0x2002,            //	Fahrzeug FxGrp1
		}
		#endregion
		#region Image Types
		public enum enumImgType
		{
			LocoSId = 1,
			LocoUId = 2,
			TachoSId = 3,
			TachoUId = 4,
		};
		#endregion
		//
		#region Enum Loco Find
		public enum enumLocoFind
		{
			Z20_TSE_LocoFindNone = 0x0000,                  //	Fahrzeug Suche, Initial Zustand
			Z20_TSE_LocoFindOld = 0x0010,					//	Fahrzeug Suche, Starten
			Z20_TSE_LocoFindInit = 0x1000,					//	Fahrzeug Suche, Starten
			Z20_TSE_LocoFindTime = 0x1001,					//	Fahrzeug Suche, Aussende Protokoll (Timestamp)
			Z20_TSE_LocoFindLoco = 0x1100,					//	Fahrzeug Suche, Gefundenes Fahrzeug
			Z20_TSE_LocoFindExit = 0x1FFF,					//	Fahrzeug Suche, beendet/beenden
		}
		#endregion
		#region Enum Loco Find, RCN218
		public enum enumFindRCN218
		{
			Z20_TSE_RCN218_Stop		  = 0x0000,		//	Fahrzeug Suche beenden
			Z20_TSE_RCN218_AutoIdNone = 0x0001,		//	Logon Aussendung mit Paramtern starten
			Z20_TSE_RCN218_OnceIdNone = 0x0002,     //	Eine Logon Sequenz
			Z20_TSE_RCN218_AutoIdInc5 = 0x0200,		//	Logon mit Id+5
			Z20_TSE_RCN218_OnceIdInc5 = 0x0202,		//	Logon mit Id+5
			Z20_TSE_RCN218_Activ	  = 0x0800,		//	RCN 218 'läuft'
			Z20_TSE_RCN218_Error	  = 0x1FFF,		//	Fahrzeug Suche, beendet/beenden
		}
		#endregion
		//
		#region Enum Decoder GUI Control
		public enum enumTrackCtrlGUI
		{
			None = 0x0000,				//	None
			ReqAck = 0x1000,			//	ZIMO GUI Abfrage (from User)
			//
			RCNa = 0x4001,              //	RCN 218, Datablock 'a'
			//
			WaitData = 0x8001,			//	GUI Data   Wait
			Done = 0x8FFF,              //	GUI 'Fertig'
										//
			ErrTimeOutInit = 0xF001,    //	GUI Header Timeout
			ErrTimeOutData = 0xF002,    //	GUI Data   Timeout
			ErrWaitGUI = 0xF008,		//
			ErrTaskActiv = 0xF010,      //	Decoder Read Task Activ
			ErrLocoUnknown = 0xF011,    //	Fahrzeug unbekannt
			//
			Zs01ErrLoco	= 0xF101,       //	ZIMO GUI Wrong Loco
			Zs01ErrBiDi = 0xF102,       //	ZIMO GUI Loco no BiDI
			Zs01ErrHdr = 0xF103,        //	ZIMO GUI Header Error
			Zs01ErrBuild = 0xF104,      //	ZIMO GUI Version Error
		}
		#endregion

		#region Enum Decoder GUI Info
		public enum enumTrackInfoGUI
		{
			None	= 0x00,          //	
			Header	= 0x01,			//	Header Info
			Build	= 0x02,			//	Version
			Size	= 0x03,          //	Size
			Date	= 0x04,          //	GUI Date
			Time	= 0x05,          //	GUI Time
			Addr	= 0x06,          //	GUI Wunschadresse
			FxMax	= 0x07,          //	GUI Max Funktionen
			Image	= 0x08,          //	GUI Fahrzeug Bild
			Gauge	= 0x09,          //	GUI Tacho
			ChkLst	= 0x0A,          //	GUI xChunk List
			//
			Block	= 0x10,         //	Block
			CheckDone = 0x11,       //	Block Check Done
			QuadLog  = 0x20,		//	Quadruppel Log
			CheckErr = 0xF1,        //	Check Error
			ErrBuild = 0xFE,        //	Error Version
		}
		#endregion
		//
		#region Enum Pin 6 Types
		public enum enumPin6Type
		{
			None = 0x00,			//	
			TrkState = 0x01,		//	Frei/Besetzt
			TrkLimit = 0x02,		//	HLU
			Output = 0x10,			//	Ausgänge
			Input = 0x20,			//	Eingänge
			LED = 0x40,
		}
		#endregion
		//
		#region Enum Pin 6 States, Track
		public enum enumPin6StateTrack : UInt16
		{
			PwrOffEmpty		= 0x0000,            //		Fahrspannung AUS, Anzeigezustand Frei
			PwrOnEmpty		= 0x0100,            //		Fahrspannung Ein, Anzeigezustand Frei
			PwrOffOcopied	= 0x1000,            //		Fahrspannung AUS, Anzeigezustand Besetzt
			PwrOnOcopied	= 0x1100,            //		Fahrspannung Ein, Anzeigezustand Besetzt
			//
			OvrCurEmpty		= 0x1201,            //		UESL- temporär, Anzeigezustand Besetzt
			OvrCurOcopied	= 0x1202,            //		UESL- temporär, Anzeigezustand Frei
			OvrOffOcopied	= 0x1203,            //		UESL- abgeschaltet, Anzeigezustand, Besetzt
		}
		#endregion
		#region Enum Pin 6 States, Output
		public enum enumPin6StatePinOut : UInt16
		{
			None		= 0x0000,				//		unbekannt
			Open		= 0x0001,				//		Offen
			Masse		= 0x0002,				//		Masse
			Power		= 0x0004,				//		Positiv
												//
			Position1	= 0x1000,               //		Position 1
			Position2	= 0x1001,				//		Position 2
			Position3	= 0x1002,               //		Position 3
			Position4	= 0x1003,               //		Position 4
			Position5	= 0x1004,				//		Position 5
			Position6	= 0x1005,				//		Position 6
			Position7	= 0x1006,				//		Position 7
			Position8	= 0x1007,				//		Position 8
		}
		#endregion
		#region Enum Pin 6 States, Input
		public enum enumPin6StatePinInp : UInt16
		{
			None = 0x0000,              //		unbekannt
			Open = 0x0001,              //		Offen
			Masse = 0x0002,             //		Masse
			Pwr3 = 0x0003,             //		Positiv
			Pwr4 = 0x0004,             //		Positiv
		}
		#endregion
		//
		#region Enum File Num
		public enum enumFileNum : UInt16
		{
			None = 0x0000,          //	
			TxtGUI		 = 0x0100,		//	'Fixed' Filenum für GUI Texte'
			TxtLbl		 = 0x0110,		//	'Fixed' Filenum für Lbl Texte'
			Zs100LPC1788 = 0x1501,      //	Firmware MX10
			Zs100Xilinx  = 0x1502,      //	Firmware MX10, Xilinx
			Zs100MiWi	 = 0x1503,      //	Firmware MX10, MiWi
		}
		#endregion
		//
		#region Enums to Debug Strings
		#region Get Mode Name
		public static string GetModeName(eNetMode _eMode)
		{
			switch (_eMode)
			{
				case eNetMode.Req:
				{
					return ("Req");
				}
				case eNetMode.Cmd:
				{
					return ("Cmd");
				}
				case eNetMode.Evt:
				{
					return ("Evt");
				}
				case eNetMode.Ack:
				{
					return ("Ack");
				}
				default:
				{
					return (_eMode.ToString());
				}
			}
		}
		#endregion
		#region Get Group Name
		public static string GetGrpName(eNetGrp _eGrp)
		{
			string sName;
			if(GetGrpName(_eGrp, out sName))
			{
				return (sName);
			}
			uint uGrp = (uint)_eGrp;
			return (uGrp.ToString("X2"));
		}
		public static Boolean GetGrpName(eNetGrp _eGrp, out string _sName)
		{
			Boolean bOk = false;
			_sName = string.Empty;
			//
			switch (_eGrp)
			{
				case eNetGrp.Grp00Sys:
				{
					_sName = "System";
					bOk = true;
				}
				break;
				case eNetGrp.Grp01Acc:
				{
					_sName = "Zubehör";
					bOk = true;
				}
				break;
				case eNetGrp.Grp02Loco:
				{
					_sName = "Loco";
					bOk = true;
				}
				break;
				case eNetGrp.Grp03:
				{
					_sName = "Grp03";
					bOk = true;
				}
				break;
				case eNetGrp.Grp04RCS:
				{
					_sName = "R.C.S.";
					bOk = true;
				}
				break;
				case eNetGrp.Grp05Train:
				{
					_sName = "Train";
					bOk = true;
				}
				break;
				case eNetGrp.Grp06TSE:
				{
					_sName = "TSE";
					bOk = true;
				}
				break;
				case eNetGrp.Grp16TSE:
				{
					_sName = "xTSE";
					bOk = true;
				}
				break;
				case eNetGrp.Grp07Data:
				{
					_sName = "Data";
					bOk = true;
				}
				break;
				case eNetGrp.Grp17Data:
				{
					_sName = "DataX";
					bOk = true;
				}
				break;
				case eNetGrp.Grp08Info:
				{
					_sName = "Info";
					bOk = true;
				}
				break;
				case eNetGrp.Grp09:
				{
					_sName = "Grp09";
					bOk = true;
				}
				break;
				case eNetGrp.Grp0ANet:
				{
					_sName = "Net";
					bOk = true;
				}
				break;
				case eNetGrp.Grp0B:
				{
					_sName = "Grp0B";
					bOk = true;
				}
				break;
				case eNetGrp.Grp0EFileCtrl:
				{
					_sName = "File";
					bOk = true;
				}
				break;
				case eNetGrp.Grp0F:
				{
					_sName = "RxTx";
					bOk = true;
				}
				break;
				case eNetGrp.GrpDebug:
				{
					_sName = "Debug";
					bOk = true;
				}
				break;
				//
				case eNetGrp.Grp18Info:
				{
					_sName = "X-Info";
					bOk = true;
				}
				break;
				case eNetGrp.Grp1ANet:
				{
					_sName = "X-Net";
					bOk = true;
				}
				break;
			}
			return (bOk);
		}
		#endregion
		#region Get Command Name
		public static string GetCmdName(eNetGrp _eGrp, eNetCmd _eCmd)
		{
			switch (_eGrp)
			{
				case eNetGrp.Grp00Sys:
				{
					return (GetCmdNameSys(_eCmd));
				}
				case eNetGrp.Grp01Acc:
				{
					return (GetCmdNameAcc(_eCmd));
				}
				case eNetGrp.Grp02Loco:
				case eNetGrp.Grp12Loco:
				{
					return (GetCmdNameLoco(_eCmd));
				}
				case eNetGrp.Grp03:
				{
					return ("Grp03, Free");
				}
				case eNetGrp.Grp04RCS:
				{
					return (GetCmdNameRCS(_eCmd));
				}
				case eNetGrp.Grp05Train:
				{
					return (GetCmdNameCfg(_eCmd));
				}
				case eNetGrp.Grp06TSE:
				case eNetGrp.Grp16TSE:
				{
					return (GetCmdNameTSE(_eCmd));
				}
				case eNetGrp.Grp07Data:
				case eNetGrp.Grp17Data:
				{
					return (GetCmdNameData(_eCmd));
				}
				case eNetGrp.Grp08Info:
				{
					return (GetCmdNameInfo(_eCmd));
				}
				case eNetGrp.Grp09:
				{
					return ("Grp09");
				}
				case eNetGrp.Grp0ANet:
				{
					return (GetCmdNameNet(_eCmd));
				}
				case eNetGrp.Grp0B:
				{
					return ("Grp0B");
				}
				case eNetGrp.Grp0EFileCtrl:
				{
					return (GetCmdNameFile(_eCmd));
				}
				case eNetGrp.Grp0F:
				{
					return ("Byte");
				}
				case eNetGrp.Grp18Info:
				{
					return (GetCmdNameInfo(_eCmd));
				}
				case eNetGrp.Grp1ANet:
				{
					return (GetCmdNameNet(_eCmd));
				}
				case eNetGrp.GrpDebug:
				{
					return (GetCmdNameDebug(_eCmd));
				}
				default:
				{
					return (_eCmd.ToString());
				}
			}
		}
		#endregion
		#region Get Command Name, System
		public static string GetCmdNameSys(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.SysState:
				{
					return ("State");
				}
				case eNetCmd.SysPwrMode:
				{
					return ("Power");
				}
				case eNetCmd.SysError:
				{
					return ("ERROR");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return ("0x" + iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		#region Get Command Name, Zubehör
		public static string GetCmdNameAcc(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.AccState:
				{
					return ("State");
				}
				case eNetCmd.AccMode:
				{
					return ("Mode ");
				}
				case eNetCmd.AccGPIO:
				{
					return ("GPIO ");
				}
				case eNetCmd.AccPin4:
				{
					return ("Pin4");
				}
				case eNetCmd.AccData:
				{
					return ("Data ");
				}
				case eNetCmd.AccPin6:
				{
					return ("Pin6");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return ("0x" + iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		#region Get Command Name, Loco
		public static string GetCmdNameLoco(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.LocoState:
				{
					return ("State");
				}
				case eNetCmd.LocoMode:
				{
					return ("Mode");
				}
				case eNetCmd.LocoSpeed:
				{
					return ("Speed");
				}
				case eNetCmd.LocoFxGPIO:
				{
					return ("Fx GPIO");
				}
				case eNetCmd.LocoFxNum:
				{
					return ("Fx State");
				}
				case eNetCmd.LocoSxNum:
				{
					return ("Sx State");
				}
				case eNetCmd.LocoFxInfo:
				{
					return ("Fx Info");
				}
				case eNetCmd.LocoFxData:
				{
					return ("Fx Data");
				}
				case eNetCmd.LocoActiv:
				{
					return ("Aktiv");
				}
				case eNetCmd.LocoStack:
				{
					return ("Stack");
				}
				case eNetCmd.LocoBiDi:
				{
					return ("BiDi");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return ("0x" + iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		#region Get Command Name, RCS
		public static string GetCmdNameRCS(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.RCS_Position:
				{
					return ("Position");
				}
				case eNetCmd.RCS_Lock:
				{
					return ("Lock");
				}
				case eNetCmd.RCS_Speed:
				{
					return ("Limit");
				}
				case eNetCmd.RCS_Signal:
				{
					return ("Signal");
				}
				case eNetCmd.RCS_Route:
				{
					return ("Route");
				}
				case eNetCmd.RCS_Table:
				{
					return ("Tisch");
				}
				case eNetCmd.RCS_Icon:
				{
					return ("Feld");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return ("0x" + iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		#region Get Command Name, Config
		public static string GetCmdNameCfg(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.CfgDate:
				{
					return ("CfgDate");
				}
				default:
				{
					return (_eCmd.ToString());
				}
			}
		}
		#endregion
		#region Get Command Name, TSE
		public static string GetCmdNameTSE(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.TSE_Info:
				{
					return ("Info");
				}
				case eNetCmd.TSE_Mode:
				{
					return ("Mode");
				}
				case eNetCmd.TSE_ValRd:
				{
					return ("Read");
				}
				case eNetCmd.TSE_ValWr:
				{
					return ("Write");
				}
				case eNetCmd.TSE_Find:
				{
					return ("Loco Find");
				}
				case eNetCmd.TSE_ZSysGuiCtrl:
				{
					return ("Decoder GUI Ctrl");
				}
				case eNetCmd.TSE_ZSysGuiInfo:
				{
					return ("Decoder GUI Info");
				}
				case eNetCmd.TSE_ZSysGuiLog:
				{
					return ("Decoder GUI Log");
				}
				//
				case eNetCmd.TSE_BiDi0:
				{
					return ("BiDi0");
				}
				case eNetCmd.TSE_BiDi1:
				{
					return ("BiDi1");
				}
				case eNetCmd.TSE_BiDiC:
				{
					return ("BiDi Ctrl");
				}
				default:
				{
					return (_eCmd.ToString());
				}
			}
		}
		#endregion
		#region Get Command Name, Data
		public static string GetCmdNameData(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.GroupCount:
				{
					return ("GrpCnt");
				}
				case eNetCmd.ItemIdx:
				{
					return ("ItemIdx");
				}
				case eNetCmd.ItemNId:
				{
					return ("ItemNId");
				}
				case eNetCmd.DataCRC32:
				{
					return ("CRC32");
				}
				case eNetCmd.DataFlag:
				{
					return ("Flag");
				}
				case eNetCmd.DataValue:
				{
					return ("Value");
				}
				case eNetCmd.DataName:
				{
					return ("Name");
				}
				case eNetCmd.DataImage:
				{
					return ("Image");
				}
				case eNetCmd.DataFxMode:
				{
					return ("FxMode");
				}
				case eNetCmd.DataFxData:
				{
					return ("FxData");
				}
				case eNetCmd.DataName21:
				{
					return ("Name (PC)");
				}
				case eNetCmd.DataObjGUI0:
				{
					return ("ObjGUI (PC, 0)");
				}
				case eNetCmd.DataObjGUI1:
				{
					return ("ObjGUI (PC, 1)");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return ("0x" + iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		#region Get Command Name, Info
		public static string GetCmdNameInfo(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.InfoPower:
				{
					return ("Power");
				}
				case eNetCmd.InfoBiDi:
				{
					return ("BiDi");
				}
				case eNetCmd.InfoZACK:
				{
					return ("ZACK");
				}
				case eNetCmd.InfoData:
				{
					return ("Info Data");
				}
				case eNetCmd.InfoBinTagValue:
				{
					return ("Bin Tag Val");
				}
				case eNetCmd.InfoPowerX:
				{
					return ("PwrX");
				}
				case eNetCmd.InfoBinTagDocu:
				{
					return ("Bin Tag Docu");
				}
				default:
				{
					return ("N.A.");
				}
			}
		}
		#endregion
		#region Get Command Name, Network
		public static string GetCmdNameNet(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.Net_Ping:
				{
					return ("Ping");
				}
				case eNetCmd.Net_Open:
				{
					return ("Open");
				}
				case eNetCmd.Net_Close:
				{
					return ("Close");
				}
				case eNetCmd.Net_Info:
				{
					return ("Info");
				}
				case eNetCmd.Net_Option:
				{
					return ("Option");
				}
				case eNetCmd.Net_Error:
				{
					return ("Error");
				}
				case eNetCmd.Net_Dbg:
				{
					return ("Debug");
				}
				default:
				{
					return ("N.A.");
				}
			}
		}
		#endregion
		#region Get Command Name, File Control
		public static string GetCmdNameFile(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.FileOpen:
				{
					return ("Open");
				}
				case eNetCmd.FileInfo:
				{
					return ("Info");
				}
				case eNetCmd.FileClose:
				{
					return ("Close");
				}
				case eNetCmd.FileData:
				{
					return ("File Data");
				}
				default:
				{
					return (_eCmd.ToString());
				}
			}
		}
		#endregion
		//
		#region Get Command Name, Debug
		public static string GetCmdNameDebug(eNetCmd _eCmd)
		{
			switch (_eCmd)
			{
				case eNetCmd.DebugBiDiError:
				{
					return ("BiDi, Err.");
				}
				case eNetCmd.DebugBiDiInit:
				{
					return ("BiDi, Init");
				}
				case eNetCmd.DebugBiDiData:
				{
					return ("BiDi, Data");
				}
				case eNetCmd.DebugBiDiResult:
				{
					return ("BiDi, Res.");
				}
				default:
				{
					UInt32 iCmd = (UInt32)_eCmd;
					return (iCmd.ToString("X2"));
				}
			}
		}
		#endregion
		//
		#endregion
	}
}

