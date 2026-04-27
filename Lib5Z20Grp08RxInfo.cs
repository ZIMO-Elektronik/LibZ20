/************************************************************************************************/
/*																								*/
/*		LibZ20 Message Decoder for Group 0x07, Data Control Datagramms							*/
/*																								*/
/*	Implemented:																				*/
/*	[0x08.0x00] Info Power																		*/
/*	[0x08.0x05] Info BiDi																		*/
/*	[0x08.0x08] Info Modul																		*/
/*	[0x08.0x0A] Info Config Value																*/
/*	[0x08.0x0C] Info Config Docu																*/
/*	Pending:																					*/
/*	[0x08.0x02] Info TSE																		*/
/*	[0x08.0x04] Info Loco																		*/
/*																								*/
/************************************************************************************************/
using System;
using System.Diagnostics;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	public class Lib5Z20Grp08RxInfo
	{
		#region Delegate Definitions
		public delegate void delegateLogMsg(string _strText);
		//
		public delegate void delegateRxInfoPowerAck(typeZ20MsgInfoPower _tPower);
		public delegate void delegateRxInfoBiDiSpeedEvt(typeZ20MsgInfoBiDi32 _tSpeed);
		public delegate void delegateRxInfoBiDiInfoEvt(typeZ20MsgInfoBiDi32 _tDirection);
		//
		public delegate void delegateRxInfoData(typeZ20MsgInfoData _tValue);
		public delegate void delegateRxInfoBinTagValue(typeZ20MsgBinTagValue _tInfo);
		public delegate void delegateRxInfoBinTagDocu(UInt16 _u16Data);
		#endregion
		#region Events
		public delegateLogMsg EventLog;
		//
		public delegateRxInfoPowerAck EventRxInfoPowerAck;
		public delegateRxInfoBiDiSpeedEvt EventRxInfoBiDiSpeedEvt;
		public delegateRxInfoBiDiInfoEvt EventRxInfoBiDiDirectionEvt;
		public delegateRxInfoBiDiInfoEvt EventRxInfoBiDiTrackVoltEvt;
		public delegateRxInfoBiDiInfoEvt EventRxInfoBiDiQoSEvt;
		//
		public delegateRxInfoData EventRxInfoData;
		public delegateRxInfoBinTagValue EventRxBinTagValue;
		public delegateRxInfoBinTagDocu EventRxInfoDocu;
		#endregion
		//
		#region Data [Grp 0x08] Message Reveiver
		public void RxGrp08Info(typeZ20Msg _tMsg)
		{
			switch (_tMsg.eCmd)
			{
				case Lib5Z20Enums.eNetCmd.InfoPower:
				{
					Lib5Z20RxInfoPower(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoTSE:
				{
					Lib5Z20RxInfoTSE(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoLoco:
				{
					Lib5Z20RxInfoLoco(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoBiDi:
				{
					Lib5Z20RxInfoBiDi(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoData:
				{
					Lib5Z20RxInfoData(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoBinTagValue:
				{
					Lib5Z20RxBinTagValue(_tMsg);
				}
				break;
				case Lib5Z20Enums.eNetCmd.InfoBinTagDocu:
				{
					Lib5Z20RxInfoCfgDoc(_tMsg);
				}
				break;
			}
		}
		#endregion
		//
		#region [0x08.0x00] Info Power
		private void Lib5Z20RxInfoPower(typeZ20Msg _tMsg)
		{
			if (EventRxInfoPowerAck != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					typeZ20MsgInfoPower tPower = new typeZ20MsgInfoPower(_tMsg);
					EventRxInfoPowerAck(tPower);
				}
			}
		}
		#endregion
		#region [0x08.0x02] Info TSE
		private void Lib5Z20RxInfoTSE(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x08.0x04] Info Loco
		private void Lib5Z20RxInfoLoco(typeZ20Msg _tMsg)
		{
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
			}
		}
		#endregion
		#region [0x08.0x05] Info BiDi
		private void Lib5Z20RxInfoBiDi(typeZ20Msg _tMsg)
		{
			string strDebug = string.Empty;
			if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
				(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
			{
				typeZ20MsgInfoBiDi32 tBiDi32 = new typeZ20MsgInfoBiDi32(_tMsg);
				strDebug = "\t\t\tBiDi Decode: L=" + tBiDi32.u16DataNId.ToString().PadLeft(5);
				switch ((eNetBiDiType)_tMsg.DataU16Get(2))
				{
					case eNetBiDiType.Speed:            //	Speed
					{
						strDebug += ", Speed";
						if (EventRxInfoBiDiSpeedEvt != null)
						{
							EventRxInfoBiDiSpeedEvt(tBiDi32);
						}
					}
					break;
					case eNetBiDiType.TiltCurve:
					{
						strDebug += ", Tilt";
					}
					break;
					case eNetBiDiType.Config:
					{
						strDebug += ", Config";
					}
					break;
					case eNetBiDiType.QoS:
					{
						strDebug += ", QoS";
						if (EventRxInfoBiDiQoSEvt != null)
						{
							EventRxInfoBiDiQoSEvt(tBiDi32);
						}
					}
					break;
					case eNetBiDiType.Level:
					{
						strDebug += ", Level";
					}
					break;
					case eNetBiDiType.EastWest:
					{
						strDebug += ", East/West";
						if (EventRxInfoBiDiDirectionEvt != null)
						{
							EventRxInfoBiDiDirectionEvt(tBiDi32);
						}
					}
					break;
					case eNetBiDiType.TrackVolt:
					{
						strDebug += ", TrackVolt";
						if (EventRxInfoBiDiTrackVoltEvt != null)
						{
							EventRxInfoBiDiTrackVoltEvt(tBiDi32);
						}
					}
					break;
					case eNetBiDiType.Alarm:
					{
						strDebug += ", Alarm";
					}
					break;
					default:
					{
						strDebug += ", Unknown";
					}
					break;
				}
				if ((strDebug.Length > 0) && (EventLog != null))
				{
					EventLog(strDebug);
				}
			}
		}
		#endregion
		//
		#region [0x08.0x08] Info Data
		private void Lib5Z20RxInfoData(typeZ20Msg _tMsg)
		{
			typeZ20MsgInfoData tInfoData = new typeZ20MsgInfoData(_tMsg);
			string strDebug = "Lib5Z20RxInfoData: DataNId=" + tInfoData.u16NId.ToString("X4") +
							  ", Type=" + tInfoData.u16Typ.ToString("X4") +
							  ", Data=" + tInfoData.u32Val.ToString();
			if ((EventLog != null) && (strDebug != string.Empty))
			{
				EventLog(strDebug);
			}
			if (EventRxInfoData != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxInfoData(tInfoData);
				}
			}
		}
		#endregion
		#region [0x08.0x0A] Info Bin Tag Value
		private void Lib5Z20RxBinTagValue(typeZ20Msg _tMsg)
		{
			if (EventRxBinTagValue != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Cmd) || 
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxBinTagValue(new typeZ20MsgBinTagValue(_tMsg));
				}
			}
		}
		#endregion
		#region [0x08.0x0C] Info Config Docu
		private void Lib5Z20RxInfoCfgDoc(typeZ20Msg _tMsg)
		{
			if (EventRxInfoDocu != null)
			{
				if ((_tMsg.eMode == Lib5Z20Enums.eNetMode.Evt) ||
					(_tMsg.eMode == Lib5Z20Enums.eNetMode.Ack))
				{
					EventRxInfoDocu(_tMsg.DataU16Get(0));
				}
			}
		}
		#endregion
	}
}
