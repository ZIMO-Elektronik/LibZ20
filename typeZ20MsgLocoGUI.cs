using System;
using System.Diagnostics;

namespace Lib5Z20
{
	public class typeZ20MsgLocoGUI
	{
		#region Properties
		public UInt16 u16LocoNId { get; set; }
		public UInt16 u16PartNId { get; set; }
		public UInt32 u32Build { get; set; }
		public UInt16 u16Flags { get; set; }
		public UInt16 u16GroupNId { get; set; }
		public string strLocoName { get; set; } = String.Empty;
		public UInt16 u16ImageId { get; set; }
		public UInt32 u32ImageCRC { get; private set; }
		public UInt16 u16TachoId { get; set; }
		public UInt32 u32TachoCRC { get; private set; }
		public UInt16 u16SpeedFwd { get; set; }
		public UInt16 u16SpeedRev { get; set; }
		public UInt16 u16SpeedRng { get; set; }
		public UInt16 u16Engine { get; set; }
		public UInt16 u16Epoche { get; set; }
		public UInt16 u16Country { get; set; }
		public UInt16[] u16FxIcon { get; private set; }
		public UInt16 u16ZCan20Type { get; set; }
		#endregion
		#region Const's
		public const UInt32 u32BuildGUI0 = 0x00000000;
		public const UInt32 u32BuildGUI1 = 0x01000000;
		#endregion
		//
		#region Create
		public typeZ20MsgLocoGUI()
		{
			u16FxIcon = new UInt16[256];
		}
		public typeZ20MsgLocoGUI(typeZ20Msg _tMsg)
		{
			u16FxIcon = new UInt16[256];
			//
			u16LocoNId = _tMsg.DataU16Get(0);           //	00 ... 01:	Loco NId
			u16PartNId = _tMsg.DataU16Get(2);           //	02 ... 03:	Artikel Nummer
			u32Build = _tMsg.DataU32Get(4);				//	04 ... 07:	GUI Type
														//
			if (u32Build == u32BuildGUI0)
			{
				typeZ20MsgLocoGUI0(_tMsg);
			}
			if (u32Build == u32BuildGUI1)
			{
				typeZ20MsgLocoGUI1(_tMsg);
			}
		}
		public typeZ20MsgLocoGUI(int _iType, typeZ20Msg _tMsg)
		{
			u16FxIcon = new UInt16[256];
			//
			u16LocoNId = _tMsg.DataU16Get(0);			//	00 ... 01:	Loco NId
			u16PartNId = _tMsg.DataU16Get(2);			//	02 ... 03:	Artikel Nummer
			//
			if(_iType == 0)
			{
				typeZ20MsgLocoGUI0(_tMsg);
			}
			if (_iType == 1)
			{
				typeZ20MsgLocoGUI1(_tMsg);
			}
		}
		#region GUI Type '0'
		private void typeZ20MsgLocoGUI0(typeZ20Msg _tMsg)
		{
			u16ZCan20Type = 0;
			strLocoName = _tMsg.DataStringGet(6, 32);
			u16ImageId = _tMsg.DataU16Get(38);      //	38 ... 39:	Fahrzeug Bild
			u16TachoId = _tMsg.DataU16Get(40);      //	40 ... 41:	Fahrzeug Tacho
			u16SpeedFwd = _tMsg.DataU16Get(42);     //	42 ... 43:	V.Max Vorwärts
			u16SpeedRev = _tMsg.DataU16Get(44);     //	44 ... 45:	V.Max Rückwärts
			u16SpeedRng = _tMsg.DataU16Get(46);     //	46 ... 47:	Rangierfahrt
			u16Engine = _tMsg.DataU16Get(48);       //	48 ... 49:	Antrieb
			u16Epoche = _tMsg.DataU16Get(50);       //	50 ... 52:	Epoche
			u16Country = _tMsg.DataU16Get(52);      //	53 ... 54:	Land
													//
			for (UInt16 iFx = 0; iFx < 64; iFx++)
			{
				u16FxIcon[iFx] = _tMsg.DataU16Get((UInt16)(54 + (iFx * 2)));
			}
		}
		#endregion
		#region GUI Type '1'
		private void typeZ20MsgLocoGUI1(typeZ20Msg _tMsg)
		{
			u16ZCan20Type = 1;
			u16Flags = _tMsg.DataU16Get(8);				//	08 ... 09:	Flags
			u16GroupNId = _tMsg.DataU16Get(10);			//	10 ... 11:	Gruppe
			strLocoName = _tMsg.DataStringGet(12, 32);	//	12 ... 43:	Fahrzeug Name
			if (strLocoName == null)
			{
				strLocoName = string.Empty;
			}
			u16ImageId = _tMsg.DataU16Get(44);          //	44 ... 45:	Fahrzeug Bild Num
			u32ImageCRC = _tMsg.DataU32Get(46);         //	46 ... 49:	Fahrzeug Bild CRC
			u16TachoId = _tMsg.DataU16Get(50);          //	50 ... 51:	Fahrzeug Tacho Num
			u32TachoCRC = _tMsg.DataU32Get(52);         //	52 ... 55:	Fahrzeug Tacho CRC
			u16SpeedFwd = _tMsg.DataU16Get(56);         //	56 ... 57:	V.Max Vorwärts
			u16SpeedRev = _tMsg.DataU16Get(58);         //	58 ... 59:	V.Max Rückwärts
			u16SpeedRng = _tMsg.DataU16Get(60);         //	60 ... 61:	V.Max Rangierfahrt
			u16Engine = _tMsg.DataU16Get(62);           //	62 ... 63:	Antrieb
			u16Epoche = _tMsg.DataU16Get(64);           //	64 ... 65:	Epoche
			u16Country = _tMsg.DataU16Get(66);          //	66 ... 67:	Land
			//
			for (UInt16 iFx = 0; iFx < 64; iFx++)
			{
				u16FxIcon[iFx] = _tMsg.DataU16Get((UInt16)(68 + (iFx * 2)));
			}
			/*
				Int16U		u16FxMode[LOCOFX_MAX];		//	Moment/Dauer
			*/
		}
		#endregion
		#endregion
	}
}
