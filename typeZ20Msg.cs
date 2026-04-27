using System;


namespace Lib5Z20
{
	public class typeZ20Msg
	{
		#region Variables
		private string sSrcAddr = string.Empty;
		#endregion
		//
		#region Get/Set:	Net Info
		public Boolean bWaitOk { get; set; }
		public DateTime dtTimeStamp { get; set; }
		public string strSrcAddr { get; set; }
		public int iSrcPort { get; set; }
		public int iSize { get; set; }
		#endregion
		#region Get/Set:	Net Header
		public Lib5Z20Enums.eNetGrp eGrp { get; set; }
		public Lib5Z20Enums.eNetCmd eCmd { get; set; }
		public Lib5Z20Enums.eNetMode eMode { get; set; }
		public UInt16 u16GrpCmdMode
		{
			get
			{
				UInt16 u16Return = (UInt16)eGrp;
				u16Return <<= 6;
				u16Return |= (UInt16)eCmd;
				u16Return <<= 2;
				u16Return |= (UInt16)eMode;
				return (u16Return);
			}
		}
		public UInt16 u16HdrNId { get; set; }
		#endregion
		//
		#region Get/Set:	Data Index
		public UInt16 iDataIdx { get; set; }
		#endregion
		#region Get/Set:	CRC 32
		public UInt32 iCRC32 { get; set; }
		#endregion
		#region Get/Set:	DataByte
		public byte[] u08Data { get; set; }
		#endregion
		#region Get/Set:	DataInt16
		public void DataU16Set(UInt16 _uOffset, UInt16 _iData)
		{
			u08Data[_uOffset + 0] = (byte)((_iData >> 0) & 0xFF);
			u08Data[_uOffset + 1] = (byte)((_iData >> 8) & 0xFF);
		}
		public UInt16 DataU16Get(UInt16 _uOffset)
		{
			UInt16 uTemp;
			uTemp = (UInt16)((u08Data[_uOffset + 0] << 0) & 0x00FF);
			uTemp |= (UInt16)((u08Data[_uOffset + 1] << 8) & 0xFF00);
			return (uTemp);
		}
		#endregion
		#region Get/Set:	DataInt32
		public void DataU32Set(UInt16 _uOffset, UInt32 _iData)
		{
			u08Data[_uOffset + 0] = (byte)((_iData >> 0) & 0xFF);
			u08Data[_uOffset + 1] = (byte)((_iData >> 8) & 0xFF);
			u08Data[_uOffset + 2] = (byte)((_iData >> 16) & 0xFF);
			u08Data[_uOffset + 3] = (byte)((_iData >> 24) & 0xFF);
		}
		public UInt32 DataU32Get(UInt16 _uOffset)
		{
			UInt32 iTemp;
			iTemp = (UInt32)((u08Data[_uOffset + 0] << 0) & 0x000000FF);
			iTemp |= (UInt32)((u08Data[_uOffset + 1] << 8) & 0x0000FF00);
			iTemp |= (UInt32)((u08Data[_uOffset + 2] << 16) & 0x00FF0000);
			iTemp |= (UInt32)((u08Data[_uOffset + 3] << 24) & 0xFF000000);
			return (iTemp);
		}
		#endregion
		#region Get/Set:	DataInt64
		public void DataU64Set(UInt16 _uOffset, UInt64 _u64Data)
		{
			u08Data[_uOffset + 0] = (byte)((_u64Data >> 0) & 0xFF);
			u08Data[_uOffset + 1] = (byte)((_u64Data >> 8) & 0xFF);
			u08Data[_uOffset + 2] = (byte)((_u64Data >> 16) & 0xFF);
			u08Data[_uOffset + 3] = (byte)((_u64Data >> 24) & 0xFF);
			u08Data[_uOffset + 4] = (byte)((_u64Data >> 32) & 0xFF);
			u08Data[_uOffset + 5] = (byte)((_u64Data >> 40) & 0xFF);
			u08Data[_uOffset + 6] = (byte)((_u64Data >> 48) & 0xFF);
			u08Data[_uOffset + 7] = (byte)((_u64Data >> 56) & 0xFF);
		}
		public UInt64 DataU64Get(UInt16 _uOffset)
		{
			UInt64 u64Temp1;
			UInt64 u64Temp2;
			//
			u64Temp1 = (UInt32)((u08Data[_uOffset + 0] << 0) & 0x000000FF);
			u64Temp1 |= (UInt32)((u08Data[_uOffset + 1] << 8) & 0x0000FF00);
			u64Temp1 |= (UInt32)((u08Data[_uOffset + 2] << 16) & 0x00FF0000);
			u64Temp1 |= (UInt32)((u08Data[_uOffset + 3] << 24) & 0xFF000000);
			//
			u64Temp2 = (UInt32)((u08Data[_uOffset + 4] << 0) & 0x000000FF);
			u64Temp2 |= (UInt32)((u08Data[_uOffset + 5] << 8) & 0x0000FF00);
			u64Temp2 |= (UInt32)((u08Data[_uOffset + 6] << 16) & 0x00FF0000);
			u64Temp2 |= (UInt32)((u08Data[_uOffset + 7] << 24) & 0xFF000000);
			u64Temp2 <<= 32;
			//
			return (u64Temp1 | u64Temp2);
		}
		#endregion
		#region Get/Set:	String
		public void DataStringSet(UInt16 _uOffset, UInt32 _iData)
		{
		}
		public string DataStringGet(UInt16 _uOffset, int _iLen)
		{
			for(int iCnt = 0; iCnt < _iLen; iCnt++)
			{
				if (u08Data[_uOffset + iCnt] == 0x00)
				{
					_iLen = iCnt;
					break;
				}
			}
			string sTemp = System.Text.Encoding.UTF8.GetString(u08Data, _uOffset, _iLen);
			return (sTemp);
		}
		#endregion
		//
		#region Create
		public typeZ20Msg()
		{
			dtTimeStamp = DateTime.Now;
			iSize = 0;
			eMode = Lib5Z20Enums.eNetMode.Req;
			eGrp = Lib5Z20Enums.eNetGrp.GrpNONE;
			eCmd = Lib5Z20Enums.eNetCmd.NONE;
			u16HdrNId = 0;
			u08Data = new byte[1024];
			for (int iCnt = 0; iCnt < 1024; iCnt++)
			{
				u08Data[iCnt] = 0;
			}
//			m_iCRC = 0;
		}
		public typeZ20Msg(Lib5Z20Enums.eNetGrp _eGrp,
						  Lib5Z20Enums.eNetCmd _eCmd,
						  Lib5Z20Enums.eNetMode _eMode) : this()
		{
			eMode = _eMode;
			eGrp = _eGrp;
			eCmd = _eCmd;
			u16HdrNId = 0;
		}
		public typeZ20Msg(Lib5Z20Enums.eNetGrp _eGrp,
						  Lib5Z20Enums.eNetCmd _eCmd,
						  Lib5Z20Enums.eNetMode _eMode,
						  UInt16 _u16TxNId) : this()
		{
			eMode = _eMode;
			eGrp = _eGrp;
			eCmd = _eCmd;
			u16HdrNId = _u16TxNId;
		}
		public typeZ20Msg(Lib5Z20Enums.eNetMode _eMode,
						  Lib5Z20Enums.eNetGrp _eGrp,
						  UInt16 _iFileNum, UInt16 _iByteIdx)
		{
			eMode = _eMode;
			eGrp = _eGrp;
			iDataIdx = _iByteIdx;
			u16HdrNId = _iFileNum;
		}
		#endregion
		//
		#region Clear
		public void Clear()
		{
			bWaitOk = false;
			dtTimeStamp = DateTime.MinValue;
			eGrp = Lib5Z20Enums.eNetGrp.GrpNONE;
			eCmd = Lib5Z20Enums.eNetCmd.NONE;
			eMode = 0;
			for (int iCnt = 0; iCnt < 256; iCnt++)
			{
				u08Data[iCnt] = 0;
			}
		}
		#endregion
		#region Copy
		public void Copy(typeZ20Msg _cSource)
		{
			if(_cSource.iSize >= 1024)
			{
				return;
			}
			dtTimeStamp = _cSource.dtTimeStamp;
			iSize = _cSource.iSize;
			eGrp = _cSource.eGrp;
			eCmd = _cSource.eCmd;
			eMode = _cSource.eMode;
			for (int iCnt = 0; iCnt < _cSource.iSize; iCnt++)
			{
				u08Data[iCnt] = _cSource.u08Data[iCnt];
			}
		}
		#endregion
		#region Equal
		public override bool Equals(object obj)
		{
			if (!(obj is typeZ20Msg))
			{
				return false;
			}
			typeZ20Msg tTest = (typeZ20Msg)obj;
			if ((this.iSize != tTest.iSize) ||
				(this.eGrp != tTest.eGrp) ||
				(this.eCmd != tTest.eCmd) ||
				(this.eMode != tTest.eMode))
			{
				return(false);
			}
			for (int iCnt = 0; iCnt < this.iSize; iCnt++)
			{
				if (this.u08Data[iCnt] != tTest.u08Data[iCnt])
				{
					return (false);
				}
			}
			return (true);
		}
		public static bool operator ==(typeZ20Msg tMsg1, typeZ20Msg tMsg2)
		{
			return (tMsg1.Equals(tMsg2));
		}
		public static bool operator !=(typeZ20Msg tMsg1, typeZ20Msg tMsg2)
		{
			return (!(tMsg1.Equals(tMsg2)));
		}
		#endregion
		//
		#region Data
		#region Data:	Add Byte
		public void AddInt08(Byte _iData)
		{
			u08Data[iSize++] = _iData;
		}
		#endregion
		#region Data:	Add Int16
		public void AddInt16(UInt16 _iData)
		{
			u08Data[iSize++] = (Byte)((_iData >> 0) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 8) & 0xFF);
		}
		#endregion
		#region Data:	Add Int32
		public void AddInt32(UInt32 _iData)
		{
			u08Data[iSize++] = (Byte)((_iData >> 0) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 8) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 16) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 24) & 0xFF);
		}
		#endregion
		#region Data:	Add Int32
		public void AddInt64(UInt64 _iData)
		{
			u08Data[iSize++] = (Byte)((_iData >> 0) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 8) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 16) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 24) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 32) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 40) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 48) & 0xFF);
			u08Data[iSize++] = (Byte)((_iData >> 56) & 0xFF);
		}
		#endregion
		#region Data:	Add String
		public void AddString(string _sText, int _iSize)
		{
			foreach (byte u08Text in _sText)
			{
				u08Data[iSize++] = u08Text;
				_iSize--;
			}
			if(_iSize > 1)
			{
				for (int iCnt = 0; iCnt < _iSize; iCnt++)
				{
					u08Data[iSize++] = 0x00;
				}
			}
			//
			u08Data[iSize++] = 0x00;
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
		#endregion
		#endregion
	}
	#region Net Message as EventArgs
	public class EvtNetRxArgs : EventArgs
	{
		private typeZ20Msg cMsg { get; set; }
		//
		public EvtNetRxArgs(typeZ20Msg _cMsg)
		{
			cMsg = _cMsg;
		}
	}
	#endregion
	//
	#region Sys Message
	public class typeZ20MsgSystemState
	{
		#region Enums
		public enum enumSysState
		{
			None = 0,
			Run = 1,
			SSP0 = 2,
			SSPE = 3,
			PcIfOff = 4,
			SProg = 5,
			MaxI = 10,
			MinU = 11,
			PrgMaxI = 12,
			PwrOff = 13,
		}
		#endregion
		#region Properties
		public UInt16 u16NId { get; private set; }
		public byte u08Pin { get; private set; }
		public enumSysState eState { get; private set; }
		#endregion
		#region Create
		public typeZ20MsgSystemState()
		{
		}
		public typeZ20MsgSystemState(typeZ20Msg _tMsg)
		{
			u16NId = _tMsg.DataU16Get(0);				//	00 ... 01:	System NId
			u08Pin = _tMsg.u08Data[2];					//	02:			Output Pin
			eState = (enumSysState)_tMsg.u08Data[3];	//	03:			Pin State
		}
		#endregion
	}
	#endregion
}
