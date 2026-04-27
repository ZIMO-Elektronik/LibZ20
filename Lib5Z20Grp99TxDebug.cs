using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20Grp99TxDebug
	{
		#region Tx:	 Debug BiDi Preset
		public typeZ20Msg DebugBiDiPreset(UInt16 _u16Typ, UInt16 _u16NId, UInt16 _u16Pin)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.GrpDebug,
											 Lib5Z20Enums.eNetCmd.DebugBiDiConfig,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16Typ);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Pin);
			return (tMsg);
		}
		public void DebugBiDiPreset(bool _bWait, UInt16 _uTyp, UInt16 _uNId, UInt16 _uPin)
		{
			typeZ20Msg tMsg = DebugBiDiPreset(_uTyp, _uNId, _uPin);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
