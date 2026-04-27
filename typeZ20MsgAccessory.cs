using System;
using System.Diagnostics;

namespace Lib5Z20
{
	#region Modul Msg State	[0x01.x00]
	public class typeZ20MsgModulState : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt16 u16State { get; private set; }
		public UInt32 u32Data { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgModulState() : base()
		{
		}
		public typeZ20MsgModulState(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00 ... 01:	Modul NId
			u16State = _tMsg.DataU16Get(2);		//	02 ... 03:	Modul State
			u32Data = _tMsg.DataU32Get(4);		//	04 ... 07:	Modul Data
		}
		#endregion
	}
	#endregion
	#region Modul Msg Mode	[0x01.x01]
	public class typeZ20MsgModulMode : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public byte u08Typ { get; private set; }
		public byte u08Pin { get; private set; }
		public UInt16 u16Mode1 { get; private set; }
		public UInt16 u16Mode2 { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgModulMode() : base()
		{
		}
		public typeZ20MsgModulMode(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00 ... 01:	Modul NId
			u08Typ = _tMsg.u08Data[2];			//	02:			Type
			u08Pin = _tMsg.u08Data[3];          //	03:			Pin
			u16Mode1 = _tMsg.DataU16Get(4);     //	04 ... 05:	Pin Mode 1
			u16Mode2 = _tMsg.DataU16Get(6);     //	06 ... 07:	Pin Mode 2
		}
		#endregion
	}
	#endregion
	#region Modul Msg GPIO	[0x01.x02]
	public class typeZ20MsgModulGPIO : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt16 u16Typ { get; private set; }
		public UInt32 u32Data { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgModulGPIO() : base()
		{
		}
		public typeZ20MsgModulGPIO(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00...01:	Modul NId
			u16Typ = _tMsg.DataU16Get(2);		//	02...03:	Type
			u32Data = _tMsg.DataU32Get(4);		//	04...07:	GPIO Data
		}
		#endregion
	}
	#endregion
	#region Modul Msg Pin4	[0x01.x04]
	public class typeZ20MsgModulPin4 : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public byte u08Pin { get; private set; }
		public byte u08Val { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgModulPin4() : base()
		{
		}
		public typeZ20MsgModulPin4(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00...01:	Modul NId
			u08Pin = _tMsg.u08Data[2];			//	02			Pin Nummer
			u08Val = _tMsg.u08Data[3];			//	03:			Pin 'Wert'
		}
		#endregion
	}
	#endregion
	#region Modul Msg Data	[0x01.x05]
	public class typeZ20MsgModulData : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public byte u08Pin { get; private set; }
		public byte u08Typ { get; private set; }
		public UInt32 u32Val { get; private set; }
		public UInt16 u16Val1
		{
			get
			{
				return ((UInt16)((u32Val >> 0) & UInt16.MaxValue));
			}
		}
		public UInt16 u16Val2
		{
			get
			{
				return ((UInt16)((u32Val >> 16) & UInt16.MaxValue));
			}
		}
		#endregion
		#region Create
		public typeZ20MsgModulData() : base()
		{
		}
		public typeZ20MsgModulData(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00...01:	Modul NId
			u08Pin = _tMsg.u08Data[2];          //	02			Pin Nummer
			u08Typ = _tMsg.u08Data[3];          //	03:			Pin Wert Typ
			u32Val = _tMsg.DataU32Get(4);       //	04...07:	Pin Data
		}
		#endregion
	}
	#endregion
	#region Modul Msg Pin6	[0x01.x06]
	public class typeZ20MsgModulPin6 : typeZ20Msg
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public byte u08Pin { get; private set; }
		public byte u08Typ { get; private set; }
		public UInt16 u16Val1 { get; private set; }
		public UInt16 u16Val2 { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgModulPin6() : base()
		{
		}
		public typeZ20MsgModulPin6(typeZ20Msg _tMsg) : base()
		{
			u16NId = _tMsg.DataU16Get(0);       //	00...01:	Modul NId
			u08Pin = _tMsg.u08Data[2];          //	02			Pin Nummer
			u08Typ = _tMsg.u08Data[3];          //	03:			Pin Data Typ
			u16Val1 = _tMsg.DataU16Get(4);       //	04...05:	Pin Value
			u16Val2 = _tMsg.DataU16Get(6);       //	06...07:	Pin Value
		}
		#endregion
	}
	#endregion
}
