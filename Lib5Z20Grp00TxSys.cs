using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Sender for Group 0x00, System State										*/
/*																								*/
/************************************************************************************************/

namespace Lib5Z20
{
	public class Lib5Z20Grp00TxSys
	{
		#region [0x00.0x00]:	Command Track State
		public typeZ20Msg CmdTrackState(UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16Mode)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp00Sys,
											 Lib5Z20Enums.eNetCmd.SysState,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((byte)_u16Pin);
			tMsg.AddInt08((byte)_u16Mode);
			return (tMsg);
		}
		public void CmdTrackState(bool _bWait, UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16Mode)
		{
			if(_u16NId == 0)
			{
				_u16NId = LibNetZ20.u16ZsNId;
			}
			typeZ20Msg tMsg = CmdTrackState(_u16NId, _u16Pin, _u16Mode);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
