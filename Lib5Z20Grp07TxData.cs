using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Lib5Z20;

namespace Lib5Z20
{
	public class Lib5Z20Grp07TxData
	{
		#region [0x07.0x00]:	Anzahl Abfrage
		public typeZ20Msg ObjCountReq(UInt16 _u16SrcNId, UInt16 _u16GrpNId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.GroupCount,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16GrpNId);
			return (tMsg);
		}
		public bool ObjCountReq(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16GrpNId)
		{
			if (LibNetZ20.bIsOpen)          //	Connection Open ??
			{
				typeZ20Msg tMsg = ObjCountReq(_u16SrcNId, _u16GrpNId);
				return(LibNetZ20.MsgTx(_bWait, tMsg));
			}
			return (false);
		}
		#endregion
		#region [0x07.0x01]:	Object by Index
		public typeZ20Msg ObjByIdxReq(UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.ItemIdx,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16GrpNId);
			tMsg.AddInt16((UInt16)_u16Index);
			return (tMsg);
		}
		public Boolean ObjByIdxReq(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			if (LibNetZ20.bIsOpen)          //	Connection Open ??
			{
				typeZ20Msg tMsg = ObjByIdxReq(_u16SrcNId, _u16GrpNId, _u16Index);
				return (LibNetZ20.MsgTx(_bWait, tMsg));
			}
			return(false);
		}
		#endregion
		#region [0x07.0x02]:	Object by 'Next' NId
		public typeZ20Msg ObjByNIdReq(UInt16 _u16SrcNId, UInt16 _u16ObjNId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.ItemNId,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16ObjNId);
			return (tMsg);
		}
		public Boolean ObjByNIdReq(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16ObjNId)
		{
			typeZ20Msg tMsg = ObjByNIdReq(_u16SrcNId, _u16ObjNId);
			return(LibNetZ20.MsgTx(_bWait, tMsg));
		}
		#endregion
		//
		#region [0x07.0x08]:	Command Data Clear
		public typeZ20Msg CmdDelete(UInt16 _u16SrcNId, UInt16 _u16NId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.DataDel,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16NId);
			return (tMsg);
		}
		public void CmdDelete(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16NId)
		{
			typeZ20Msg tMsg = CmdDelete(_u16SrcNId, _u16NId);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x07.0x12]:	Data Image, Command
		public typeZ20Msg DataImageCmd(UInt16 _u16NId, Lib5Z20Enums.enumImgType _eType, UInt16 _u16ImgSId)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.DataImage,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_eType);
			tMsg.AddInt16((UInt16)_u16ImgSId);
			return (tMsg);
		}
		public void DataImageCmd(bool _bWait, UInt16 _u16NId, Lib5Z20Enums.enumImgType _eType, UInt16 _u16ImgSId)
		{
			typeZ20Msg tMsg = DataImageCmd(_u16NId, _eType, _u16ImgSId);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x07.0x15]:	Data Fx Property Command
		public typeZ20Msg DataFxPropCmd(UInt16 _u16NId, UInt16 _u16Num, UInt16 _u16Typ, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.DataFxData,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Num);
			tMsg.AddInt16((UInt16)_u16Typ);
			tMsg.AddInt16((UInt16)_u16Val);
			return (tMsg);
		}
		public void DataFxPropCmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Num, UInt16 _u16Typ, UInt16 _u16Val)
		{
			typeZ20Msg tMsg = DataFxPropCmd(_u16NId, _u16Num, _u16Typ, _u16Val);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x07.0x12]:	Data Speed Item Cmd
		public typeZ20Msg DataSpeedItemCmd(UInt16 _u16NId, byte _08Tab, byte _u08Item, 
										   UInt16 _u16Step, UInt16 _u16Speed)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp07Data,
											 Lib5Z20Enums.eNetCmd.DataSpeedItem,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16(_u16NId);
			tMsg.AddInt08(_08Tab);
			tMsg.AddInt08(_u08Item);
			tMsg.AddInt16(_u16Step);
			tMsg.AddInt16(_u16Speed);
			return (tMsg);
		}
		public void DataSpeedItemCmd(bool _bWait, UInt16 _u16NId, byte _08Tab, byte _u08Item, 
									 UInt16 _u16Step, UInt16 _u16Speed)
		{
			typeZ20Msg tMsg = DataSpeedItemCmd(_u16NId, _08Tab, _u08Item, _u16Step, _u16Speed);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x17.0x01]:	Object by Index
		public typeZ20Msg XObjByIdxReq(UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.ItemIdx,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16GrpNId);
			tMsg.AddInt16((UInt16)_u16Index);
			return (tMsg);
		}
		public Boolean XObjByIdxReq(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			typeZ20Msg tMsg = XObjByIdxReq(_u16SrcNId, _u16GrpNId, _u16Index);
			return (LibNetZ20.MsgTx(_bWait, tMsg));
		}
		#endregion
		//
		#region [0x17.0x08]:	Abfrage Item DataX
		public typeZ20Msg ItemDataXReg(UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataValue,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Tag);
			return (tMsg);
		}
		public void ItemDataXReg(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tag)
		{
			typeZ20Msg tMsg = ItemDataXReg(_u16SrcNId, _u16NId, _u16Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x17.0x10]:	GUI Name Request
		public typeZ20Msg ItemNameXReq(UInt16 _u16NId, UInt16 _u16Type)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataName,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Type);
			return (tMsg);
		}
		public bool ItemNameXReq(bool _bWait, UInt16 _u16NId, UInt16 _u16Type)
		{
			if (LibNetZ20.bIsOpen)          //	Connection Open ??
			{
				typeZ20Msg tMsg = ItemNameXReq(_u16NId, _u16Type);
				return(LibNetZ20.MsgTx(_bWait, tMsg));
			}
			return (false);
		}
		#endregion
		#region [0x17.0x10]:	GUI Name Command
		public typeZ20Msg ItemNameXCmd(UInt16 _u16NId, UInt16 _u16Type, UInt32 _u32Val1, UInt32 _u32Val2, UInt32 _u32Val3, string _strName)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataName,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Type);
			tMsg.AddInt32((UInt32)_u32Val1);
			tMsg.AddInt32((UInt32)_u32Val2);
			tMsg.AddInt32((UInt32)_u32Val3);
			tMsg.AddString(_strName, 32);
			return (tMsg);
		}
		public bool ItemNameXCmd(bool _bWait, UInt16 _u16NId, UInt16 _u16Type, UInt32 _u32Val1, UInt32 _u32Val2, UInt32 _u32Val3, string _strName)
		{
			if(LibNetZ20.bIsOpen)			//	Connection Open ??
			{
				typeZ20Msg tMsg = ItemNameXCmd(_u16NId, _u16Type, _u32Val1, _u32Val2, _u32Val3, _strName);
				return(LibNetZ20.MsgTx(_bWait, tMsg));
			}
			return (false);
		}
		#endregion
		//
		#region [0x07.0x01]:	Object by Index
		public typeZ20Msg ObjByIdxReqX(UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.ItemIdx,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16GrpNId);
			tMsg.AddInt16((UInt16)_u16Index);
			return (tMsg);
		}
		public Boolean ObjByIdxReqX(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16GrpNId, UInt16 _u16Index)
		{
			if (LibNetZ20.bIsOpen)          //	Connection Open ??
			{
				typeZ20Msg tMsg = ObjByIdxReqX(_u16SrcNId, _u16GrpNId, _u16Index);
				return (LibNetZ20.MsgTx(_bWait, tMsg));
			}
			return (false);
		}
		#endregion
		//
		#region [0x17.0x19]:	Speed Table RequestX
		public typeZ20Msg ItemSpeedTabReqX(UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tab)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataSpeedTab,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt08((byte)_u16Tab);
			return (tMsg);
		}
		public void ItemSpeedTabReqX(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tab)
		{
			typeZ20Msg tMsg = ItemSpeedTabReqX(_u16SrcNId, _u16NId, _u16Tab);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		public void ItemSpeedTabReqX(bool _bWait, UInt16 _u16NId, UInt16 _u16Tab)
		{
			typeZ20Msg tMsg = ItemSpeedTabReqX(0, _u16NId, _u16Tab);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		//
		#region [0x17.0x28]:	Abfrage Item GUIX
		public typeZ20Msg ItemGUIXReq(UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tag)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataObjGUI1,
											 Lib5Z20Enums.eNetMode.Req);
			tMsg.AddInt16((UInt16)_u16SrcNId);
			tMsg.AddInt16((UInt16)_u16NId);
			tMsg.AddInt16((UInt16)_u16Tag);
			return (tMsg);
		}
		public void ItemGUIXReq(bool _bWait, UInt16 _u16SrcNId, UInt16 _u16NId, UInt16 _u16Tag)
		{
			typeZ20Msg tMsg = ItemGUIXReq(_u16SrcNId, _u16NId, _u16Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		public void ItemGUIXReq(bool _bWait, UInt16 _u16NId, UInt16 _u16Tag)
		{
			typeZ20Msg tMsg = ItemGUIXReq(LibNetZ20.u16ZsNId, _u16NId, _u16Tag);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
		#region [0x17.0x27]:	Command Item GUIX Version 1
		public typeZ20Msg CmdItemGUIX(typeZ20MsgLocoGUI _tLocoGUI)
		{
			typeZ20Msg tMsg = new typeZ20Msg(Lib5Z20Enums.eNetGrp.Grp17Data,
											 Lib5Z20Enums.eNetCmd.DataObjGUI1,
											 Lib5Z20Enums.eNetMode.Cmd);
			tMsg.AddInt16((UInt16)_tLocoGUI.u16LocoNId);        //	00..01:	NId für Fahrzeug
			tMsg.AddInt16((UInt16)0);                           //	02..03:	NId für Element
			tMsg.AddInt32((UInt32)0x01010001);                  //	04..ß7:	Version
			tMsg.AddInt16((UInt16)0);							//	08..09:	Flags
			tMsg.AddInt16((UInt16)0);                           //	10..11:	Gruppe
			for(int iCnt = 0; iCnt < 32; iCnt ++)               //	09..41:	Fahrzeug Name
			{
				byte u08Chr = 0;
				if(_tLocoGUI.strLocoName.Length > iCnt)
				{
					if ((_tLocoGUI.strLocoName[iCnt] > 0) && (_tLocoGUI.strLocoName[iCnt] < 0xFF))
					{
						u08Chr = (byte)_tLocoGUI.strLocoName[iCnt];
					}
				}
				tMsg.AddInt08(u08Chr);
			}
			//
			tMsg.AddInt16((UInt16)_tLocoGUI.u16ImageId);			//	38..39:	Image Id  Fahrzeug
			tMsg.AddInt32((UInt16)0);								//	40..43:	Image CRC Fahrzeug
			tMsg.AddInt16((UInt16)_tLocoGUI.u16TachoId);			//	44..45:	Image Id  Tacho
			tMsg.AddInt32((UInt16)0);								//	46..49:	Image CRC Tacho
			tMsg.AddInt16((UInt16)_tLocoGUI.u16SpeedFwd);			//	50..51:	V.Max. Vorwärts
			tMsg.AddInt16((UInt16)_tLocoGUI.u16SpeedFwd);			//	52..53:	V.Max. Rückwärts
			tMsg.AddInt16((UInt16)_tLocoGUI.u16SpeedFwd);			//	54..55:	V.Max. Rangieren
			tMsg.AddInt16((UInt16)_tLocoGUI.u16Engine);				//	Antriebsart
			tMsg.AddInt16((UInt16)_tLocoGUI.u16Epoche);				//	Epoche
			tMsg.AddInt16((UInt16)_tLocoGUI.u16Country);            //	Land
																	//
			int iIconMax = _tLocoGUI.u16FxIcon.Length;
			if(iIconMax > 64)
			{
				iIconMax = 64;
			}
			for (int iCnt = 0; iCnt < iIconMax; iCnt ++)
			{
				tMsg.AddInt16((UInt16)_tLocoGUI.u16FxIcon[iCnt]);   //	Fx Iconst
			}
			for (int iCnt = 0; iCnt < iIconMax; iCnt++)
			{
				tMsg.AddInt32((UInt16)0);                           //	Fx Modes
			}
			return (tMsg);
		}
		public void CmdItemGUIX(bool _bWait, typeZ20MsgLocoGUI _tLocoGUI)
		{
			typeZ20Msg tMsg = CmdItemGUIX(_tLocoGUI);
			LibNetZ20.MsgTx(_bWait, tMsg);
		}
		#endregion
	}
}
