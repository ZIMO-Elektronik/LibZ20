/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x07, Data Control Datagramms							*/
/*																								*/
/*	Implemented:																				*/
/*	[0x07.0x01] Item By Index																	*/
/*	[0x07.0x02] Item By Net-Id																	*/
/*	[0x07.0x07] Data Value																		*/
/*	[0x07.0x1F] Data Del																		*/
/*	--------------------			LAN															*/
/*	[0x17.0x10] Data NameX																		*/
/*	[0x17.0x19] Data Speed Tab																	*/
/*	[0x17.0x27] Data GUI 0																		*/
/*	[0x17.0x28] Data GUI 1																		*/
/*																								*/
/*	Pending:																					*/
/*	[0x07.0x00] Data Count																		*/
/*	[0x07.0x03] Data CRC																		*/
/*	[0x07.0x06] Data Flag																		*/
/*	[0x07.0x10] Data Name																		*/
/*	[0x07.0x12] Data Image																		*/
/*	[0x07.0x14] Data Fx Mode																	*/
/*	[0x07.0x18] Data Speed Item																	*/
/*	[0x07.0x1A] Data Save																		*/
/*																								*/
/************************************************************************************************/
using System;

namespace Lib5Z20
{
	public class Lib5Z20Grp07RxData
	{
		#region Delegate Definitions
		public delegate void delegateRxDataGrpCntAck(typeZ20MsgDataGrpCnt _tGrpCnt);
		public delegate void delegateRxDataListIdxAck(typeZ20MsgDataListIdx _tListIdxItem);
		public delegate void delegateRxDataListNIdAck(typeZ20MsgDataListNId _tListNIdItem);
		public delegate void delegateRxDataValAck(typeZ20MsgDataValueX _tValX);
		public delegate void delegateRxDataNameAck(typeZ20MsgDataNameX _tNameX);
		public delegate void delegateRxDataGuiAck(typeZ20MsgLocoGUI _tLocoGUI);
		public delegate void delegateRxDataClear(UInt16 _u16NId);
		public delegate void delegateRxDataSpeedTab(typeZ20MsgDataSpeedTab _tSpeedTab);
		#endregion
		#region Events
		public delegateRxDataGrpCntAck EventRxDataGrpCnt;
		public delegateRxDataListIdxAck EventRxDataListIdx;
		public delegateRxDataListNIdAck EventRxDataListNId;
		public delegateRxDataValAck EventRxDataValAck;
		public delegateRxDataNameAck EventRxDataNameAck;
		public delegateRxDataClear EventRxDataClear;
		public delegateRxDataGuiAck EventRxDataGuiAck;
		public delegateRxDataSpeedTab EventRxDataSpeedTab;
		#endregion
		#region Data [Grp 0x07] Message Reveiver
		public void Lib5Z20RxGrp07(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.GroupCount:
				{
					Lib5Z20RxDataCount(_tMsg);
				}
				break;
				//
				case Lib5Z20Enums.eNetCmd.ItemIdx:
				{
					Lib5Z20RxDataItemByIdx(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.ItemNId:
				{
					Lib5Z20RxDataItemByNId(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataCRC32:
				{
					Lib5Z20RxDataCRC(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataFlag:
				{
					Lib5Z20RxDataBit(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataValue:
				{
					Lib5Z20RxDataVal(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataName:
				{
					Lib5Z20RxDataName(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataImage:
				{
					Lib5Z20RxDataImage(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataFxMode:
				{
					Lib5Z20RxDataFxMode(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataSpeedItem:
				{
					Lib5Z20RxDataSpeedItem(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataSave:
				{
					Lib5Z20RxDataSave(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataDel:
				{
					Lib5Z20RxDataDel(_tMsg);
				}
				break;
			}
		}
		#endregion
		#region Data [Grp 0x17] Message Reveiver
		public void Lib5Z20RxGrp17(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.ItemIdx:
				{
					Lib5Z20RxDataXItemByIdx(_tMsg);
				}
				break;
				//--------	eXtended LAN Datagramms
				case Lib5Z20Enums.eNetCmd.DataObjGUI0:
				{
					Lib5Z20RxDataGUI0(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataObjGUI1:
				{
					Lib5Z20RxDataGUI1(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.DataSpeedTab:
				{
					Lib5Z20RxDataSpeedTab(_tMsg);
				}
				break;
			}
		}
		#endregion
		#region [0x07.0x00] Data Count
		private void Lib5Z20RxDataCount(typeZ20Msg _tMsg)
		{
			if (EventRxDataGrpCnt != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataGrpCnt tGrpCnt = new typeZ20MsgDataGrpCnt (_tMsg);
					EventRxDataGrpCnt(tGrpCnt);
				}
			}
		}
		#endregion
		#region [0x07.0x01] Item By Index
		private void Lib5Z20RxDataItemByIdx(typeZ20Msg _tMsg)
		{
			if (EventRxDataListIdx != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataListIdx tListIdx = new typeZ20MsgDataListIdx(_tMsg);
					EventRxDataListIdx(tListIdx);
				}
			}
		}
		#endregion
		#region [0x07.0x02] Item By Net-Id
		private void Lib5Z20RxDataItemByNId(typeZ20Msg _tMsg)
		{
			if (EventRxDataListNId != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataListNId tListNId = new typeZ20MsgDataListNId(_tMsg);
					EventRxDataListNId(tListNId);
				}
			}
		}
		#endregion
		#region [0x07.0x03] Data CRC
		private void Lib5Z20RxDataCRC(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x07.0x06] Data Flag
		private void Lib5Z20RxDataBit(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x07.0x07] Data Value
		private void Lib5Z20RxDataVal(typeZ20Msg _tMsg)
		{
			if (EventRxDataValAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataValueX tValX = new typeZ20MsgDataValueX(_tMsg);
					EventRxDataValAck(tValX);
				}
			}
		}
		#endregion
		#region [0x07.0x10] Data Name
		private void Lib5Z20RxDataName(typeZ20Msg _tMsg)
		{
			if (EventRxDataNameAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataNameX tNameX = new typeZ20MsgDataNameX(_tMsg);
					EventRxDataNameAck(tNameX);
				}
			}
		}
		#endregion
		#region [0x07.0x12] Data Image
		private void Lib5Z20RxDataImage(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x07.0x14] Data Fx Mode
		private void Lib5Z20RxDataFxMode(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x07.0x18] Data Speed Item
		private void Lib5Z20RxDataSpeedItem(typeZ20Msg _tMsg)
		{
		}
		#endregion
		//
		#region [0x07.0x1A] Data Save
		private void Lib5Z20RxDataSave(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x07.0x1F] Data Del
		private void Lib5Z20RxDataDel(typeZ20Msg _tMsg)
		{
			if (EventRxDataClear != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxDataClear(_tMsg.DataU16Get(0));
				}
			}
		}
		#endregion
		//
		//--------	eXtended LAN Datagramms
		//
		#region [0x17.0x01] Item By Index
		private void Lib5Z20RxDataXItemByIdx(typeZ20Msg _tMsg)
		{
			if (EventRxDataListIdx != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataListIdx tListIdx = new typeZ20MsgDataListIdx(_tMsg);
					EventRxDataListIdx(tListIdx);
				}
			}
		}
		#endregion
		#region [0x17.0x19] Data Speed Tab
		private void Lib5Z20RxDataSpeedTab(typeZ20Msg _tMsg)
		{
			if (EventRxDataSpeedTab != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgDataSpeedTab tDataSpeedTab = new typeZ20MsgDataSpeedTab(_tMsg);
					EventRxDataSpeedTab(tDataSpeedTab);
				}
			}
		}
		#endregion
		#region [0x17.0x27] Data GUI 0
		private void Lib5Z20RxDataGUI0(typeZ20Msg _tMsg)
		{
			if (EventRxDataGuiAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgLocoGUI tLocoGUI = new typeZ20MsgLocoGUI(0, _tMsg);
					EventRxDataGuiAck(tLocoGUI);
				}
			}
		}
		#endregion
		#region [0x17.0x28] Data GUI 1
		private void Lib5Z20RxDataGUI1(typeZ20Msg _tMsg)
		{
			if (EventRxDataGuiAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgLocoGUI tLocoGUI = new typeZ20MsgLocoGUI(_tMsg);
					EventRxDataGuiAck(tLocoGUI);
				}
			}
		}
		#endregion
	}
}
