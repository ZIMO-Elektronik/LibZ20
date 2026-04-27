using System;
using System.Collections;

namespace Lib5Z20
{
	#region Info Msg Power		[0x18.x00]
	public class typeZ20MsgInfoPower
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; }
		public bool bTrack1Off { get; private set; }
		public bool bTrack1ErrMinU { get; private set; }
		public bool bTrack1ErrMaxI { get; private set; }
		public UInt16 u16Track1Mode { get; private set; }
		
		public UInt16 u16Track1U { get; private set; }
		public UInt16 u16Track1I { get; private set; }
		//
		public bool bTrack2Off { get; private set; }
		public bool bTrack2ErrMinU { get; private set; }
		public bool bTrack2ErrMaxI { get; private set; }
		public UInt16 u16Track2Mode { get; private set; }
		public UInt16 u16Track2U { get; private set; }
		public UInt16 u16Track2I { get; private set; }
		//
		public UInt16 u16Pwr32I { get; private set; }
		public UInt16 u16Pwr12I { get; private set; }
		public UInt16 u16SourceV { get; private set; }
		public UInt16 u16SysTemp { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgInfoPower()
		{
		}
		public typeZ20MsgInfoPower(typeZ20Msg _tMsg)
		{
			u16DataNId = _tMsg.DataU16Get(0);           //	00 ... 01:	Data NId
			//
			u16Track1Mode = _tMsg.DataU16Get(2);        //	02 ... 03:	Track 1 Betriebsmode
			u16Track1U = _tMsg.DataU16Get(4);			//	04 ... 05:	Track 1 Spannung
			u16Track1I = _tMsg.DataU16Get(6);           //	06 ... 07:	Track 1 Strom Mittelwert
			//
			u16Track2Mode = _tMsg.DataU16Get(8);
			u16Track2U = _tMsg.DataU16Get(10);			//	10 ... 11:	Track 2 Spannung
			u16Track2I = _tMsg.DataU16Get(12);			//	12 ... 13:	Track 2 Strom Mittelwert
			//
			u16Pwr32I = _tMsg.DataU16Get(14);			//	14 ... 15:	Strom 32V
			u16Pwr12I = _tMsg.DataU16Get(16);           //	16 ... 17:	Strom 12V
			u16SourceV = _tMsg.DataU16Get(18);			//	18 ... 19:	Eingangsspannung
			u16SysTemp = _tMsg.DataU16Get(20);			//	20 ... 21:	System Temperatur
		}
		#endregion
	}
	#endregion
	#region Info Msg BiDi Data 16	[0x08.x05]
	public class typeZ20MsgInfoBiDi16
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgInfoBiDi16()
		{
		}
		public typeZ20MsgInfoBiDi16(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
	#endregion
	#region Info Msg BiDi Data 32	[0x08.x05]
	public class typeZ20MsgInfoBiDi32
	{
		#region Properties
		public UInt16 u16DataNId { get; private set; }
		public UInt16 u16Type { get; private set; }
		public UInt32 u32Data { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgInfoBiDi32()
		{
			u16DataNId = UInt16.MaxValue;
			u16Type = 0;
			u32Data = 0;
		}
		public typeZ20MsgInfoBiDi32(typeZ20Msg _tMsg)
		{
			u16DataNId = _tMsg.DataU16Get(0);           //	00 ... 01:	Data NId
			u16Type = _tMsg.DataU16Get(2);              //	02 ... 03:	BiDi Info Type
			u32Data = _tMsg.DataU32Get(4);              //	04 ... 07:	BiDi Data
		}
		#endregion
	}
	#endregion
	//
	#region Info Msg Data		[0x08.x08]
	public class typeZ20MsgInfoData
	{
		#region Properties
		public UInt16 u16NId { get; private set; } = UInt16.MaxValue;
		public UInt16 u16Typ { get; private set; } = 0;
		public UInt32 u32Val { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgInfoData()
		{
			u16NId = UInt16.MaxValue;
		}
		public typeZ20MsgInfoData(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);           //	00 ... 01:	Data NId
			u16Typ = _tMsg.DataU16Get(2);           //	02 ... 03:	Info Type
			u32Val = _tMsg.DataU32Get(4);           //	04 ... 07:	Info Value
		}
		#endregion
	}
	#endregion
	#region Info Msg Data		[0x08.x0A]
	public class typeZ20MsgBinTagValue
	{
		#region Properties
		public UInt16 u16NId { get; private set; } = UInt16.MaxValue;
		public UInt32 u32Tag { get; private set; } = 0;
		public UInt16 u16Val { get; private set; } = 0;
		#endregion
		#region Create
		public typeZ20MsgBinTagValue()
		{
			u16NId = UInt16.MaxValue;
		}
		public typeZ20MsgBinTagValue(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);			//	00 ... 01:	Data NId
			u32Tag = _tMsg.DataU32Get(2);			//	02 ... 05:	Bin Tag
			u16Val = _tMsg.DataU16Get(6);			//	06 ... 07:	Value
		}
		#endregion
	}
	#endregion
}
