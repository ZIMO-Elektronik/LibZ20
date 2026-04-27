/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x05, Train Control Datagramms							*/
/*																								*/
/*	Implemented:																				*/
/*	Pending:																					*/
/*	[0x05.0x01] Train Part List																	*/
/*	[0x05.0x02] Train Part Find																	*/
/*	[0x05.0x03] Train Owner Set																	*/
/*	[0x05.0x04] Train Owner Del																	*/
/*	[0x05.0x08] Train Part Set																	*/
/*	[0x05.0x09] Train Part Cfg																	*/
/*	[0x05.0x0F] Train Part Del																	*/
/*																								*/
/************************************************************************************************/

namespace Lib5Z20
{
	public class Lib5Z20Grp05RxTrain
	{
		#region Delegate Definitions
		public delegate void delegateRxTrainList(typeZ20Msg _tMsg);
		public delegate void delegateRxTrainPart(typeZ20Msg _tMsg);
		public delegate void delegateRxTrainPartFind(typeZ20Msg _tMsg);
		public delegate void delegateRxLocoFxAck(typeZ20MsgLocoFx _tLocoFx);
		public delegate void delegateRxLocoStackAck(typeZ20MsgLocoStack _tLocoStack);
		public delegate void delegateRxLocoActivAck(typeZ20MsgLocoActiv _tLocoActiv);
		#endregion
		#region Events
		public delegateRxTrainList EventRxTrainListAck;
		public delegateRxTrainPart EventRxTrainPartAck;
		public delegateRxTrainPartFind delegateRxTrainPartFindAckAck;
		public delegateRxLocoFxAck EventRxLocoFxAck;
		public delegateRxLocoStackAck EventRxLocoStack;
		public delegateRxLocoActivAck EventRxLocoActivAck;
		#endregion
		//
		#region Message Reveiver
		public void Lib5Z20RxGrp02(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.TrainPartList:
				{
					Lib5Z20RxTrainPartList(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainPartFind:
				{
					Lib5Z20RxTrainPartFind(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainOwnerSet:
				{
					Lib5Z20RxTrainOwnerSet(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainOwnerDel:
				{
					Lib5Z20RxTrainOwnerDel(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainPartSet:
				{
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainPartCfg:
				{
				}
				break;
				case Lib5Z20Enums.eNetCmd.TrainPartDel:
				{
				}
				break;
			}
		}
		#endregion
		//
		#region [0x05.0x01] Train Part List
		private void Lib5Z20RxTrainPartList(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x02] Train Part Find
		private void Lib5Z20RxTrainPartFind(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x03] Train Owner Set
		private void Lib5Z20RxTrainOwnerSet(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x04] Train Owner Del
		private void Lib5Z20RxTrainOwnerDel(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x08] Train Part Set
		private void Lib5Z20RxTrainPartSet(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x09] Train Part Cfg
		private void Lib5Z20RxTrainPartCfg(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x05.0x0F] Train Part Del
		private void Lib5Z20RxTrainPartDel(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
	}
}
