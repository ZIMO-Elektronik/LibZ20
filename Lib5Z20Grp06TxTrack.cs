/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x02, Loco Control Datagramms							*/
/*																								*/
/*	Implemented:																				*/
/*	[0x06.0x08] Track RdWr																		*/
/*	[0x06.0x11] Track Logon Control																*/
/*	[0x06.0x12] Track Logon UId																	*/
/*	[0x06.0x13] Track Logon Assign																*/
/*	[0x06.0x14] Track Request GUI																*/
/*	[0x06.0x15] Track Info GUI																	*/
/*	Pending:																					*/
/*	[0x06.0x00] Track Mode																		*/
/*	[0x06.0x02] Track Info																		*/
/*	[0x06.0x04] Track Clear																		*/
/*	[0x06.0x0D] Track Vale 16 Wr																*/
/*	[0x06.0x0E] Track Task Read																	*/
/*	[0x06.0x10] Track Find																		*/
/*	[0x06.0x1D] BiDi Raw Data 0																	*/
/*	[0x06.0x1E] BiDi Raw Data 1																	*/
/*	[0x06.0x1F] BiDi Raw Data Ctrl																*/
/*																								*/
/************************************************************************************************/

using System;
using System.Diagnostics;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	public class Lib5Z20Grp06TxTrack
	{
		#region [0x06.0x00]:	Abfrage Track Engine Mode
		public typeZ20Msg ReqTrackMode(UInt16 _u16SrcNId, UInt16 _u16Pin)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp06TSE,
											 Lib5Z20Enums.eNetCmd.TSE_Mode,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt08((byte)_u16Pin);
			return (tMsg);
		}
		public void ReqInfoModul(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16Pin)
		{
			typeZ20Msg tMsg = ReqTrackMode(_u16SrcNId, _u16Pin);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x06.0x10]:	Abfrage Track Find
		public typeZ20Msg TrackFindCmd(UInt16 _u16SrcNId, enumLocoFind _eMode)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp06TSE,
											 Lib5Z20Enums.eNetCmd.TSE_Find,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_eMode);
			return (tMsg);
		}
		public void TrackFindCmd(bool _bWait, UInt16 _u16SrcNId, enumLocoFind _eMode)
		{
			typeZ20Msg tMsg = TrackFindCmd(_u16SrcNId, _eMode);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x06.0x11]:	RCN 218 Control Command
		public typeZ20Msg RCN218CtrlCmd(enumFindRCN218 _eControl, UInt16 _u16Repeat, UInt16 _u16Ticks)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_RCN218Ctrl,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16(0);						//	'All' Systems
			tMsg.AddInt16((UInt16)_eControl);		//
			tMsg.AddInt16(_u16Repeat);
			tMsg.AddInt16(_u16Ticks);
			return (tMsg);
		}
		public void RCN218CtrlCmd(bool _bWait, enumFindRCN218 _eControl, UInt16 _u16Repeat, UInt16 _u16Ticks)
		{
			typeZ20Msg tMsg = RCN218CtrlCmd(_eControl, _u16Repeat, _u16Ticks);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x06.0x13]:	RCN 218 Assign Command
		public typeZ20Msg RCN218AssignCmd(UInt16 _u16LocoNId, UInt16 _u16Vendor, UInt32 _u32DecodeUId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_RCN218Assign,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16(_u16LocoNId);
			tMsg.AddInt16(_u16Vendor);
			tMsg.AddInt32(_u32DecodeUId);
			return (tMsg);
		}
		public void RCN218AssignCmd(bool _bWait, UInt16 _u16LocoNId, UInt16 _u16Vendor, UInt32 _u32DecodeUId)
		{
			typeZ20Msg tMsg = RCN218AssignCmd(_u16LocoNId, _u16Vendor, _u32DecodeUId);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x06.0x14]:	Decoder GUI Command
		public typeZ20Msg CmdTrackDecoderGUI(UInt16 _u16NId, UInt16 _u16Typ)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp06TSE,
											 Lib5Z20Enums.eNetCmd.TSE_ZSysGuiCtrl,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Typ);
			return (tMsg);
		}
		public void CmdTrackDecoderGUI(bool _bWait, UInt16 _u16NId, UInt16 _u16Typ)
		{
			typeZ20Msg tMsg = CmdTrackDecoderGUI(_u16NId, _u16Typ);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x16.0x18]:	Decoder Config Read Command
		public typeZ20Msg TrackCfgRdCmd(UInt16 _u16NId, UInt32 _u32Num)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_ValRd,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)LibNetZ20.u16ZsNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt32((UInt32)_u32Num);
			return (tMsg);
		}
		public void TrackCfgRdCmd(bool _bWait, UInt16 _u16NId, UInt32 _u32Num)
		{
			Debug.WriteLine(DateTime.Now.ToString("ss:fff") +
							" Loco=" + _u16NId.ToString().PadLeft(5) + " N=" + _u32Num.ToString());
			typeZ20Msg tMsg = TrackCfgRdCmd(_u16NId, _u32Num);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x16.0x18]:	Decoder Config Write Command
		public typeZ20Msg TrackCfgWr08Cmd(UInt16 _u16NId, UInt32 _u32Num, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_ValWr,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)LibNetZ20.u16ZsNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt32((UInt32)_u32Num);
			tMsg.AddInt16((UInt16)_u16Val);
			return (tMsg);
		}
		public void TrackCfgWr08Cmd(bool _bWait, UInt16 _u16NId, UInt32 _u32Num, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = TrackCfgWr08Cmd(_u16NId, _u32Num, _u16Val);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x16.0x18]:	Decoder Config Write Command
		public typeZ20Msg TrackCfgWr16Cmd(UInt16 _u16NId, UInt16 _u16Num, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_ValWr16,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)LibNetZ20.u16ZsNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Num);
			tMsg.AddInt16((UInt16)_u16Val);
			return (tMsg);
		}
		public void TrackCfgWr16Cmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Num, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = TrackCfgWr16Cmd(_u16NId, _u16Num, _u16Val);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x16.0x18]:	Decoder Config Write Command
		public typeZ20Msg TrackCfgWrXCmd(UInt16 _u16NId, UInt32 _u32Num, byte _u08Cnt, byte[] _u08Val)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp16TSE,
											 Lib5Z20Enums.eNetCmd.TSE_ValWrX,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)LibNetZ20.u16ZsNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt32((UInt32)_u32Num);
			tMsg.AddInt08((byte)_u08Cnt);
			for(int iCnt = 0; iCnt < _u08Cnt; iCnt++)
			{
				tMsg.AddInt08((byte)_u08Val[iCnt]);
			}
			return (tMsg);
		}
		public void TrackCfgWrXCmd(bool _bWait, UInt16 _u16NId, UInt32 _u32Num, byte _u08Cnt, byte[] _u08Val)
		{
			typeZ20Msg tMsg = TrackCfgWrXCmd(_u16NId, _u32Num, _u08Cnt, _u08Val);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
