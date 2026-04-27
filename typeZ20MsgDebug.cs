using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	#region Data Msg List By Idx [0x17.x01]
	public class typeZ20MsgDebugTSE
	{
		#region Properties
		public UInt32 u32Tick { get; private set; }
		public UInt32 u32ObjUId { get; private set; }
		public UInt32 u32Info { get; private set; }
		public UInt16[] u16Loco { get; private set; }
		public UInt16[] u16Cnt { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgDebugTSE()
		{
			u16Loco = new UInt16[128];
			u16Cnt = new UInt16[128];
		}
		public typeZ20MsgDebugTSE(typeZ20Msg _tMsg)
		{
			u32Tick = _tMsg.DataU32Get(0);
			u32ObjUId = _tMsg.DataU32Get(4);
			u32Info = _tMsg.DataU32Get(8);
			//
			u16Loco = new UInt16[128];
			u16Cnt = new UInt16[128];
			//
			int iOffest;
			for (int iCnt = 0; iCnt < 128; iCnt++)
			{
				iOffest = ((iCnt * 4) + 12);
				u16Cnt [iCnt] = _tMsg.u08Data[(iOffest + 1)];
				u16Loco[iCnt] = _tMsg.DataU16Get((UInt16)(iOffest + 2));
			}
		}
		#endregion
	}
	#endregion
}
