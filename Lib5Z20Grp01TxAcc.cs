/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x01, Accessory Control Datagramms						*/
/*																								*/
/*	Implemented:																				*/
/*	[0x01.0x00] Accessory State					N.A.											*/
/*	[0x01.0x01] Accessory Mode																	*/
/*	[0x01.0x02] Accessory GPIO																	*/
/*	[0x01.0x04] Accessory Pin4					Req/Cmd											*/
/*	[0x01.0x06] Accessory Pin6					Req/Cmd											*/
/*	[0x01.0x05] Accessory Data																	*/
/*	Pending:																					*/
/*	[0x01.0x07] Accessory Auto Speed															*/
/*	[0x01.0x09] Accessory Limit																	*/
/*	[0x01.0x0A] Accessory Signal																*/
/*	[0x01.0x16] Accessory Pin 16																*/
/*																								*/
/************************************************************************************************/

using System;

using Lib5Z20;

namespace Lib5Z20
{
	public class Lib5Z20Grp01TxAcc
	{
		#region [0x01.0x00]:	Accessory State Request	!NA!
		#endregion

		#region [0x01.0x01.Req]:	Accessory Mode Request
		public typeZ20Msg AccModeReq(UInt16 _u16NId, byte _u08Type, byte _u08Port)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccMode,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((byte)_u08Type);
			tMsg.AddInt08((byte)_u08Port);
			return (tMsg);
		}
		public void AccModeReq(bool _bWait, UInt16 _u16NId, byte _u08Type, byte _u08Port)
		{
			typeZ20Msg tMsg = AccModeReq(_u16NId, _u08Type, _u08Port);
			LibNetZ20.MsgTx(tMsg);
		}
		#endregion
		#region [0x01.0x02.Req]:	Accessory GPIO Request
		public typeZ20Msg AccPortReq(UInt16 _u16NId, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccGPIO,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((byte)_u16Type);
			return (tMsg);
		}
		public void AccPortReq(bool _bWait, UInt16 _u16NId, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = AccPortReq(_u16NId, _u16Type);
		}
		#endregion
		#region [0x01.0x04.Req]:	Accessory Pin 4 Request
		public typeZ20Msg AccPin4Req(UInt16 _u16NId, UInt16 _u16Port)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccPin4,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((Byte)_u16Port);
			return (tMsg);
		}
		public void AccPin4Req(bool _bWait, UInt16 _u16NId, UInt16 _u16Pin)
		{
			typeZ20Msg tMsg = AccPin4Req(_u16NId, _u16Pin);
		}
		#endregion
		#region [0x01.0x04.Cmd]:	Accessory Pin 4 Command
		public typeZ20Msg AccPin4Cmd(UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16State)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccPin4,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((Byte)_u16Pin);
			tMsg.AddInt08((Byte)_u16State);
			return (tMsg);
		}
		public void AccPin4Cmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16State)
		{
			typeZ20Msg tMsg = AccPin4Cmd(_u16NId, _u16Pin, _u16State);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x01.0x06.Req]:	Accessory Pin 6 Request
		public typeZ20Msg ReqAccPort6(UInt16 _u16NId, Byte _u08Type, Byte _u08Pin)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccPin6,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((Byte)_u08Pin);
			tMsg.AddInt08((Byte)_u08Type);
			return (tMsg);
		}
		public void ReqAccPort6(bool _bWait, UInt16 _u16NId, Byte _u08Type, Byte _u08Pin)
		{
			typeZ20Msg tMsg = ReqAccPort6(_u16NId, _u08Type, _u08Pin);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x01.0x06.Req]:	Accessory Pin 6 Command
		public typeZ20Msg AccPin6Cmd(UInt16 _u16NId, UInt32 _u32Type, UInt32 _u32Pin, UInt32 _u32Data)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccPin6,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((Byte)_u32Pin);
			tMsg.AddInt08((Byte)_u32Type);
			tMsg.AddInt16((Byte)_u32Data);
			return (tMsg);
		}
		public void AccPin6Cmd(bool _bWait, UInt16 _u16NId, UInt32 _u32Type, UInt32 _u32Pin, UInt32 _u32Data)
		{
			typeZ20Msg tMsg = AccPin6Cmd(_u16NId, _u32Type, _u32Pin, _u32Data);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x01.0x05.Req]:	Accessory Data Request
		public typeZ20Msg AccDataReq(UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp01Acc,
											 Lib5Z20Enums.eNetCmd.AccData,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((Byte)_u16Pin);
			tMsg.AddInt08((Byte)_u16Type);
			return (tMsg);
		}
		public void AccDataReq(bool _bWait, UInt16 _u16NId, UInt16 _u16Pin, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = AccDataReq(_u16NId, _u16Pin, _u16Type);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x11.0x05.Req]:	Accessory Data Request
		public typeZ20Msg AccDataReqX(UInt16 _uSrcNId, UInt16 _uObjNId, UInt16 _uSubGrp, UInt32 _uPins)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp11Acc,
											 Lib5Z20Enums.eNetCmd.AccData,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_uSrcNId);
			tMsg.AddInt16((UInt16)_uObjNId);
			tMsg.AddInt16((UInt16)_uSubGrp);
			tMsg.AddInt32((UInt32)_uPins);
			return (tMsg);
		}
		public void AccDataReqX(bool _bWait, UInt16 _uSrcNId, UInt16 _uObjNId, UInt16 _uSubGrp, UInt32 _uPins)
		{
			typeZ20Msg tMsg = AccDataReqX(_uSrcNId, _uObjNId, _uSubGrp, _uPins);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
