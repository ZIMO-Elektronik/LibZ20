using System;
using System.Collections;
using static Lib5Z20.Lib5Z20Enums;

/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x02, Loco Actions										*/
/*	The primäry UDP Receiver uses these Classes to Decode UDP Datagramms to meaningfull 		*/
/*	Variable Names. After that, it checks if an Event is Assigned to that Message and			*/
/*	Issues that Event.																			*/
/*	'High-Level' Routines should assign Events and Prozess the typeClasses						*/
/*																								*/
/*	General Info, if not otherwise noted, all Loco Messages Contains Loco NId					*/
/*	(Byte 0/1 in User Data). In case of DCC these NId is equel to the Loco Adress		 		*/
/*	-	0x02.0x01:	Loco State Message, always from MX10										*/
/*	-	0x02.0x02:	Loco Mode  Message, Send bei Cab to spezify the Track Modes of Loco			*/
/*										Requested if Loco Exists, ACK by MX10					*/
/*	-	0x02.0x02:	Loco Speed Message, Contains Loco NId, 16 Bit Speed Information,			*/
/*										'Master' (normaly CAB) how sends that Command			*/
/*																								*/
/************************************************************************************************/


namespace Lib5Z20
{
	#region Loco Msg State	[0x02.x01]
	public class typeZ20MsgLocoState
	{
		#region Properties
		public UInt16 u16NId { get; private set; }			//	Loco Network Id (if DCC, Adress of Loco)
		public byte u08Train { get; private set; }			//	Train Number '0' if not in Train
		public Boolean bBiDi { get; private set; }			//	Loco sends RailCom
		public bool bZACK { get; private set; }				//	Loco sends ZACK
		public bool bDirCmd { get; private set; }			//	Last Direction Command
		public bool bDirAck { get; private set; }			//	Actuall Direction send to Track
		public bool bSpeedZ { get; private set; }			//	Loco Speed Zero
		public bool bStopE { get; private set; }			//	Loco E-Stop
		public byte u08SxEW { get; private set; }			//	State of Special Function East/West
		public byte u08BiDiEW { get; private set; }			//	Last Rceived BiDi East/West
		public byte u08CtrlSec { get; private set; }		//	Seconds since last Control Command (Speed/Fx)
		public UInt16 u16MasterNId { get; private set; }	//	
		#endregion
		#region Create
		public typeZ20MsgLocoState()
		{
		}
		public typeZ20MsgLocoState(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);			//	00 ... 02:	Loco NId
			u08Train = _tMsg.u08Data[2];
			bBiDi = (_tMsg.u08Data[3] & 0x01) != 0;
			bZACK = (_tMsg.u08Data[3] & 0x02) != 0;
			bDirCmd = (_tMsg.u08Data[3] & 0x04) != 0;
			bDirAck = (_tMsg.u08Data[3] & 0x08) != 0;
			u08SxEW = (byte)((_tMsg.u08Data[3] >> 5) & 0x03);
			bSpeedZ = (_tMsg.u08Data[3] & 0x80) != 0;
			bStopE = (_tMsg.u08Data[4] & 0x01) != 0;
			u08BiDiEW = (byte)((_tMsg.u08Data[4] >> 1) & 0x0F);
			//
			u08CtrlSec = _tMsg.u08Data[5];
			u16MasterNId = _tMsg.DataU16Get(6);       //	06 ... 07:	Last Master
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg Mode		[0x02.x02]
	public class typeZ20MsgLocoMode
	{
		#region Properties
		public UInt16 u16NId { get; private set; }						//	Loco Network Id (if DCC, Adress of Loco)
		public enumTrkFmt eTrackFmt { get; set; } = enumTrkFmt.DCC;		//	Loco Track Format
		public enumTrkSteps eTrackSteps { get; set; } = enumTrkSteps.Max0128;	//	Loco Speed Steps
		public byte u08MaxFx { get; private set; }						//	Maximal possible Functions
		#endregion
		#region Create
		public typeZ20MsgLocoMode() : base()
		{
		}
		public typeZ20MsgLocoMode(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);								//	00 ... 01:			Loco NId
			eTrackFmt = (enumTrkFmt)(_tMsg.u08Data[2] >> 4);			//	02, High Nibble:	Track Format
			eTrackSteps = (enumTrkSteps)(_tMsg.u08Data[2] & 0x0F);		//	02, Low Nibbel:		Speed Steps
			u08MaxFx = _tMsg.u08Data[3];								//	03:					Max Loco Function
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg Speed	[0x02.0x02]
	public class typeZ20MsgLocoSpeed
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt16 u16SpeedFlags;
		public UInt32 u08RgDiv { get; private set; }
		public UInt16 u16MasterNId { get; private set; }
		#endregion

