using System;

/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x00, System State										*/
/*																								*/
/************************************************************************************************/

namespace Lib5Z20
{
	public class Lib5Z20Grp00RxSys
	{
		public delegate void delegateRxSystemStateAck(typeZ20MsgSystemState _tState);
		//
		public delegateRxSystemStateAck EventRxSystemStateAck;
		//
		public Lib5Z20Grp00RxSys() { }
		//
		public void Lib5Z20RxGrp01(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				//----	[0x00.0x00] System Status
				case Lib5Z20Enums.eNetCmd.SysState:
				{
					if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
						(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
					{
						if (EventRxSystemStateAck != null)
						{
							typeZ20MsgSystemState tSysState = new typeZ20MsgSystemState(_tMsg);
							EventRxSystemStateAck(tSysState);
						}
					}
				}
				break;
				//----	[0x00.0x02] System Reset
				case Lib5Z20Enums.eNetCmd.SysReset:
				{
					if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
						(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
					{
					}
				}
				break;
				//----	[0x00.0x04] System Power Mode
				case Lib5Z20Enums.eNetCmd.SysPwrMode:
				{
					if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
						(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
					{
					}
				}
				break;
				//----	[0x00.0x02] System Error
				case Lib5Z20Enums.eNetCmd.SysError:
				{
					if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) || 
						(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
					{
						if (EventRxSystemStateAck != null)
						{
							typeZ20MsgSystemState tSysState = new typeZ20MsgSystemState(_tMsg);
							EventRxSystemStateAck(tSysState);
						}
					}
				}
				break;
				default:
				{
					LibNetZ20.objLog.Write("unknown Cmd");
				}
				break;
			}
		}
	}
}
