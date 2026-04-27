using System;
using System.Reflection.Metadata.Ecma335;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	#region TSE Track Mode	[0x06.0x00]
	public class typeZ20MsgTrackMode
	{
		#region Properties
		public UInt16 u16SrcNId { get; private set; } = 0;
		public byte u08PinNum { get; private set; } = 0;
		public byte u08Mode { get; private set; } = 0;
		public UInt16 u16TrackU { get; private set; } = 0;
		public UInt16 u16TrackI { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgTrackMode()
		{
		}
		public typeZ20MsgTrackMode(typeZ20Msg _tMsg)
		{
			u16SrcNId = _tMsg.DataU16Get(0);			//	00 ... 01:	'Quell' Gerät
			u08PinNum = _tMsg.u08Data[2];               //	02:			Anschlussnummer
			u08Mode = _tMsg.u08Data[3];                 //	03:			Anschlussmode
			u16TrackU = _tMsg.DataU16Get(4);            //	04 ... 05:	Spannung
			u16TrackI = _tMsg.DataU16Get(7);            //	04 ... 05:	Strom
		}
		#endregion
	}
	#endregion
	//
	#region TSE Prog Clear	[0x06.0x04]
	public class typeZ20MsgProgClear
	{
		#region Properties
		public UInt16 u16SrcNId { get; private set; } = 0;
		public UInt16 u16ObjNId { get; private set; } = 0;
		public UInt16 u16Options { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgProgClear()
		{
		}
		public typeZ20MsgProgClear(typeZ20Msg _tMsg)
		{
			u16SrcNId = _tMsg.DataU16Get(0);			//	00 ... 01:	'Quell' Gerät
			u16ObjNId = _tMsg.DataU16Get(2);            //	02 ... 03:	Object which Cfg is to Clear
			u16Options = _tMsg.DataU16Get(4);           //	04 ... 05:	Clear Options
		}
		#endregion
	}
	#endregion
	//
	#region TSE Msg Config	[0x06.0x08/0x06.0x09]
	public class typeZ20MsgTrackCfgVal
	{
		#region Properties
		public UInt16 u16SrcNId { get; private set; } = 0;
		public UInt16 u16ObjNId { get; private set; } = 0;
		public UInt32 u32CfgNum { get; private set; } = 0;
		public UInt32 u16CfgNum { get; private set; } = 0;
		public UInt16 u16CfgVal { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgTrackCfgVal()
		{
		}
		public typeZ20MsgTrackCfgVal(typeZ20Msg _tMsg)
		{
			u16SrcNId = _tMsg.DataU16Get(0);           //	00 ... 01:	'Quell' Gerät
			u16ObjNId = _tMsg.DataU16Get(2);           //	02 ... 03:	'Object'
			u32CfgNum = _tMsg.DataU32Get(4);           //	04 ... 07:	Config Num
			u16CfgVal = _tMsg.DataU16Get(8);           //	02 ... 03:	'Object'
		}
		#endregion
	}
	#endregion
	//
	#region TSE Find	[0x06.0x10]
	public class typeZ20MsgTrackFind
	{
		#region Properties
		public UInt16 u16SrcNId { get; private set; } = 0;
		public UInt16 u16Mode { get; private set; } = 0;
		public UInt16 u16LocoNId { get; private set; } = 0;
		public UInt16 u16Tick { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgTrackFind()
		{
		}
		public typeZ20MsgTrackFind(typeZ20Msg _tMsg)
		{
			u16SrcNId = _tMsg.DataU16Get(0);		//	00 ... 01:	'Quell' Gerät
			u16Mode = _tMsg.DataU16Get(2);			//	02 ... 03:	Mode
			u16LocoNId = _tMsg.DataU16Get(4);		//	04 ... 05:	Found NId
			u16Tick = _tMsg.DataU16Get(6);			//	04 ... 05:	Found Tick
		}
		#endregion
	}
	#endregion
	//
	#region RCN218 Control	[0x06.0x11]
	public class typeZ20MsgRCN218Control
	{
		#region Properties
		public UInt16 u16Action { get; private set; } = 0;
		public UInt16 u16Count { get; private set; } = 0;
		public UInt16 u16Ticks { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgRCN218Control()
		{
		}
		public typeZ20MsgRCN218Control(typeZ20Msg _tMsg)
		{
			u16Action = _tMsg.DataU16Get(0);		//	00 ... 01:	RCN 218 Command
			u16Count = _tMsg.DataU16Get(2);			//	02 ... 03:	Widerholungen
			u16Ticks = _tMsg.DataU16Get(4);			//	04 ... 05:	Intervall
		}
		#endregion
	}
	#endregion
	#region RCN218 Result		[0x06.0x12]
	public class typeZ20MsgRCN218Result
	{
		#region Properties
		public byte u08Control { get; private set; } = 0;
		public UInt16 u16Vendor { get; private set; } = 0;
		public UInt16 u16LocoNId { get; private set; } = 0;
		public UInt32 u32DecoderUId { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgRCN218Result()
		{
		}
		public typeZ20MsgRCN218Result(typeZ20Msg _tMsg)
		{
			if(_tMsg.eGrp == eNetGrp.Grp06TSE)
			{
				u08Control = (byte)((_tMsg.u08Data[0] >> 4) & 0x0F);        //	00, High Nibbel:	RCN 218 Control
				u16Vendor = ((ushort)(_tMsg.DataU16Get(0) & 0x0FFF));
				u16LocoNId = _tMsg.DataU16Get(2);
				u32DecoderUId = _tMsg.DataU32Get(4);
			}
			else
			{
				u08Control = _tMsg.u08Data[0];                              //	00:	RCN 218 Control
				//_tMsg.u08Data[0]												01: Flags
				u16LocoNId = _tMsg.DataU16Get(2);                           //	Fahrzeug NId
				u16Vendor = _tMsg.DataU16Get(4);                            //	Hersteller
				u32DecoderUId = _tMsg.DataU32Get(6);
			}

		}
		#endregion
	}
	#endregion
	#region RCN218 Assign		[0x06.0x13]
	public class typeZ20MsgRCN218Assign
	{
		#region Properties
		public UInt16 u16LocoNId { get; private set; } = 0;
		public UInt16 u16VendorNum { get; private set; } = 0;
		public UInt32 u32DecoderUId { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgRCN218Assign()
		{
		}
		public typeZ20MsgRCN218Assign(typeZ20Msg _tMsg)
		{
			u16LocoNId = _tMsg.DataU16Get(0);
			u16VendorNum = _tMsg.DataU16Get(2);
			u32DecoderUId = _tMsg.DataU32Get(4);
		}
		#endregion
	}
	#endregion
	#region Decoder GUI Control	[0x06.0x14]
	public class typeZ20MsgDecoderCtrlGUI
	{
		#region Properties
		public UInt16 u16NId { get; private set; } = 0;
		public enumTrackCtrlGUI eCtrl { get; private set; } = enumTrackCtrlGUI.None;
		public UInt32 u32Data { get; private set; } = 0;
		public byte u08ChunkCRC
		{
			get { return (byte)((u32Data >> 0) & 0xFF); }
		}
		public UInt16 u16ChunkNum
		{
			get { return (UInt16)((u32Data >> 8) & 0xFFFF); }
		}
		public byte u08ChunkMax
		{
			get { return (byte)((u32Data >> 24) & 0xFF); }
		}
		#endregion
		#region Create
		public typeZ20MsgDecoderCtrlGUI(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);
			eCtrl = (enumTrackCtrlGUI)_tMsg.DataU16Get(2);
			u32Data = _tMsg.DataU32Get(4);
		}
		#endregion
	}
	#endregion
	#region Decoder GUI Info	[0x06.0x15]
	public class typeZ20MsgDecoderInfoGUI
	{
		#region Properties
		public UInt16 u16NId { get; private set; } = 0;     //	00..15:	Geräte NId (Zentrale)
		public enumTrackInfoGUI eTyp { get; private set; } = enumTrackInfoGUI.None;
		public byte u08State { get; private set; } = 0;
		public UInt32 u32Data { get; private set; } = 0;
		//
		public byte u08QuadNum
		{
			get { return (u08State); }
		}
		#endregion
		#region Create
		public typeZ20MsgDecoderInfoGUI()
		{
		}
		public typeZ20MsgDecoderInfoGUI(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);
			eTyp = (enumTrackInfoGUI)_tMsg.u08Data[2];
			if (_tMsg.iSize == 8)
			{
				u08State = _tMsg.u08Data[3];
				u32Data = _tMsg.DataU32Get(4);
			}
			if (_tMsg.iSize == 10)
			{
				u08State = _tMsg.u08Data[3];
				u32Data = _tMsg.DataU32Get(4);
			}
		}
		#endregion
	}
	#endregion
	#region Decoder GUI Info	[0x06.0x16]
	public class typeZ20MsgDecoderGuiLog
	{
		#region Properties
		public UInt16 u16NId { get; private set; } = 0;
		public UInt16 u16Idx { get; private set; } = 0;
		public UInt16 u16State { get; private set; } = 0;
		public UInt32 u32Data { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgDecoderGuiLog()
		{
		}
		public typeZ20MsgDecoderGuiLog(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);       //	00.. 15:	Fahrzeug NId
			u16Idx = _tMsg.u08Data[2];			//	16.. 23:	Index
			u16State = _tMsg.u08Data[3];		//	24.. 31:	Status
			u32Data = _tMsg.DataU32Get(4);		//	32.. 63:	Quadruppel Data
		}
		#endregion
	}
	#endregion
	//
	#region TSE Info Lan [0x16.0x02]
	public class typeZ20MsgTrackInfoLan
	{
		//pMsg->uSize		 = 8;
		//pMsg->u16Data[0] = _uNId;						//	Fahrzeug
		//pMsg->u08Data[2] = (_uNum & MAXINT08);			//	Cfg Num
		//pMsg->u08Data[3] = (_uNum >>  8) & MAXINT08;	//	Cfg Num
		//pMsg->u08Data[4] = (_uNum >> 16) & MAXINT08;	//	Cfg Num
		//pMsg->u08Data[5] = (_uInfo & MAXINT08);			//	Cfg Info
		//pMsg->u08Data[6] = (_uInfo >>  8) & MAXINT08;	//	Cfg Info
		#region Properties
		#endregion
		#region Create
		public typeZ20MsgTrackInfoLan()
		{
		}
		public typeZ20MsgTrackInfoLan(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
	//
	#region BiDi Raw Data Channel 0	[0x06.0x1D]
	public class typeZ20MsgBiDiRawDataChannel0
	{
		#region Properties
		#endregion
		#region Create
		public typeZ20MsgBiDiRawDataChannel0()
		{
		}
		public typeZ20MsgBiDiRawDataChannel0(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
	#region BiDi Raw Data Channel 1	[0x06.0x1E]
	public class typeZ20MsgBiDiRawDataChannel1
	{
		#region Properties
		#endregion
		#region Create
		public typeZ20MsgBiDiRawDataChannel1()
		{
		}
		public typeZ20MsgBiDiRawDataChannel1(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
	#region BiDi Raw Data Ack/NAck	[0x06.0x1F]
	public class typeZ20MsgBiDiRawDataAck
	{
		#region Properties
		#endregion
		#region Create
		public typeZ20MsgBiDiRawDataAck()
		{
		}
		public typeZ20MsgBiDiRawDataAck(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
}