		#region Create
		public typeZ20MsgLocoSpeed()
		{
		}
		public typeZ20MsgLocoSpeed(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);			//	00 ... 02:	Loco NId
			u16SpeedFlags = _tMsg.DataU16Get(2);    //	03 ... 04:	Speed inkl. Flags
			u08RgDiv = _tMsg.u08Data[5];			//	05:
													//	06:
			u16MasterNId = _tMsg.DataU16Get(2);		//	07 ... 08:	Last Master NId
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg Function	[0x02.0x04]
	public class typeZ20MsgLocoFx
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt16 u16Num { get; private set; }
		public UInt16 u16Val { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgLocoFx()
		{
		}
		public typeZ20MsgLocoFx(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);       //	00 ... 01:	Loco NId
			u16Num = _tMsg.DataU16Get(2);       //	02 ... 03:	Function Num
			u16Val = _tMsg.DataU16Get(4);       //	04 ... 05:	Function Value
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg FxCfg	[0x02.0x08]
	public class typeZ20MsgLocoFxCfg : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt16 u16Num { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgLocoFxCfg() : base()
		{
		}
		public typeZ20MsgLocoFxCfg(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00 ... 01:	Loco NId
			u16Num = _tMsg.DataU16Get(2);       //	02 ... 03:	Function Num
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg Activ	[0x02.0x10]
	public class typeZ20MsgLocoActiv
	{
		#region Properties
		public UInt16 u16MasterNId { get; private set; }
		public UInt16 u16LocoNId { get; private set; }
		public UInt32 u32Mode { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgLocoActiv()
		{
		}
		public typeZ20MsgLocoActiv(typeZ20Msg _tMsg)
		{
			u16MasterNId = _tMsg.u16HdrNId;
			u16LocoNId = _tMsg.DataU16Get(0);   //	00 ... 01:	Loco NId
			u32Mode = _tMsg.DataU32Get(2);      //	02 ... 03:	Function Num
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg Stack	[0x02.0x11]
	public class typeZ20MsgLocoStack
	{
		#region Properties
		public UInt16 u16Max { get; private set; }
		public UInt16[] u16NId { get; private set; }
		public UInt16[] u16Sec { get; private set; }
		public UInt16[] u16Cnt { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgLocoStack()
		{
			u16Max = 0;
			u16NId = new UInt16[1];
			u16Sec = new UInt16[1];
			u16NId[0] = 0;
			u16Sec[0] = 0;
			u16Cnt[0] = 0;
		}
		public typeZ20MsgLocoStack(typeZ20Msg _tMsg)
		{
			u16Max = _tMsg.DataU16Get(0);
			u16NId = new UInt16[u16Max];
			u16Sec = new UInt16[u16Max];
			u16Cnt = new UInt16[u16Max];
			UInt16 u16DataIdx = 2;
			for (int iCnt = 0; iCnt < u16Max; iCnt++)
			{
				u16NId[iCnt] = _tMsg.DataU16Get(u16DataIdx);
				u16DataIdx += 2;
				u16Cnt[iCnt] = _tMsg.DataU16Get(u16DataIdx);
				u16DataIdx += 2;
				u16Sec[iCnt] = _tMsg.DataU16Get(u16DataIdx);
				u16DataIdx += 2;
			}
		}
		#endregion
	}
	#endregion
	//
	#region Loco Msg State	[0x12.x01]
	public class typeZ20MsgLocoStateX
	{
		#region Properties
		public UInt16 u16LocoNId { get; private set; }		//	Loco Network Id (if DCC, Adress of Loco)
		public UInt16 u16Type { get; private set; }         //	Type of Data
		public UInt16 u16OwnerNId { get; private set; }     //	Fahrzeug 'Eigentümer'
		public UInt16 u16TrainNId { get; private set; }     //	Train NId, sofern Teil eines Zuges
		public UInt32 u32SysTick{ get; private set; }       //	Systick beim versenden der Nachricht
		public UInt32 u32SysSpeed { get; private set; }     //	Schienen Format, Stufen, Speed
		public UInt64 u64FxState { get; private set; }      //	Status für 64 Funktionen
		public UInt32 u32TxTSE { get; private set; }		//	Anzahl bisher gesendeter Schienen Befehle
		public UInt32 u32RxTSE { get; private set; }        //	Anzahl bisher empfangener RailCom Meldungen
		public UInt32 u32PosNIdPin { get; private set; }    //	Last Rceived BiDi East/West
		public UInt32 u32PosTick { get; private set; }		//	Last Rceived BiDi East/West
		public UInt16 u16BiDiSpeed { get; private set; }	//	
		public byte	  u08BiDiTrackV { get; private set; }	//	Schienenspannung
		public byte	  u08BiDiQualS { get; private set; }    //	Quality of Service
		#endregion
		#region Create
		public typeZ20MsgLocoStateX()
		{
		}
		public typeZ20MsgLocoStateX(typeZ20Msg _tMsg)
		{
			u16LocoNId  = _tMsg.DataU16Get(0);           //	00 ... 01:	Loco NId
			u16Type		= _tMsg.DataU16Get(2);           //	02 ... 03:	Version
			u16OwnerNId = _tMsg.DataU16Get(4);           //	04 ... 05:	'Eigentümer'
			u16TrainNId = _tMsg.DataU16Get(6);           //	06 ... 07:	Zug NId, sofern Teil eines Zuges
			u32SysTick  = _tMsg.DataU32Get(8);           //	08 ... 11:	Systick beim Nachricht senden
			u32SysSpeed = _tMsg.DataU32Get(12);          //	12 ... 15:	System Speed
			u64FxState  = _tMsg.DataU64Get(16);          //	16 ... 23:	System Funktions Stati
			u32TxTSE	= _tMsg.DataU32Get(24);          //	24 ... 27:	Anzahl bisher gesendeter Schienen Befehle
			u32RxTSE	= _tMsg.DataU32Get(28);          //	28 ... 31:	Anzahl bisher empfangener RailCom Meldungen
			u32PosNIdPin = _tMsg.DataU32Get(32);         //	28 ... 31:	Fahrzeug Position
			u32PosTick	 = _tMsg.DataU32Get(36);         //	36 .. 39:	Zeitpunkt der letzten Positionsmeldung
			u16BiDiSpeed = _tMsg.DataU16Get(40);         //	40 .. 41:	Aktuelle RailCom Speed
			u08BiDiTrackV = _tMsg.u08Data[42];           //	42:			Schienen Spannung
			u08BiDiQualS  = _tMsg.u08Data[43];           //	42:			Quality of Service
		}
		#endregion
	}
	#endregion
}
