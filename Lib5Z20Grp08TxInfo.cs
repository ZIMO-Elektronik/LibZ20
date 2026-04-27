using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20Grp08TxInfo
	{
		#region [0x08.0x08.R]:	Info Modul Request
		public typeZ20Msg InfoDataReq(UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp08Info,
											 Lib5Z20Enums.eNetCmd.InfoData,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt32(_u32Tag);
			return (tMsg);
		}
		public void InfoDataReq(bool _bWait, UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = InfoDataReq(_u16SrcNId, _u32Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x08.0x08.C]:	Info Modul Command
		public typeZ20Msg InfoDataCmd(UInt16 _u16NId, Lib5Z20Enums.eModulInfo _eType, UInt32 _u32Value)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp08Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagValue,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_eType);
			tMsg.AddInt32((UInt32)_u32Value);
			return (tMsg);
		}
		public void InfoDataCmd(bool _bWait, UInt16 _u16NId, Lib5Z20Enums.eModulInfo _eType, UInt32 _u32Value)
		{
			if (_u16NId == 0)
			{
				_u16NId = LibNetZ20.u16ZsNId;
			}
			typeZ20Msg tMsg = InfoDataCmd(_u16NId, _eType, _u32Value);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x18.0x0A]:	Abfrage Info Value
		public typeZ20Msg InfoBinTagReq(UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp18Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagValue,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt32((UInt32)_u32Tag);
			return (tMsg);
		}
		public void InfoBinTagReq(bool _bWait, UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = InfoBinTagReq(_u16SrcNId, _u32Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x18.0x0A]:	Command Info Value
		public typeZ20Msg InfoValueCmd(UInt16 _u16SrcNId, UInt32 _u32Tag, UInt16 _u16Value)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp18Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagValue,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt32((UInt32)_u32Tag);
			tMsg.AddInt16((UInt16)_u16Value);
			return (tMsg);
		}
		public void InfoValueCmd(bool _bWait, UInt16 _u16SrcNId, UInt32 _u32Tag, UInt16 _u16Value)
		{
			typeZ20Msg tMsg = InfoValueCmd(_u16SrcNId, _u32Tag, _u16Value);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x18.0x0C]:	Abfrage Info Docu
		public typeZ20Msg InfoDocuReq(UInt16 _u16SrcNId, UInt16 _u16Cmd, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp18Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagDocu,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16Cmd);
			tMsg.AddInt32((UInt32)_u32Tag);
			return (tMsg);
		}
		public void ReqInfoDocu(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16Cmd, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = InfoDocuReq(_u16SrcNId, _u16Cmd, _u32Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}

