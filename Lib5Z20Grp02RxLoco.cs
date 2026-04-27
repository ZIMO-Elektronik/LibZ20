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

namespace Lib5Z20
{
	public class Lib5Z20Grp02RxLoco
	{
		#region Delegate Definitions
		public delegate void delegateRxLocoModeAck(typeZ20MsgLocoMode _tLocoMode);
		public delegate void delegateRxLocoStateAck(typeZ20MsgLocoState _tLocoState);
		public delegate void delegateRxLocoSpeedAck(typeZ20MsgLocoSpeed _tLocoSpeed);
		public delegate void delegateRxLocoFxAck(typeZ20MsgLocoFx _tLocoFx);
		public delegate void delegateRxLocoStackAck(typeZ20MsgLocoStack _tLocoStack);
		public delegate void delegateRxLocoActivAck(typeZ20MsgLocoActiv _tLocoActiv);
		//
		public delegate void delegateRxLocoStateXAck(typeZ20MsgLocoStateX _tLocoState);
		#endregion
		#region Events
		public delegateRxLocoModeAck EventRxLocoModeAck;
		public delegateRxLocoStateAck EventRxLocoStateAck;
		public delegateRxLocoSpeedAck EventRxLocoSpeedAck;
		public delegateRxLocoFxAck EventRxLocoFxAck;
		public delegateRxLocoStackAck EventRxLocoStack;
		public delegateRxLocoActivAck EventRxLocoActivAck;
		//
		public delegateRxLocoStateXAck EventRxLocoStateXAck;
		#endregion
		#region Message Reveiver
		public void Lib5Z20RxGrp02(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.LocoState:
				{
					Lib5Z20RxLocoState(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoMode:
				{
					Lib5Z20RxLocoMode(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoSpeed:
				{
					Lib5Z20RxLocoSpeed(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoFxGPIO:
				{
					Lib5Z20RxLocoGPIO(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoFxNum:
				{
					Lib5Z20RxLocoFunction(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoSxNum:
				{
					Lib5Z20RxLocoSpecialFx(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoStack:
				{
					Lib5Z20RxLocoStack(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.LocoActiv:
				{
					Lib5Z20RxLocoActiv(_tMsg);
				}
				break;
				default:
				{
				}
				break;
			}
		}
		#endregion
		//
		#region [0x02.0x00] Loco State
		private void Lib5Z20RxLocoState(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				if ((EventRxLocoStateAck != null) && (_tMsg.iSize <= 8))
				{
					typeZ20MsgLocoState tStateAck = new typeZ20MsgLocoState(_tMsg);
					EventRxLocoStateAck.Invoke(tStateAck);
				}
				if ((EventRxLocoStateXAck != null) && (_tMsg.iSize > 8))
				{
					typeZ20MsgLocoStateX tStateX = new typeZ20MsgLocoStateX(_tMsg);
					EventRxLocoStateXAck.Invoke(tStateX);
				}

			}
		}
		#endregion
		#region [0x02.0x01] Loco Mode
		private void Lib5Z20RxLocoMode(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				if (EventRxLocoModeAck != null)
				{
					typeZ20MsgLocoMode tModeAck = new typeZ20MsgLocoMode(_tMsg);
					EventRxLocoModeAck.Invoke(tModeAck);
				}
			}
		}
		#endregion
		#region [0x02.0x02] Loco Speed
		private void Lib5Z20RxLocoSpeed(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				if (EventRxLocoSpeedAck != null)
				{
					typeZ20MsgLocoSpeed tSpeedAck = new typeZ20MsgLocoSpeed(_tMsg);
					EventRxLocoSpeedAck.Invoke(tSpeedAck);
				}
			}
		}
		#endregion
		#region [0x02.0x03] Loco GPIO	!N.A.!
		private void Lib5Z20RxLocoGPIO(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x02.0x04] Loco Function
		private void Lib5Z20RxLocoFunction(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				if (EventRxLocoFxAck != null)
				{
					typeZ20MsgLocoFx tFxAck = new typeZ20MsgLocoFx(_tMsg);
					EventRxLocoFxAck.Invoke(tFxAck);
				}
			}
		}
		#endregion
		#region [0x02.0x05] Loco SpecialFx
		private void Lib5Z20RxLocoSpecialFx(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x02.0x10] Loco Activ
		private void Lib5Z20RxLocoActiv(typeZ20Msg _tMsg)
		{
			if (EventRxLocoActivAck != null)
			{
				typeZ20MsgLocoActiv tActivAck = new typeZ20MsgLocoActiv(_tMsg);
				EventRxLocoActivAck.Invoke(tActivAck);
			}
		}
		#endregion
		#region [0x02.0x11] Loco Stack
		private void Lib5Z20RxLocoStack(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				if (EventRxLocoStack != null)
				{
					typeZ20MsgLocoStack tStackAck = new typeZ20MsgLocoStack(_tMsg);
					EventRxLocoStack.Invoke(tStackAck);
				}
			}
		}
		#endregion
		#region [0x02.0x12] Loco Master
		private void Lib5Z20RxLocoMaster(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
	}
}
