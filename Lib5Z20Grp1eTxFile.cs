using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lib5Z20.Lib5Z20Enums;

namespace Lib5Z20
{
	public class Lib5Z20Grp1eTxFile
	{
		#region Type Def NAND Label
		public struct LibZ20StructFileNand
		{
			public UInt16 u16LblNId;
			public UInt32 u32LblVal1;
			public UInt32 u32LblVal2;
			public UInt32 u32LblVal3;
			public byte[] u08Text;
			//
			public byte u08Level;
			public UInt16 u16ValMin;
			public UInt16 u16ValMax;
			public byte u08Unit;
			//
			public LibZ20StructFileNand()
			{
				u08Text = new byte[20];
			}
		}
		#endregion
		//
		#region [0x1E.0x07]:	File Close Command
		public typeZ20Msg FileCloseCmd(UInt16 _u16SysNId, UInt16 _u16FNum, UInt16 _u16Type, UInt16 _u16Mode,
									   UInt32 _u32Size, UInt32 _u32CRC, UInt32 _u32Date, UInt32 _u32Time, UInt32 _u32Build)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp1E_File,
											 Lib5Z20Enums.eNetCmd.FileClose,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16SysNId);
			tMsg.AddInt16((UInt16)_u16FNum);
			tMsg.AddInt16((UInt16)_u16Type);
			tMsg.AddInt16((UInt16)_u16Mode);
			tMsg.AddInt32(_u32Size);
			tMsg.AddInt32(_u32CRC);
			tMsg.AddInt32(_u32Date);
			tMsg.AddInt32(_u32Time);
			tMsg.AddInt32(_u32Build);
			return (tMsg);
		}
		public void FileCloseCmd(bool _bWait, UInt16 _u16SysNId, UInt16 _u16FNum, UInt16 _u16Type, UInt16 _u16Mode,
											  UInt32 _u32Size, UInt32 _u32CRC, UInt32 _u32Date, UInt32 _u32Time, UInt32 _u32Build)
		{
			typeZ20Msg tMsg = FileCloseCmd(_u16SysNId, _u16FNum, _u16Type, _u16Mode, _u32Size, _u32CRC, _u32Date, _u32Time, _u32Build);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		public void FileCloseLblCmd(bool _bWait, UInt16 _u16SysNId, UInt16 _u16Language,
											  UInt32 _u32Size, UInt32 _u32CRC, UInt32 _u32Date, UInt32 _u32Time, UInt32 _u32Build)
		{
			typeZ20Msg tMsg = FileCloseCmd(_u16SysNId, (UInt16)enumFileNum.TxtLbl, 0, _u16Language, _u32Size, _u32CRC, _u32Date, _u32Time, _u32Build);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x1E.0x0A]:	File Label Request
		public typeZ20Msg FileLabelReq(UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp1E_File,
											 Lib5Z20Enums.eNetCmd.FileNand,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt32(_u32Tag);
			return (tMsg);
		}
		public void FileLabelReq(bool _bWait, UInt16 _u16SrcNId, UInt32 _u32Tag)
		{
			typeZ20Msg tMsg = FileLabelReq(_u16SrcNId, _u32Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x08.0x0A]:	File Label Command
		public typeZ20Msg FileNandCmd(UInt16 _u16NId, UInt16 _u16Content, UInt32 _u32Size, UInt32 _u32Item)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp1E_File,
											 Lib5Z20Enums.eNetCmd.FileNand,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Content);
			tMsg.AddInt32((UInt32)_u32Size);
			tMsg.AddInt32((UInt32)_u32Item);
			return (tMsg);
		}
		public void FileNandCmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Content, UInt32 _u32Size, UInt32 _u32Item)
		{
			if (_u16NId == 0)
			{
				_u16NId = LibNetZ20.u16ZsNId;
			}
			typeZ20Msg tMsg = FileNandCmd(_u16NId, _u16Content, _u32Size, _u32Item);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		public void FileNandCmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Content, UInt32 _u32Size, UInt32 _u32Item, LibZ20StructFileNand _tLbl)
		{
			if (_u16NId == 0)
			{
				_u16NId = LibNetZ20.u16ZsNId;
			}
			typeZ20Msg tMsg = FileNandCmd(_u16NId, _u16Content, _u32Size, _u32Item);
			tMsg.AddInt16(_tLbl.u16LblNId);
			tMsg.AddInt32(_tLbl.u32LblVal1);
			tMsg.AddInt32(_tLbl.u32LblVal2);
			tMsg.AddInt32(_tLbl.u32LblVal3);
			//
			for (int iCnt = 0; iCnt < 20; iCnt++)               //	Label Text
			{
				tMsg.AddInt08(_tLbl.u08Text[iCnt]);
			}
			tMsg.AddInt16(_tLbl.u16ValMin);
			tMsg.AddInt16(_tLbl.u16ValMax);
			tMsg.AddInt08(_tLbl.u08Unit);					//	Einheit
			tMsg.AddInt08(0);								//	Reserve 1
			tMsg.AddInt08(0);                               //	Reserve 2
			tMsg.AddInt08(0);                               //	Reserve 2
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
