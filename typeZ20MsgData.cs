using System;
using System.Reflection.Emit;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	#region Data Msg Group Count [0x17.x01]
	public class typeZ20MsgDataGrpCnt
	{
		#region Properties
		public UInt16 u16Group { get; private set; }
		public UInt16 u16Count { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgDataGrpCnt()
		{
		}
		public typeZ20MsgDataGrpCnt(typeZ20Msg _tMsg)
		{
			u16Group = _tMsg.DataU16Get(0);               //	02 ... 03:	Data Idx
			u16Count = _tMsg.DataU16Get(2);               //	00 ... 01:	Data NId
		}
		#endregion
	}
	#endregion
	#region Data Msg List By Idx [0x17.x01]
	public class typeZ20MsgDataListIdx
	{
		#region Properties
		public bool bMsgX { get; private set; } = false;
		public UInt16 u16DataIdx { get; private set; }
		public UInt16 u16DataNId { get; private set; }
		public UInt32 u32DataVal { get; private set; } = 0;
		public enumTrkFmt eTrkFmt { get; private set; } = enumTrkFmt.Unknown;
		public enumTrkSteps eSteps { get; private set; } = enumTrkSteps.Unknown;
		public bool f01EStop { get; private set; } = false;
		public Boolean f01DirCmd { get; private set; } = false;
		public bool f01DirAck { get; private set; }
		public UInt16 u16Speed { get; private set; } = 0;
		public byte u08EastWest { get; private set; } = 0;
		public bool f01BiDi { get; private set; } = false;
		public bool f01ZACK { get; private set; } = false;
		public byte u08CabEW { get; private set; } = 0;
		public bool f01SxMan { get; private set; } = false;
		public bool f01Train { get; private set; } = false;
		public bool f01Deleted { get; private set; } = false;
		//----------------------------------------------------
		public byte u08FxMax { get; private set; } = 0;
		public byte u08AxMax { get; private set; } = 0;
		public UInt16 u16TrainNId { get; private set; }
		public UInt32 u32DataVal1 { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgDataListIdx()
		{
		}
		public typeZ20MsgDataListIdx(typeZ20Msg _tMsg)
		{
			u16DataIdx = _tMsg.DataU16Get(0);					//	00 ... 01:	Data Idx
			u16DataNId = _tMsg.DataU16Get(2);					//	02 ... 03:	Data NId
			u32DataVal = _tMsg.DataU32Get(4);					//	04 ... 07:	Data Val
			if((u16DataNId >= (UInt16)eDataGrpNId.LocoDCCMin) &&
				(u16DataNId <= (UInt16)eDataGrpNId.LocoDCCMax))
			{
				eTrkFmt = (enumTrkFmt)(u32DataVal & 0x0F);
				eSteps = (enumTrkSteps)((u32DataVal >> 4) & 0x0F);
				f01DirCmd = ((u32DataVal >> 8) & 0x01) == 1;
				f01DirAck = ((u32DataVal >> 9) & 0x01) == 1;
				u16Speed = (UInt16)((u32DataVal >> 10) & 0x3FF);
				u08EastWest = (byte)((u32DataVal >> 20) & 0x03);
				//
				f01Deleted = ((u32DataVal >> 31) & 0x01) == 1;
				//
				if(_tMsg.iSize > 8)
				{
					bMsgX = true;
					u08FxMax = _tMsg.u08Data[8];
					u08AxMax = _tMsg.u08Data[9];
					u16TrainNId = _tMsg.DataU16Get(10);              //	02 ... 03:	Data Idx
				}
			}
		}
		#endregion
	}
	#endregion
	#region Data Msg List By NId [0x17.x02]
	public class typeZ20MsgDataListNId
	{
		#region Properties
		public UInt16 u16DataIdx { get; private set; }
		public UInt16 u16DataNId { get; private set; }
		public UInt32 u32DataVal { get; private set; }
		public enumTrkFmt eTrkFmt { get; private set; }
		public enumTrkSteps eSteps { get; private set; }
		public bool f01EStop { get; private set; }
		public Boolean f01DirCmd { get; private set; }
		public bool f01DirAck { get; private set; }
		public UInt16 u16Speed { get; private set; }
		public byte u08EastWest { get; private set; }
		public bool f01BiDi { get; private set; }
		public bool f01ZACK { get; private set; }
		public byte u08CabEW { get; private set; }
		public bool f01SxMan { get; private set; }
		public bool f01Train { get; private set; }
		public bool f01Deleted { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgDataListNId()
		{
		}
		public typeZ20MsgDataListNId(typeZ20Msg _tMsg)
		{
			u16DataIdx = _tMsg.DataU16Get(2);               //	02 ... 03:	Data Idx
			u16DataNId = _tMsg.DataU16Get(0);               //	00 ... 01:	Data NId
			u32DataVal = _tMsg.DataU32Get(4);				//	00 ... 01:	Data Val

			if ((u16DataNId >= (UInt16)eDataGrpNId.LocoDCCMin) &&
				(u16DataNId <= (UInt16)eDataGrpNId.LocoDCCMax))
			{
				eTrkFmt = (enumTrkFmt)(u32DataVal & 0x0F);
				eSteps = (enumTrkSteps)((u32DataVal >> 4) & 0x0F);
				f01DirCmd = ((u32DataVal >> 8) & 0x01) == 1;
				f01DirAck = ((u32DataVal >> 9) & 0x01) == 1;
				u16Speed = (UInt16)((u32DataVal >> 10) & 0x3FF);
				u08EastWest = (byte)((u32DataVal >> 20) & 0x03);
				//
				f01Deleted = ((u32DataVal >> 31) & 0x01) == 1;
			}
		}
		#endregion
	}
	#endregion
	#region Data Msg ValueX	[0x17.x08]
	public class typeZ20MsgDataValueX : typeZ20Msg
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; }
		public UInt16 u16DataIdx { get; private set; }
		public UInt32 u32DataVal { get; private set; }
		public UInt32 u32Flags { get; private set; }
		public UInt32 u32Train { get; private set; }
		public UInt16 u16Vendor { get; private set; }
		public UInt32 u32DecoderId { get; private set; }
		public UInt16 u16DecoderTyp { get; private set; }
		public UInt32 u32SoundCode { get; private set; }
		public byte u08TrackFmt { get; private set; }
		public byte u08FxMax { get; private set; }
		//---------------------------------------------
		public UInt16 u16TrkTxCnt { get; private set; }
		public UInt16 u16TrkRxCnt { get; private set; }
		public UInt16 u16TrkRxFlag { get; private set; }
		//---------------------------------------------
		public UInt16 u16OwnerNId { get; private set; }
		public UInt32 u32OwnerTick { get; private set; }
		public UInt16 u16TSE2NId { get; private set; }
		public UInt32 u32TSE2Tick { get; private set; }
		//---------------------------------------------
		public UInt16 u16Speed { get; private set; }
		public UInt32 u32FxVal { get; private set; }
		public UInt32 u32SxVal { get; private set; }
		//---------------------------------------------
		public UInt16 u16BiDiSpeedVal { get; private set; }
		public UInt32 u32BiDiSpeedTick { get; private set; }

		#endregion
		#region Create
		public typeZ20MsgDataValueX()
		{
		}
		public typeZ20MsgDataValueX(typeZ20Msg _tMsg)
		{
			u16DataNId = _tMsg.DataU16Get(0);               //	00 ... 01:	Data NId
			u16DataIdx = _tMsg.DataU16Get(2);               //	02 ... 03:	Data Idx
			u32DataVal = _tMsg.DataU32Get(4);               //	02 ... 03:	Data Val
			switch (u16DataIdx)
			{
				case 0:
				{
					u16Vendor = _tMsg.DataU16Get(4);        //	04 ... 05:	RCN218 Hersteller Kennung
					u32DecoderId = _tMsg.DataU32Get(6);     //	06 ... 09:	RCN218 Dekoder UId
					u16DecoderTyp = _tMsg.DataU16Get(10);   //	10 ... 11:	ZIMO Decoder Type
					u32SoundCode = _tMsg.DataU32Get(12);    //	12 ... 15:	ZIMO Sound Project
					u08TrackFmt = _tMsg.u08Data[16];        //	16:			Schienen Format
					u08FxMax = _tMsg.u08Data[17];           //	17:			Anzahl 'simpel' Funktionen
					u16TrkTxCnt = _tMsg.DataU16Get(18);     //	18 ... 19:	Anzahl Track Commands
					u16TrkRxCnt = _tMsg.DataU16Get(20);     //	20 ... 21:	Anzahl RailCom Rx
					u16TrkRxFlag = _tMsg.DataU16Get(22);    //	22 ... 23:	RailCom Rx Flags
															//
					u16OwnerNId = _tMsg.DataU16Get(24);     //	20 ... 25:	Aktueller 'Besitzer'
					u32OwnerTick = _tMsg.DataU16Get(26);    //	26 ... 29:	Tick 'Besitzmeldung'
					u16TSE2NId = _tMsg.DataU16Get(30);      //	30 ... 31:	TSE RüF Prio
					u32TSE2Tick = _tMsg.DataU16Get(32);     //	32 ... 35:	TSE RüF Prio
															//
					u16Speed = _tMsg.DataU16Get(36);        //	36 ... 37:	Aktuelle Speed
					u32FxVal = _tMsg.DataU32Get(38);        //	38 ... 41:	Funktionsstatus
					u32SxVal = _tMsg.DataU32Get(42);        //	42 ... 45:	Special Functions
															//				00..03: Rangier Div
															//				04..05:	Man
															//	46 ...		32xAnalog (Num/Val)

				}
				break;
				case 1:
				{
					u32Flags = _tMsg.DataU32Get(6);         //	06 ... 09:	Object Flags
					u32Train = _tMsg.DataU32Get(10);        //	10 ... 13:	Object Train
					u16Vendor = _tMsg.DataU16Get(14);       //	14 ... 15:	RCN218 Hersteller Kennung
					u32DecoderId = _tMsg.DataU32Get(16);    //	16 ... 19:	RCN218 Dekoder UId
					u16DecoderTyp = _tMsg.DataU16Get(20);   //	20 ... 21:	ZIMO Decoder Type
					u32SoundCode = _tMsg.DataU32Get(22);    //	22 ... 25:	ZIMO Sound Project
					u08TrackFmt = _tMsg.u08Data[26];        //	26:			Schienen Format
					u08FxMax = _tMsg.u08Data[27];           //	27:			Anzahl 'simpel' Funktionen
					u16TrkTxCnt = _tMsg.DataU16Get(28);     //	28 ... 29:	Anzahl Track Commands
					u16TrkRxCnt = _tMsg.DataU16Get(30);     //	30 ... 31:	Anzahl RailCom Rx
					u16TrkRxFlag = _tMsg.DataU16Get(32);    //	32 ... 33:	RailCom Rx Flags
															//
					u16OwnerNId = _tMsg.DataU16Get(34);     //	34 ... 35:	Aktueller 'Besitzer'
					u32OwnerTick = _tMsg.DataU16Get(36);    //	36 ... 39:	Tick 'Besitzmeldung'
					u16TSE2NId = _tMsg.DataU16Get(40);      //	40 ... 41:	TSE RüF Prio
					u32TSE2Tick = _tMsg.DataU16Get(42);     //	42 ... 45:	TSE RüF Prio
															//
					u16Speed = _tMsg.DataU16Get(46);        //	46 ... 47:	Aktuelle Speed
					u32FxVal = _tMsg.DataU32Get(48);        //	48 ... 51:	Funktionsstatus
					u32SxVal = _tMsg.DataU32Get(52);        //	52 ... 55:	Special Functions
															//				00..03: Rangier Div
															//				04..05:	Man
				}
				break;
			}
		}
		#endregion
	}
	#endregion
	#region Data Msg ValueX	[0x17.x08]
	public class typeZ20MsgDataNameX : typeZ20Msg
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; } = 0;
		public UInt16 u16NameType { get; private set; } = 0xFFFF;
		public UInt32 u32DataVal1 { get; private set; } = 0;
		public UInt32 u32DataVal2 { get; private set; } = 0;
		public UInt32 u32DataVal3 { get; private set; } = 0;
		public string strName { get; private set; } = string.Empty;

		#endregion
		#region Create
		public typeZ20MsgDataNameX()
		{
		}
		public typeZ20MsgDataNameX(typeZ20Msg _tMsg)
		{
			u16DataNId  = _tMsg.DataU16Get(0);			        //	00 ... 01:	Data NId
			u16NameType = _tMsg.DataU16Get(2);		            //	02 ... 03:	Data Idx
			u32DataVal1 = _tMsg.DataU32Get(4);					//	04 ... 07:	Data Val
			u32DataVal2 = _tMsg.DataU32Get(8);				    //	08 ... 11:	Data Val
			u32DataVal3 = _tMsg.DataU32Get(12);			        //	12 ... 15:	Data Val
																//
			strName = _tMsg.DataStringGet(16, _tMsg.iSize-16);	//	12 ... 43:	Fahrzeug Name
			if (strName == null)
			{
				strName = string.Empty;
			}
		}
		#endregion
	}
	#endregion

	#region Data Msg List By X-Idx [0x17.x01]
	public class typeZ20MsgDataListIdxX
	{
		#region Properties
		public UInt16 u16DataIdx { get; private set; }
		public UInt16 u16DataNId { get; private set; }
		public UInt32 u32DataVal { get; private set; }
		public enumTrkFmt eTrkFmt { get; private set; }
		public enumTrkSteps eSteps { get; private set; }
		public bool f01EStop { get; private set; }
		public Boolean f01DirCmd { get; private set; }
		public bool f01DirAck { get; private set; }
		public UInt16 u16Speed { get; private set; }
		public byte u08EastWest { get; private set; }
		public bool f01BiDi { get; private set; }
		public bool f01ZACK { get; private set; }
		public byte u08CabEW { get; private set; }
		public bool f01SxMan { get; private set; }
		public bool f01Train { get; private set; }
		public bool f01Deleted { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgDataListIdxX()
		{
		}
		public typeZ20MsgDataListIdxX(typeZ20Msg _tMsg)
		{
			if (_tMsg.eCmd == Lib5Z20Enums.eNetCmd.ItemIdx)
			{
				u16DataIdx = _tMsg.DataU16Get(0);               //	02 ... 03:	Data Idx
				u16DataNId = _tMsg.DataU16Get(1);               //	00 ... 01:	Data NId
			}
			if (_tMsg.eCmd == Lib5Z20Enums.eNetCmd.ItemNId)
			{
				u16DataIdx = _tMsg.DataU16Get(2);               //	02 ... 03:	Data Idx
				u16DataNId = _tMsg.DataU16Get(0);               //	00 ... 01:	Data NId
			}
			u32DataVal = _tMsg.DataU32Get(4);                   //	00 ... 01:	Data Val
			if ((u16DataNId >= (UInt16)eDataGrpNId.LocoDCCMin) &&
				(u16DataNId <= (UInt16)eDataGrpNId.LocoDCCMax))
			{
				eTrkFmt = (enumTrkFmt)(u32DataVal & 0x0F);
				eSteps = (enumTrkSteps)((u32DataVal >> 4) & 0x0F);
				f01DirCmd = ((u32DataVal >> 8) & 0x01) == 1;
				f01DirAck = ((u32DataVal >> 9) & 0x01) == 1;
				u16Speed = (UInt16)((u32DataVal >> 10) & 0x3FF);
				u08EastWest = (byte)((u32DataVal >> 20) & 0x03);
				//
				f01Deleted = ((u32DataVal >> 31) & 0x01) == 1;
			}
		}
		#endregion
	}
	#endregion
	#region Data Msg Image [0x17.0xnn]
	public class typeZ20MsgDataImage
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; }
		public UInt32 u32ImgType { get; private set; }
		public UInt32 u32ImgCode { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgDataImage()
		{
		}
		public typeZ20MsgDataImage(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
	#region Data Msg SpeedTabX	[0x17.x08]
	public class typeZ20MsgDataSpeedTab : typeZ20Msg
	{
		#region Properties
		public UInt16 u16SrcNId { get; private set; }
		public UInt16 u16ObjNId { get; private set; }
		public byte u08TabIdx { get; private set; }
		public byte u08Count { get; private set; }          //	Anzahl Elemente
		public uint[] u16Level { get; private set; } = new uint[16];
		public uint[] u16Speed { get; private set; } = new uint[16];
		#endregion
		#region Create
		public typeZ20MsgDataSpeedTab()
		{
		}
		public typeZ20MsgDataSpeedTab(typeZ20Msg _tMsg)
		{
			
			u16SrcNId = _tMsg.DataU16Get(0);					//	00 ... 01:	NId der 'Quelle'
			u16ObjNId = _tMsg.DataU16Get(2);					//	02 ... 03:	NId des Objects
			u08TabIdx = _tMsg.u08Data[4];                       //	02 ... 03:	Data Val
			u08Count  = _tMsg.u08Data[5];                       //	02 ... 03:	Data Val
			//
			UInt16 u16BytePos = 0;
			switch (u08TabIdx)
			{
				case 0:
				{
					u16BytePos = 6;
					for (int iCnt = 0; iCnt < u08Count; iCnt++)
					{
						u16Level[iCnt] = _tMsg.DataU16Get(u16BytePos);
						u16BytePos += 2;
						u16Speed[iCnt] = _tMsg.DataU16Get(u16BytePos);
						u16BytePos += 2;
					}
				}
				break;
			}
		}
		#endregion
	}
	#endregion
}
