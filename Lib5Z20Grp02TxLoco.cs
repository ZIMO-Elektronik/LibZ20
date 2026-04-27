/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x02, Loco Control Datagramms							*/
/*																								*/
/*	Implemented:																				*/
/*	[0x02.0x00] Loco State																		*/
/*	[0x02.0x01] Loco Mode																		*/
/*	[0x02.0x02] Loco Speed																		*/
/*	[0x02.0x04] Loco Function																	*/
/*	[0x02.0x05] Loco Function, Special															*/
/*	[0x02.0x10] Loco Activ																		*/
/*	[0x02.0x11] Loco Stack																		*/
/*																								*/
/*	[0x02.0x00] Loco State																		*/
/*																								*/
/*	Pending:																					*/
/*	[0x02.0x03] Loco GPIO																		*/
/*	[0x02.0x05] Loco SpecialFx																	*/
/*	[0x02.0x12] Loco Master																		*/
/*																								*/
/************************************************************************************************/
using System;

namespace Lib5Z20
{
	public class Lib5Z20Grp02TxLoco
	{
		#region [0x02.0x00.Req]:	State Request
		public typeZ20Msg ReqLocoState(UInt16 _u16LocoNId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp02Loco,
											 Lib5Z20Enums.eNetCmd.LocoState,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16LocoNId);
			return (tMsg);
		}
		public void ReqLocoState(bool _bWait, UInt16 _u16LocoNId)
		{
			typeZ20Msg tMsg = ReqLocoState(_u16LocoNId);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x02.0x02.Cmd]:	Speed Command
		public typeZ20Msg LocoSpeedCmd(UInt16 _u16LocoNId, UInt16 _u16DirSpeed)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp02Loco,
											 Lib5Z20Enums.eNetCmd.LocoSpeed,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16LocoNId);
			tMsg.AddInt16((UInt16)_u16DirSpeed);
			tMsg.AddInt08((byte)0);
			tMsg.AddInt08((byte)0);
			tMsg.AddInt16((UInt16)0);
			return (tMsg);
		}
		public void LocoSpeedCmd(bool _bWait, UInt16 _u16LocoNId, UInt16 _u16DirSpeed)
		{
			typeZ20Msg tMsg = LocoSpeedCmd(_u16LocoNId, _u16DirSpeed);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x02.0x04.Cmd]:	Function Command
		public typeZ20Msg LocoFxCmd(UInt16 _u16LocoNId, UInt16 _u16FxNum, UInt16 _u16FxVal)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp02Loco,
											 Lib5Z20Enums.eNetCmd.LocoFxNum,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16LocoNId);
			tMsg.AddInt16((UInt16)_u16FxNum);
			tMsg.AddInt16((UInt16)_u16FxVal);
			return (tMsg);
		}
		public void LocoFxCmd(bool _bWait, UInt16 _u16LocoNId, UInt16 _u16FxNum, UInt16 _u16FxVal)
		{
			typeZ20Msg tMsg = LocoFxCmd(_u16LocoNId, _u16FxNum, _u16FxVal);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x02.0x10.Cmd]:	Activ Command
		public typeZ20Msg LocoActivCmd(UInt16 _u16LocoNId, UInt16 _u16Mode)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp02Loco,
											 Lib5Z20Enums.eNetCmd.LocoActiv,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16LocoNId);
			tMsg.AddInt16((UInt16)_u16Mode);
			return (tMsg);
		}
		public void LocoActivCmd(bool _bWait, UInt16 _u16LocoNId, UInt16 _u16Mode)
		{
			typeZ20Msg tMsg = LocoActivCmd(_u16LocoNId, _u16Mode);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x12.0x11.Req]:	Stack Request
		public typeZ20Msg LocoStackReq(UInt16 _uSrcNId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp12Loco,
											 Lib5Z20Enums.eNetCmd.LocoStack,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_uSrcNId);
			return (tMsg);
		}
		public void LocoStackReq(bool _bWait, UInt16 _uSrcNId)
		{
			typeZ20Msg tMsg = LocoStackReq(_uSrcNId);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
