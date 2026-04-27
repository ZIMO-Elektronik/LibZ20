using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20TxGrp08
	{
		#region [0x18.0x08]:	Abfrage Info Data
		public typeZ20Msg ReqInfoData(UInt16 _u16SrcNId, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp08Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagValue,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16Type);
			return (tMsg);
		}
		public void ReqDataData(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = ReqInfoData(_u16SrcNId, _u16Type);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x18.0x0C]:	Abfrage Info Docu
		public typeZ20Msg ReqInfoBinTagDocu(UInt16 _u16SrcNId, UInt16 _u16Cmd, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp18Info,
											 Lib5Z20Enums.eNetCmd.InfoBinTagDocu,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16Cmd);
			tMsg.AddInt32((UInt32)_u32Tag);
			return (tMsg);
		}
		public void ReqInfoBinTagDocu(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16Cmd, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = ReqInfoBinTagDocu(_u16SrcNId, _u16Cmd, _u32Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
