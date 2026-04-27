using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib5Z20
{
	public class Lib5Z20Util
	{
		#region Convert Date (Int32U, String, ...)
		public string NetZ20Date2String(UInt32 _u32Date)
		{
			string sDate = string.Empty;
			if (_u32Date > 1000)
			{
				UInt32 u32Calc;
				u32Calc = (_u32Date >> 16) & 0xFFFF;        //	16 ... 31:	Aktuelles RTC Jahr
				sDate = u32Calc.ToString("D4") + ".";
				u32Calc = (_u32Date >> 8) & 0xFF;          //	08 ... 15:	Aktuelle  RTC Monat
				sDate += u32Calc.ToString("D2") + ".";
				u32Calc = (_u32Date >> 0) & 0xFF;          //	08 ... 15:	Aktuelle  RTC Tag
				sDate += u32Calc.ToString("D2");
			}
			return (sDate);
		}
		#endregion
		#region Convert Time (Int32U, String, ...)
		public string NetZ20Time2String(UInt32 _u32Time)
		{
			string sTime = string.Empty;
			if (_u32Time > 1000)
			{
				UInt32 u32Calc;
				u32Calc = (_u32Time >> 24) & 0x001F;		//	24 ... 28:	Aktuelle RTC Stunde
				sTime = u32Calc.ToString("D2") + ":";
				u32Calc = (_u32Time >> 16) & 0x3F;          //	08 ... 15:	Aktuelle  RTC Minute
				sTime += u32Calc.ToString("D2") + ":";
				u32Calc = (_u32Time >> 10) & 0x3F;          //	08 ... 15:	Aktuelle  RTC Secunde
				sTime += u32Calc.ToString("D2");
			}
			return (sTime);
		}
		#endregion
		#region Convert Time (Int32U, String, ...)
		public UInt32 NetZ20Time2UInt32(int _iHour, int _iMin, int _iSec)
		{
			UInt32 u32Calc = 0;
			u32Calc  = (UInt32)(_iHour << 24);		//	24 ... 32:	Stunde
			u32Calc |= (UInt32)(_iMin << 16);		//	16 ... 23:	Minute
			u32Calc |= (UInt32)(_iSec <<  8);       //	08 ... 15:	Secunde
			return (u32Calc);
		}
		#endregion
	}
}
