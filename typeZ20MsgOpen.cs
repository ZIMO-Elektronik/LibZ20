using System;
using System.Collections;
using System.Diagnostics;

namespace Lib5Z20
{
	public class typeZ20MsgPing
	{
		#region Properties
		public UInt16 u16NId { get; private set; }
		public UInt32 u32UId { get; private set; }
		public UInt16 u16Typ { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgPing()
		{
		}
		public typeZ20MsgPing(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.u16HdrNId;
			u32UId = _tMsg.DataU32Get(0);           //	00 ... 03
			u16Typ = _tMsg.DataU16Get(4);           //	04 ... 05
		}
		#endregion
	}
	#region Message Open
	public class typeZ20MsgOpen
	{
		#region Properties
		public UInt32 u32Options { get; private set; }
		public bool bLongMsg
		{
			get
			{
				BitArray b = new BitArray(BitConverter.GetBytes(u32Options));
				return (b[0]);
			}
		}
		public bool bLostStop
		{
			get
			{
				BitArray b = new BitArray(BitConverter.GetBytes(u32Options));
				return (b[23]);
			}
		}
		public bool bDebug
		{
			get
			{
				BitArray b = new BitArray(BitConverter.GetBytes(u32Options));
				return (b[24]);
			}
		}
		public UInt32 u32Verion
		{
			get
			{
				return ((u32Options >> 28) & 0x000F);
			}
		}
		public UInt32 u32AppCode { get; private set; }
		public UInt32 u32SysIP { get; private set; }
		public UInt16 u16SysNId { get; private set; }
		public UInt32 u32SysUId { get; private set; }
		public string sSysName { get; private set; }
		public UInt32 u32SysTick { get; private set; }
		public UInt32 u32HardWareBuild { get; private set; }
		public UInt32 u32SoftWareBuild { get; private set; }
		public UInt32 u32SoftWareDate { get; private set; }
		public UInt32 u32SoftWareTime { get; private set; }
		public UInt32 u32MiWiBuildHw { get; private set; }
		public UInt32 u32MiWiBuildSw { get; private set; }
		public UInt16 u16MiWiChannel { get; private set; }
		public UInt32 u32ClockDate { get; private set; }
		public UInt32 u32ClockTime { get; private set; }
		public UInt32 u32TotalTicks { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgOpen()
		{
		}
		public typeZ20MsgOpen(typeZ20Msg _tMsg)
		{
			u32Options = _tMsg.DataU32Get(0);           //	00 ... 03
			u32AppCode = _tMsg.DataU32Get(4);           //	04 ... 07
			u32SysIP = _tMsg.DataU32Get(8);             //	08 ... 11
			u16SysNId = _tMsg.DataU16Get(12);           //	12 ... 13
			u32SysUId = _tMsg.DataU32Get(12);           //	14 ... 17
			sSysName = System.Text.Encoding.UTF8.GetString(_tMsg.u08Data, 18, 32);
			Debug.WriteLine(sSysName);
			u32SysTick = _tMsg.DataU32Get(50);          //	50 ... 53
			u32HardWareBuild = _tMsg.DataU32Get(54);    //	54 ... 57
			u32SoftWareBuild = _tMsg.DataU32Get(58);    //	58 ... 61
			u32SoftWareDate = _tMsg.DataU32Get(62);     //	62 ... 65
			u32SoftWareTime = _tMsg.DataU32Get(66);     //	66 ... 69
			if (u32Verion == 1)
			{
				u32MiWiBuildHw = _tMsg.DataU32Get(70);      //	70 ... 73
				u32MiWiBuildSw = _tMsg.DataU32Get(74);      //	74 ... 77
				u16MiWiChannel = _tMsg.DataU16Get(78);      //	78 ... 79
				u32ClockDate = _tMsg.DataU32Get(80);        //	80 ... 83
				u32ClockTime = _tMsg.DataU32Get(84);        //	84 ... 87
				u32TotalTicks = _tMsg.DataU32Get(88);       //	84 ... 87
			}
		}
		#endregion
	}
	#endregion
}
