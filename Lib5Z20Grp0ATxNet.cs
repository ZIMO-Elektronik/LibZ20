using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20Grp0ATxNet
	{
		#region Tx:	 Net Management Open Cmd
		public typeZ20Msg PortOpen()
		{
			typeZ20Msg cMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp0ANet,
											 Lib5Z20Enums.eNetCmd.Net_Open,
											 Lib5Z20Enums.eNetMode.Cmd);
			return (cMsg);
		}
		public void PortOpen(bool _bWait)
		{
			typeZ20Msg tMsg = PortOpen();
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		public typeZ20Msg PortOpen(UInt32 _iOption, UInt32 _iAppCode, string _sName)
		{
			typeZ20Msg cMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp1ANet,
											 Lib5Z20Enums.eNetCmd.Net_Open,
											 Lib5Z20Enums.eNetMode.Cmd);
			cMsg.AddInt32(_iOption);
			cMsg.AddInt32(_iAppCode);
			cMsg.AddString(_sName, 32);
			return (cMsg);
		}
		public void PortOpen(bool _bWait, UInt32 _iOption, UInt32 _iAppCode, string _sName)
		{
			typeZ20Msg cMsg = PortOpen(_iOption, _iAppCode, _sName);
			LibNetZ20.MsgTx(_bWait, cMsg);
		}
		#endregion
		#region Tx:	 Net Management Close Cmd
		public typeZ20Msg PortClose()
		{
			typeZ20Msg cMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp0ANet,
											 Lib5Z20Enums.eNetCmd.Net_Close,
											 Lib5Z20Enums.eNetMode.Cmd);
			return (cMsg);
		}
		public Boolean PortClose(Boolean _bWait)
		{
			typeZ20Msg cMsg = PortClose();
			return (LibNetZ20.MsgTx(_bWait, cMsg));
		}
		#endregion
		//
		#region Tx:	 Net Option
		public typeZ20Msg OptionCmd(UInt16 _iNId, UInt16 _iType, UInt32 _iOption)
		{
			typeZ20Msg cMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp0ANet,
											 Lib5Z20Enums.eNetCmd.Net_Option,
											 Lib5Z20Enums.eNetMode.Cmd);
			cMsg.AddInt16((UInt16)_iNId);
			cMsg.AddInt16((UInt16)_iType);
			cMsg.AddInt32((UInt32)_iOption);
			return (cMsg);
		}
		public void OptionCmd(bool _bWait, UInt16 _iNId, UInt16 _iType, UInt32 _iOption)
		{
			typeZ20Msg cMsg = OptionCmd(_iNId, _iType, _iOption);
			LibNetZ20.MsgTx(_bWait, cMsg);
		}
		#endregion
		//
		#region Tx:	 Connection Alive
		public typeZ20Msg PingEvtTx(UInt16 _u16NId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp0ANet,
											 Lib5Z20Enums.eNetCmd.Net_Ping,
											 Lib5Z20Enums.eNetMode.Evt);
			tMsg.AddInt16((UInt16)_u16NId);
			return (tMsg);
		}
		public void PingEvtTx(bool _bWait, UInt16 _u16NId)
		{
			typeZ20Msg cMsg = PingEvtTx(_u16NId);
			LibNetZ20.MsgTx(_bWait, cMsg);
		}
		#endregion
	}
}
