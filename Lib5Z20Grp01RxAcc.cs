/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x01, Accessory Control Datagramms						*/
/*																								*/
/*	Implemented:																				*/
/*	[0x01.0x00] Accessory State																	*/
/*	[0x01.0x01] Accessory Mode																	*/
/*	[0x01.0x02] Accessory GPIO																	*/
/*	[0x01.0x04] Accessory Pin4																	*/
/*	[0x01.0x06] Accessory Pin6																	*/
/*	[0x01.0x05] Accessory Data																	*/
/*	Pending:																					*/
/*	[0x01.0x07] Accessory Auto Speed															*/
/*	[0x01.0x09] Accessory Limit																	*/
/*	[0x01.0x0A] Accessory Signal																*/
/*	[0x01.0x16] Accessory Pin 16																*/
/*																								*/
/************************************************************************************************/
using System;
using System.Diagnostics;

namespace Lib5Z20
{
	public class Lib5Z20Grp01RxAcc
	{
		#region Delegate Definitions
		public delegate void delegateRxModulStateAck(typeZ20MsgModulState _tState);
		public delegate void delegateRxModulModeAck(typeZ20MsgModulMode _tMode);
		public delegate void delegateRxModulGPIOAck(typeZ20MsgModulGPIO _tGPIO);
		public delegate void delegateRxModulPin4Ack(typeZ20MsgModulPin4 _tPin4);
		public delegate void delegateRxModulDataAck(typeZ20MsgModulData _tData);
		public delegate void delegateRxModulPin6Ack(typeZ20MsgModulPin6 _tPin6);
		#endregion
		#region Events
		public delegateRxModulStateAck EventRxModulStateAck;
		public delegateRxModulModeAck EventRxModulModeAck;
		public delegateRxModulGPIOAck EventRxModulGPIOAck;
		public delegateRxModulPin4Ack EventRxModulPin4Ack;
		public delegateRxModulDataAck EventRxModulDataAck;
		public delegateRxModulPin6Ack EventRxModulPin6Ack;
		#endregion
		#region Accessory [Grp 0x01] Message Reveiver
		public void Lib5Z20RxGrp01(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				//	Message 0x01.0x00: Accessory State
				case Lib5Z20Enums.eNetCmd.AccState:
				{
					Lib5Z20RxAccessoryState(_tMsg);
				}
				break;
				//	Message 0x01.0x01: Accessory Mode
				case Lib5Z20Enums.eNetCmd.AccMode:
				{
					Lib5Z20RxAccessoryMode(_tMsg);
				}
				break;
				//	Message 0x01.0x02: Accessory GPIO
				case Lib5Z20Enums.eNetCmd.AccGPIO:
				{
					Lib5Z20RxAccessoryGPIO(_tMsg);
				}
				break;
				//	Message 0x01.0x04: Accessory Pin Data, 4 Byte Form
				case Lib5Z20Enums.eNetCmd.AccPin4:
				{
					Lib5Z20RxAccessoryPin4(_tMsg);
				}
				break;
				//	Message 0x01.0x06: Accessory Pin Data, 6 Byte Form
				case Lib5Z20Enums.eNetCmd.AccPin6:
				{
					Lib5Z20RxAccessoryPin6(_tMsg);
				}
				break;
				//	Message 0x01.0x05: Accessory Data
				case Lib5Z20Enums.eNetCmd.AccData:
				{
					Lib5Z20RxAccessoryData(_tMsg);
				}
				break;
				//	Message 0x01.0x07:
				case Lib5Z20Enums.eNetCmd.AccAutoSpeed:
				{
					Lib5Z20RxAccessoryAutoSpeed(_tMsg);
				}
				break;
				//	Message 0x01.0x09:
				case Lib5Z20Enums.eNetCmd.AccLimitCmd:
				{
					Lib5Z20RxAccessoryAutoLimit(_tMsg);
				}
				break;
				//	Message 0x01.0x0A:
				case Lib5Z20Enums.eNetCmd.AccSignalCtrl:
				{
					Lib5Z20RxAccessorySignal(_tMsg);
				}
				break;
			}
		}
		#endregion
		#region [0x01.0x00] Accessory State
		private void Lib5Z20RxAccessoryState(typeZ20Msg _tMsg)
		{
			if (EventRxModulStateAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulState tState = new typeZ20MsgModulState(_tMsg);
					EventRxModulStateAck(tState);
				}
			}
		}
		#endregion
		#region [0x01.0x01] Accessory Mode
		private void Lib5Z20RxAccessoryMode(typeZ20Msg _tMsg)
		{
			if (EventRxModulModeAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || (_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulMode tMode = new typeZ20MsgModulMode(_tMsg);
					EventRxModulModeAck(tMode);
				}
			}
		}
		#endregion
		#region [0x01.0x02] Accessory GPIO
		private void Lib5Z20RxAccessoryGPIO(typeZ20Msg _tMsg)
		{
			if (EventRxModulGPIOAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || (_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulGPIO tGPIO = new typeZ20MsgModulGPIO(_tMsg);
					EventRxModulGPIOAck(tGPIO);
				}
			}
		}
		#endregion
		#region [0x01.0x04] Accessory Pin4
		private void Lib5Z20RxAccessoryPin4(typeZ20Msg _tMsg)
		{
			if (EventRxModulPin4Ack != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulPin4 tPin4 = new typeZ20MsgModulPin4(_tMsg);
					EventRxModulPin4Ack(tPin4);
				}
			}
		}
		#endregion
		#region [0x01.0x06] Accessory Pin6
		private void Lib5Z20RxAccessoryPin6(typeZ20Msg _tMsg)
		{
			if (EventRxModulPin6Ack != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulPin6 tPin6 = new typeZ20MsgModulPin6(_tMsg);
					EventRxModulPin6Ack(tPin6);
				}
			}
		}
		#endregion
		#region [0x01.0x05] Accessory Data
		private void Lib5Z20RxAccessoryData(typeZ20Msg _tMsg)
		{
			if (EventRxModulDataAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgModulData tData = new typeZ20MsgModulData(_tMsg);
					EventRxModulDataAck(tData);
				}
			}
		}
		#endregion
		#region [0x01.0x07] Accessory Auto Speed
		private void Lib5Z20RxAccessoryAutoSpeed(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x01.0x09] Accessory Limit
		private void Lib5Z20RxAccessoryAutoLimit(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x01.0x0A] Accessory Signal
		private void Lib5Z20RxAccessorySignal(typeZ20Msg _tMsg)
		{
		}
		#endregion
		#region [0x01.0x16] Accessory Pin 16
		private void Lib5Z20RxAccessoryPin16(typeZ20Msg _tMsg)
		{
		}
		#endregion
	}
}

