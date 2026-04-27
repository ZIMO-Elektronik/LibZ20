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

using System.Diagnostics;

namespace Lib5Z20
{
	public class Lib5Z20Grp06RxTrack
	{
		#region Delegate Definitions
		public delegate void delegateRxTSE_CfgAck(typeZ20MsgTrackCfgVal _tTrackCfgVal);
		public delegate void delegateRxTrackFindAck(typeZ20MsgTrackFind _tTrackFind);
		public delegate void delegateRxRCN218CtrlAck(typeZ20MsgRCN218Control _tRCN218Control);
		public delegate void delegateRxRCN218LogonUId(typeZ20MsgRCN218Result _tRCN218Result);
		public delegate void delegateRxRCN218Assign(typeZ20MsgRCN218Assign _tRCN218Assign);
		//
		public delegate void delegateRxDecoderCtrlGUI(typeZ20MsgDecoderCtrlGUI _tDecoderCtrlGUI);
		public delegate void delegateRxDecoderInfoGUI(typeZ20MsgDecoderInfoGUI _tDecoderInfoGUI);
		public delegate void delegateRxDecoderLogGUI(typeZ20MsgDecoderGuiLog _tDecoderGuiLog);
		#endregion
		#region Events
		public delegateRxTSE_CfgAck EventRxTSE_CfgCommandAck;
		public delegateRxTSE_CfgAck EventRxTSE_CfgResultAck;
		public delegateRxTrackFindAck EventRxTrackFindAck;
		public delegateRxRCN218CtrlAck EventRxRCN218CtrlAck;
		public delegateRxRCN218LogonUId EventRxRCN218LogonUId;
		public delegateRxRCN218Assign EventRxRCN218Assign;
		//
		public delegateRxDecoderCtrlGUI EventRxDecoderCtrlGUI;
		public delegateRxDecoderInfoGUI EventRxDecoderInfoGUI;
		public delegateRxDecoderLogGUI EventRxDecoderGuiLog;
		#endregion
		//
		#region Track Signal Engine [Grp 0x06] Message Reveiver
		public void Lib5Z20RxGrp06(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.TSE_Mode:
				{
					Lib5Z20RxTrackMode(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_Info:
				{
					Lib5Z20RxTrackInfo(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ValClr:
				{
					Lib5Z20RxTrackValClr(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ValRd:
				case Lib5Z20Enums.eNetCmd.TSE_ValRdX:
				case Lib5Z20Enums.eNetCmd.TSE_ValWr:
				{
					Lib5Z20RxTrackValRdWr(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ValWr16:
				{
					Lib5Z20RxTrackVal16Wr(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_TaskRd:
				{
					Lib5Z20RxTrackTaskRd(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_Find:
				{
					Lib5Z20RxTrackFind(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_RCN218Ctrl:
				{
					Lib5Z20RxTrackLogonCtrl(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_RCN218UId:
				{
					Lib5Z20RxTrackLogonUId(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_RCN218Assign:
				{
					Lib5Z20RxTrackLogonAssign(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ZSysGuiCtrl:
				{
					Lib5Z20RxZSysGuiCtrl(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ZSysGuiInfo:
				{
					Lib5Z20RxZSysGuiInfo(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_ZSysGuiLog:
				{
					Lib5Z20RxZSysGuiLog(_tMsg);
				}
				break;
				//
				case Lib5Z20Enums.eNetCmd.TSE_BiDi0:
				{
					Lib5Z20RxBiDiRawData0(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_BiDi1:
				{
					Lib5Z20RxBiDiRawData1(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TSE_BiDiC:
				{
					Lib5Z20RxBiDiRawDataC(_tMsg);
				}
				break;
			}
		}
		#endregion
		//
		#region [0x06.0x00] Track Mode
		private void Lib5Z20RxTrackMode(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x06.0x02] Track Info
		private void Lib5Z20RxTrackInfo(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x06.0x04] Track Clear
		private void Lib5Z20RxTrackValClr(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x06.0x08] Track RdWr
		private void Lib5Z20RxTrackValRdWr(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				//----	Just Command Ack
				if ((_tMsg.iSize == 8) && (EventRxTSE_CfgCommandAck !=  null))
				{
					EventRxTSE_CfgCommandAck(new typeZ20MsgTrackCfgVal(_tMsg));
				}
				//----	Result Ack
				if ((_tMsg.iSize == 10) && (EventRxTSE_CfgResultAck != null))
				{
					EventRxTSE_CfgResultAck(new typeZ20MsgTrackCfgVal(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x0D] Track Vale 16 Wr
		private void Lib5Z20RxTrackVal16Wr(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x06.0x0E] Track Task Read
		private void Lib5Z20RxTrackTaskRd(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x06.0x10] Track Find
		private void Lib5Z20RxTrackFind(typeZ20Msg _tMsg)
		{
			if(EventRxTrackFindAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxTrackFindAck(new typeZ20MsgTrackFind(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x11] Track Logon Control
		private void Lib5Z20RxTrackLogonCtrl(typeZ20Msg _tMsg)
		{
			if (EventRxRCN218CtrlAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxRCN218CtrlAck(new typeZ20MsgRCN218Control(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x12] Track Logon UId
		private void Lib5Z20RxTrackLogonUId(typeZ20Msg _tMsg)
		{
			if (EventRxRCN218LogonUId != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxRCN218LogonUId(new typeZ20MsgRCN218Result(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x13] Track Logon Assign
		private void Lib5Z20RxTrackLogonAssign(typeZ20Msg _tMsg)
		{
			if (EventRxRCN218Assign != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxRCN218Assign(new typeZ20MsgRCN218Assign(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x14] Track Request GUI
		private void Lib5Z20RxZSysGuiCtrl(typeZ20Msg _tMsg)
		{
			if (EventRxDecoderCtrlGUI != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxDecoderCtrlGUI(new typeZ20MsgDecoderCtrlGUI(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x15] Track Info GUI
		private void Lib5Z20RxZSysGuiInfo(typeZ20Msg _tMsg)
		{
			if (EventRxDecoderInfoGUI != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxDecoderInfoGUI(new typeZ20MsgDecoderInfoGUI(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x16] Track Log GUI
		private void Lib5Z20RxZSysGuiLog(typeZ20Msg _tMsg)
		{
			if (EventRxDecoderGuiLog != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
//					EventRxDecoderGuiLog(new typeZ20MsgDecoderGuiLog(_tMsg));
				}
			}
		}
		#endregion
		#region [0x06.0x1D] BiDi Raw Data 0
		private void Lib5Z20RxBiDiRawData0(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x06.0x1E] BiDi Raw Data 1
		private void Lib5Z20RxBiDiRawData1(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x06.0x1F] BiDi Raw Data C
		private void Lib5Z20RxBiDiRawDataC(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
}
