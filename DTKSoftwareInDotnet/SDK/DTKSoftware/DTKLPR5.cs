using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

namespace DTKSoftwareInDotnet.SDK.DTKSoftware
{
    public class DTKLPR5
    {
        const string dllName = "DTKLPR5.dll";
        static DTKLPR5()
        {
            var is64 = IntPtr.Size == 8;
            string assembly_path = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(DTKLPR5)).Location), "SDK\\DTKSoftware\\DLLs");
            string library_path = is64 ? assembly_path + "\\x64\\" : assembly_path + "\\x86\\";

            SetDllDirectory(library_path);
            IntPtr res = LoadLibrary(dllName);
            SetDllDirectory("");
            if (res == IntPtr.Zero)
            {
                LoadLibrary(dllName);
            }
        }

        #region DLL Imports

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        // =====================
        // Callbacks
        // =====================

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void LicensePlateDetectedNative(IntPtr engine, IntPtr plate);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void FrameProcessingCompletedNative(IntPtr engine, long customData, int status);

        // =====================
        // LPREngine
        // =====================
        [DllImport(dllName)]
        public static extern IntPtr LPREngine_Create(IntPtr hParams, [MarshalAs(UnmanagedType.U1)] bool bVideo, IntPtr plateDetectedCallback);

        [DllImport(dllName)]
        public static extern void LPREngine_Destroy(IntPtr hEngine);

        [DllImport(dllName)]
        public static extern void LPREngine_SetFrameProcessingCompletedCallback(IntPtr hEngine, IntPtr prameProcessingCompletedCallback);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr LPREngine_ReadFromFile(IntPtr hEngine, string fileName);

        [DllImport(dllName)]
        public static extern IntPtr LPREngine_ReadFromMemFile(IntPtr hEngine, IntPtr pBuffer, int bufferSize);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr LPREngine_ReadFromURL(IntPtr hEngine, string url);

        [DllImport(dllName)]
        public static extern IntPtr LPREngine_ReadFromImageBuffer(IntPtr hEngine, IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat);

        [DllImport(dllName)]
        public static extern int LPREngine_PutFrameImageBuffer(IntPtr hEngine, IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat, long timestamp, long customData);

        [DllImport(dllName)]
        public static extern int LPREngine_PutFrame(IntPtr hEngine, IntPtr hFrame, long customData);

        [DllImport(dllName)]
        public static extern int LPREngine_GetProcessingFPS(IntPtr hEngine);

        [DllImport(dllName)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LPREngine_IsQueueEmpty(IntPtr hEngine);

        [DllImport(dllName)]
        public static extern int LPREngine_IsLicensed(IntPtr hEngine);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_GetSupportedCountries(StringBuilder buffer, int bufferSize);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_GetLibraryVersion(StringBuilder buffer, int bufferSize);

        // ==================
        // LPRParams
        // ==================
        [DllImport(dllName)]
        public static extern IntPtr LPRParams_Create();

        [DllImport(dllName)]
        public static extern void LPRParams_Destroy(IntPtr hParams);

        [DllImport(dllName)]
        public static extern int LPRParams_get_MinPlateWidth(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_MinPlateWidth(IntPtr hParams, int val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_MaxPlateWidth(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_MaxPlateWidth(IntPtr hParams, int val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_RotateAngle(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_RotateAngle(IntPtr hParams, int val);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPRParams_get_Countries(IntPtr hParams, StringBuilder val, int val_len);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPRParams_set_Countries(IntPtr hParams, string val);

        [DllImport(dllName)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LPRParams_get_FormatPlateText(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_FormatPlateText(IntPtr hParams, [MarshalAs(UnmanagedType.U1)] bool val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_NumThreads(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_NumThreads(IntPtr hParams, int val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_FPSLimit(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_FPSLimit(IntPtr hParams, int val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_DuplicateResultsDelay(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_DuplicateResultsDelay(IntPtr hParams, int val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_ResultConfirmationsCount(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_ResultConfirmationsCount(IntPtr hParams, int val);

        [DllImport(dllName)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LPRParams_get_RecognitionOnMotion(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_RecognitionOnMotion(IntPtr hParams, [MarshalAs(UnmanagedType.U1)] bool val);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPRParams_get_BurnFormatString(IntPtr hParmas, StringBuilder val, int val_len);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPRParams_set_BurnFormatString(IntPtr hParmas, string val);

        [DllImport(dllName)]
        public static extern int LPRParams_get_BurnPosition(IntPtr hParams);

        [DllImport(dllName)]
        public static extern void LPRParams_set_BurnPosition(IntPtr hParams, int val);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPRParams_GetXOption(IntPtr hParmas, string optionName, StringBuilder val, int val_len);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPRParams_SetXOption(IntPtr hParmas, string optionName, string val);

        [DllImport(dllName)]
        public static extern int LPRParams_GetZonesCount(IntPtr hParams);

        [DllImport(dllName)]
        public static extern int LPRParams_AddZone(IntPtr hParams);

        [DllImport(dllName)]
        public static extern int LPRParams_RemoveZone(IntPtr hParams, int zone);

        [DllImport(dllName)]
        public static extern int LPRParams_GetZonePointsCount(IntPtr hParams, int zone);

        [DllImport(dllName)]
        public static extern void LPRParams_GetZonePoint(IntPtr hParams, int zone, int pointNum, out int x, out int y);

        [DllImport(dllName)]
        public static extern void LPRParams_SetZonePoint(IntPtr hParams, int zone, int pointNum, int x, int y);

        [DllImport(dllName)]
        public static extern int LPRParams_AddZonePoint(IntPtr hParams, int zone, int x, int y);

        [DllImport(dllName)]
        public static extern int LPRParams_RemoveZonePoint(IntPtr hParams, int zone, int pointNum);

        [DllImport(dllName)]
        public static extern int LPRParams_GetZonePointsCountF(IntPtr hParams, int zone);

        [DllImport(dllName)]
        public static extern void LPRParams_GetZonePointF(IntPtr hParams, int zone, int pointNum, out float x, out float y);

        [DllImport(dllName)]
        public static extern void LPRParams_SetZonePointF(IntPtr hParams, int zone, int pointNum, float x, float y);

        [DllImport(dllName)]
        public static extern int LPRParams_AddZonePointF(IntPtr hParams, int zone, float x, float y);

        [DllImport(dllName)]
        public static extern int LPRParams_RemoveZonePointF(IntPtr hParams, int zone, int pointNum);

        // ==================
        // LPRResult
        // ==================

        [DllImport(dllName)]
        public static extern void LPRResult_Destroy(IntPtr hResult);

        [DllImport(dllName)]
        public static extern int LPRResult_GetPlatesCount(IntPtr hResult);

        [DllImport(dllName)]
        public static extern IntPtr LPRResult_GetPlate(IntPtr hResult, int index);

        [DllImport(dllName)]
        public static extern int LPRResult_GetProcessingTime(IntPtr hResult);

        // ==================
        // LicensePlate
        // ==================

        [DllImport(dllName)]
        public static extern int LicensePlate_Destroy(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetText(IntPtr hPlate, [MarshalAs(UnmanagedType.LPArray)] byte[] text, int maxLen);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LicensePlate_GetCountryCode(IntPtr hPlate, StringBuilder code, int maxLen);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LicensePlate_GetState(IntPtr hPlate, StringBuilder state, int maxLen);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetType(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetNumRows(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetX(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetY(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetWidth(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetHeight(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolsCount(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolX(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolY(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolWidth(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolHeight(IntPtr hPlate, int index);

        [DllImport(dllName, CharSet = CharSet.Auto)]
        public static extern char LicensePlate_GetSymbol(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolConfidence(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetSymbolRowNum(IntPtr hPlate, int index);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetConfidence(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetZone(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetDirection(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern long LicensePlate_GetTimestamp(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern long LicensePlate_GetFrameTimestamp(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern int LicensePlate_GetDateTimeString(IntPtr hPlate, StringBuilder buffer, int bufferSize);

        [DllImport(dllName)]
        public static extern long LicensePlate_GetId(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern long LicensePlate_GetCustomData(IntPtr hPlate);

        [DllImport(dllName)]
        public static extern void LicensePlate_GetImageBuffer(IntPtr hPlate, out IntPtr pBuffer, out int width, out int height, out int stride);

        [DllImport(dllName)]
        public static extern void LicensePlate_GetPlateImageBuffer(IntPtr hPlate, out IntPtr pBuffer, out int width, out int height, out int stride);

        [DllImport(dllName)]
        public static extern void LicensePlate_FreeImageBuffer(IntPtr pBuffer);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LicensePlate_SaveImage(IntPtr hPlate, string fileName, int param);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LicensePlate_SavePlateImage(IntPtr hPlate, string fileName, int param);

        // ==================
        // License
        // ==================
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_ActivateLicenseOnline(string licenseKey, string comments);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_ActivateLicenseOnlineEx(string licenseKey, string comments, int channels, string security_key);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPREngine_GetActivatedLicenseInfo(StringBuilder licenseKey, int licenseKeyMaxLen, StringBuilder comments, int commentsMaxLen, out int channels, out ulong expirationDate);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPREngine_GetActivatedLicenseInfoEx(StringBuilder licenseKey, int licenseKeyMaxLen, StringBuilder comments, int commentsMaxLen, out int channels, out ulong expirationDate, StringBuilder usbDongleID, int usbDongleIDMaxLen);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_GetSystemID(StringBuilder systemID, int systemIDMaxLen);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_ActivateLicenseOffline(string activationCode);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern int LPREngine_GetActivationLink(string licenseKey, string comments, StringBuilder activationLink, int activationLinkMaxLen);

        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void LPREngine_SetNetLicenseServer(string ip_address, int port);

        [DllImport(dllName)]
        public static extern void LPREngine_ReloadUSBDongles();

        #endregion

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, int count);

        public static Bitmap CreateBitmapFromBuffer(IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat)
        {
            // Create bitmap and copy pixels data
            PixelFormat format;
            if (pixelFormat == PIXFMT.BGR24)
                format = PixelFormat.Format24bppRgb;
            else if (pixelFormat == PIXFMT.RGB24)
                format = PixelFormat.Format24bppRgb;
            else if (pixelFormat == PIXFMT.GRAYSCALE)
                format = PixelFormat.Format8bppIndexed;
            else
                return null;
            Bitmap bmp = new Bitmap(width, height, format);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            IntPtr dst = bmpData.Scan0;
            IntPtr src = pBuffer;
            for (int i = 0; i < height; i++)
            {
                CopyMemory(dst, src, stride);
                dst = new IntPtr(dst.ToInt64() + bmpData.Stride);
                src = new IntPtr(src.ToInt64() + stride);
            }
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }

    public delegate void LicensePlateDetected(LPREngine engine, LicensePlate plate);
    public delegate void FrameProcessingCompleted(LPREngine engine, long customData, int status);

    public enum BURN_POS
    {
        LEFT_TOP = 0,
        RIGHT_TOP = 1,
        LEFT_BOTTOM = 2,
        RIGHT_BOTTOM = 3,
    }

    public enum PLATE_TYPE
    {
        DARK_ON_LIGHT_BKG = 1,
        LIGHT_ON_DARK_BKG = 2,
    }

    /// <summary>
    /// LPRParams class
    /// </summary>
    public class LPRParams
    {
        internal IntPtr hParmas = IntPtr.Zero;

        public LPRParams()
        {
            hParmas = DTKLPR5.LPRParams_Create();
        }

        ~LPRParams()
        {
            DTKLPR5.LPRParams_Destroy(hParmas);
        }

        public int MinPlateWidth
        {
            get
            {
                return DTKLPR5.LPRParams_get_MinPlateWidth(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_MinPlateWidth(hParmas, value);
            }
        }

        public int MaxPlateWidth
        {
            get
            {
                return DTKLPR5.LPRParams_get_MaxPlateWidth(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_MaxPlateWidth(hParmas, value);
            }
        }
        public bool FormatPlateText
        {
            get
            {
                return DTKLPR5.LPRParams_get_FormatPlateText(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_FormatPlateText(hParmas, value);
            }
        }

        public int RotateAngle
        {
            get
            {
                return DTKLPR5.LPRParams_get_RotateAngle(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_RotateAngle(hParmas, value);
            }
        }

        public int FPSLimit
        {
            get
            {
                return DTKLPR5.LPRParams_get_FPSLimit(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_FPSLimit(hParmas, value);
            }
        }

        public int DuplicateResultsDelay
        {
            get
            {
                return DTKLPR5.LPRParams_get_DuplicateResultsDelay(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_DuplicateResultsDelay(hParmas, value);
            }
        }

        public bool RecognitionOnMotion
        {
            get
            {
                return DTKLPR5.LPRParams_get_RecognitionOnMotion(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_RecognitionOnMotion(hParmas, value);
            }
        }

        public int ResultConfirmationsCount
        {
            get
            {
                return DTKLPR5.LPRParams_get_ResultConfirmationsCount(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_ResultConfirmationsCount(hParmas, value);
            }
        }

        public int NumThreads
        {
            get
            {
                return DTKLPR5.LPRParams_get_NumThreads(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_NumThreads(hParmas, value);
            }
        }

        public string BurnFormatString
        {
            get
            {
                int size = DTKLPR5.LPRParams_get_BurnFormatString(hParmas, null, 0);
                StringBuilder builder = new StringBuilder(size + 1);
                DTKLPR5.LPRParams_get_BurnFormatString(hParmas, builder, builder.Capacity);
                return builder.ToString();
            }
            set
            {
                DTKLPR5.LPRParams_set_BurnFormatString(hParmas, value);
            }
        }

        public int BurnPosition
        {
            get
            {
                return DTKLPR5.LPRParams_get_BurnPosition(hParmas);
            }
            set
            {
                DTKLPR5.LPRParams_set_BurnPosition(hParmas, value);
            }
        }

        public int GetZonesCount()
        {
            return DTKLPR5.LPRParams_GetZonesCount(hParmas);
        }

        public int AddZone()
        {
            return DTKLPR5.LPRParams_AddZone(hParmas);
        }

        public void RemoveZone(int zoneIndex)
        {
            DTKLPR5.LPRParams_RemoveZone(hParmas, zoneIndex);
        }

        public int GetZonePointsCount(int zoneIndex)
        {
            return DTKLPR5.LPRParams_GetZonePointsCount(hParmas, zoneIndex);
        }

        public void GetZonePoint(int zoneIndex, int pointIndex, out int x, out int y)
        {
            DTKLPR5.LPRParams_GetZonePoint(hParmas, zoneIndex, pointIndex, out x, out y);
        }
        public void SetZonePoint(int zoneIndex, int pointIndex, int x, int y)
        {
            DTKLPR5.LPRParams_SetZonePoint(hParmas, zoneIndex, pointIndex, x, y);
        }

        public int AddZonePoint(int zoneIndex, int x, int y)
        {
            return DTKLPR5.LPRParams_AddZonePoint(hParmas, zoneIndex, x, y);
        }

        public int RemoveZonePoint(int zoneIndex, int pointIndex)
        {
            return DTKLPR5.LPRParams_RemoveZonePoint(hParmas, zoneIndex, pointIndex);
        }

        public int GetZonePointsCountF(int zoneIndex)
        {
            return DTKLPR5.LPRParams_GetZonePointsCountF(hParmas, zoneIndex);
        }

        public void GetZonePointF(int zoneIndex, int pointIndex, out float x, out float y)
        {
            DTKLPR5.LPRParams_GetZonePointF(hParmas, zoneIndex, pointIndex, out x, out y);
        }
        public void SetZonePointF(int zoneIndex, int pointIndex, float x, float y)
        {
            DTKLPR5.LPRParams_SetZonePointF(hParmas, zoneIndex, pointIndex, x, y);
        }

        public int AddZonePointF(int zoneIndex, float x, float y)
        {
            return DTKLPR5.LPRParams_AddZonePointF(hParmas, zoneIndex, x, y);
        }

        public int RemoveZonePointF(int zoneIndex, int pointIndex)
        {
            return DTKLPR5.LPRParams_RemoveZonePointF(hParmas, zoneIndex, pointIndex);
        }

        public string Countries
        {
            get
            {
                int size = DTKLPR5.LPRParams_get_Countries(hParmas, null, 0);
                if (size > 0)
                {
                    StringBuilder builder = new StringBuilder(size + 1);
                    DTKLPR5.LPRParams_get_Countries(hParmas, builder, builder.Capacity);
                    return builder.ToString();
                }
                return "";
            }
            set
            {
                DTKLPR5.LPRParams_set_Countries(hParmas, value);
            }
        }


        public void SetXOption(string optionName, string val)
        {
            DTKLPR5.LPRParams_SetXOption(hParmas, optionName, val);
        }

        public string GetXOption(string optionName)
        {
            StringBuilder builder = new StringBuilder(255);
            DTKLPR5.LPRParams_GetXOption(hParmas, optionName, builder, 255);
            return builder.ToString();
        }

    }

    /// <summary>
    /// LicensePlate class
    /// </summary>
    public class LicensePlate : IDisposable
    {
        internal IntPtr hPlate = IntPtr.Zero;

        internal LicensePlate(IntPtr hPlate)
        {
            this.hPlate = hPlate;
        }
        ~LicensePlate()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
            }
            else
            {
            }
            if (hPlate != IntPtr.Zero)
            {
                DTKLPR5.LicensePlate_Destroy(hPlate);
                hPlate = IntPtr.Zero;
            }
        }

        public DateTime DateTime
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Timestamp).ToLocalTime();
            }
        }

        public string Text
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return "";
                byte[] data = new byte[32];
                int size = DTKLPR5.LicensePlate_GetText(hPlate, data, data.Length);
                return Encoding.UTF8.GetString(data, 0, size);
            }
        }

        public string CountryCode
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return "";
                StringBuilder builder = new StringBuilder(8);
                DTKLPR5.LicensePlate_GetCountryCode(hPlate, builder, 8);
                return builder.ToString();
            }
        }
        public string State
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return "";
                StringBuilder builder = new StringBuilder(8);
                DTKLPR5.LicensePlate_GetState(hPlate, builder, 8);
                return builder.ToString();
            }
        }

        public PLATE_TYPE Type
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return (PLATE_TYPE)DTKLPR5.LicensePlate_GetType(hPlate);
            }
        }

        public int NumRows
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetNumRows(hPlate);
            }
        }

        public int X
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetX(hPlate);
            }
        }

        public int Y
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetY(hPlate);
            }
        }

        public int Width
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetWidth(hPlate);
            }
        }

        public int Height
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetHeight(hPlate);
            }
        }

        public int GetSymbolsCount()
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolsCount(hPlate);
        }

        public int GetSymbolX(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolX(hPlate, index);
        }

        public int GetSymbolY(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolY(hPlate, index);
        }

        public int GetSymbolWidth(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolWidth(hPlate, index);
        }

        public int GetSymbolHeight(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolHeight(hPlate, index);
        }

        public char GetSymbol(int index)
        {
            if (hPlate == IntPtr.Zero)
                return '?';
            return DTKLPR5.LicensePlate_GetSymbol(hPlate, index);
        }

        public int GetSymbolConfidence(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolConfidence(hPlate, index);
        }

        public int GetSymbolRowNum(int index)
        {
            if (hPlate == IntPtr.Zero)
                return 0;
            return DTKLPR5.LicensePlate_GetSymbolRowNum(hPlate, index);
        }

        public int Confidence
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetConfidence(hPlate);
            }
        }

        public int Zone
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetZone(hPlate);
            }
        }

        public int Direction
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetDirection(hPlate);
            }
        }

        public long Timestamp
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetTimestamp(hPlate);
            }
        }

        public long FrameTimestamp
        {
            get
            {
                return DTKLPR5.LicensePlate_GetFrameTimestamp(hPlate);
            }
        }

        public string DateTimeString
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return "";
                int size = DTKLPR5.LicensePlate_GetDateTimeString(hPlate, null, 0);
                StringBuilder builder = new StringBuilder(size);
                DTKLPR5.LicensePlate_GetDateTimeString(hPlate, builder, size);
                return builder.ToString();
            }
        }

        public long Id
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetId(hPlate);
            }
        }

        public long CustomData
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return 0;
                return DTKLPR5.LicensePlate_GetCustomData(hPlate);
            }
        }

        public int SaveImage(string fileName, int param)
        {
            return DTKLPR5.LicensePlate_SaveImage(hPlate, fileName, param);
        }

        public int SavePlateImage(string fileName, int param)
        {
            return DTKLPR5.LicensePlate_SavePlateImage(hPlate, fileName, param);
        }

        public Image Image
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return null;
                IntPtr pBuffer;
                int width;
                int height;
                int stride;
                DTKLPR5.LicensePlate_GetImageBuffer(hPlate, out pBuffer, out width, out height, out stride);
                if (pBuffer == IntPtr.Zero)
                    return null;
                Bitmap bmp = DTKLPR5.CreateBitmapFromBuffer(pBuffer, width, height, stride, PIXFMT.RGB24);
                DTKLPR5.LicensePlate_FreeImageBuffer(pBuffer);
                return bmp;
            }
        }

        public Image PlateImage
        {
            get
            {
                if (hPlate == IntPtr.Zero)
                    return null;
                IntPtr pBuffer;
                int width;
                int height;
                int stride;
                DTKLPR5.LicensePlate_GetPlateImageBuffer(hPlate, out pBuffer, out width, out height, out stride);
                if (pBuffer == IntPtr.Zero)
                    return null;
                Bitmap bmp = DTKLPR5.CreateBitmapFromBuffer(pBuffer, width, height, stride, PIXFMT.RGB24);
                DTKLPR5.LicensePlate_FreeImageBuffer(pBuffer);
                return bmp;
            }
        }
    }

    /// <summary>
    /// LPREngine class
    /// </summary>
    public class LPREngine : IDisposable
    {
        internal IntPtr hEngine = IntPtr.Zero;

        private LicensePlateDetected licensePlateDetected_callback = null;
        private FrameProcessingCompleted frameProcessingCompleted_callback = null;
        private DTKLPR5.LicensePlateDetectedNative licensePlateDetected_callback_native = null;
        private DTKLPR5.FrameProcessingCompletedNative frameProcessingCompleted_callback_native = null;

        private LPRParams parameters = null;

        public LPREngine(LPRParams parameters, bool bVideo, LicensePlateDetected callback)
        {
            licensePlateDetected_callback = callback;
            this.parameters = parameters;
            licensePlateDetected_callback_native = new DTKLPR5.LicensePlateDetectedNative(OnLicensePlateDetectedNative);
            frameProcessingCompleted_callback_native = new DTKLPR5.FrameProcessingCompletedNative(OnFrameProcessingCompletedNative);
            hEngine = DTKLPR5.LPREngine_Create(parameters.hParmas, bVideo, Marshal.GetFunctionPointerForDelegate(licensePlateDetected_callback_native));
        }
        ~LPREngine()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
            }
            else
            {
            }
            if (hEngine != IntPtr.Zero)
            {
                DTKLPR5.LPREngine_Destroy(hEngine);
                hEngine = IntPtr.Zero;
            }
        }

        public void SetFrameProcessingCompletedCallback(FrameProcessingCompleted callback)
        {
            DTKLPR5.LPREngine_SetFrameProcessingCompletedCallback(hEngine, Marshal.GetFunctionPointerForDelegate(frameProcessingCompleted_callback_native));
            frameProcessingCompleted_callback = callback;
        }

        private void OnLicensePlateDetectedNative(IntPtr engine, IntPtr hPlate)
        {
            LicensePlate plate = new LicensePlate(hPlate);
            if (licensePlateDetected_callback != null)
                licensePlateDetected_callback(this, plate);
        }

        private void OnFrameProcessingCompletedNative(IntPtr engine, long customData, int status)
        {
            if (frameProcessingCompleted_callback != null)
                frameProcessingCompleted_callback(this, customData, status);
        }

        public int GetProcessingFPS()
        {
            return DTKLPR5.LPREngine_GetProcessingFPS(hEngine);
        }

        public bool IsQueueEmpty
        {
            get
            {
                return DTKLPR5.LPREngine_IsQueueEmpty(hEngine);
            }
        }

        public int IsLicensed
        {
            get
            {
                return DTKLPR5.LPREngine_IsLicensed(hEngine);
            }
        }

        public List<LicensePlate> ReadFromFile(string fileName)
        {
            int dummy;
            return ReadFromFile(fileName, out dummy);
        }
        public List<LicensePlate> ReadFromFile(string fileName, out int processingTime)
        {
            List<LicensePlate> res = new List<LicensePlate>();

            IntPtr hResult = DTKLPR5.LPREngine_ReadFromFile(hEngine, fileName);
            if (hResult == IntPtr.Zero)
                throw new Exception("Error reading image");
            int count = DTKLPR5.LPRResult_GetPlatesCount(hResult);
            for (int i = 0; i < count; i++)
            {
                IntPtr hPlate = DTKLPR5.LPRResult_GetPlate(hResult, i);
                res.Add(new LicensePlate(hPlate));
            }
            processingTime = DTKLPR5.LPRResult_GetProcessingTime(hResult);
            DTKLPR5.LPRResult_Destroy(hResult);
            return res;
        }

        public List<LicensePlate> ReadFromMemFile(IntPtr pBuffer, int bufferSize)
        {
            int dummy;
            return ReadFromMemFile(pBuffer, bufferSize, out dummy);
        }

        public List<LicensePlate> ReadFromMemFile(IntPtr pBuffer, int bufferSize, out int processingTime)
        {
            List<LicensePlate> res = new List<LicensePlate>();
            IntPtr hResult = DTKLPR5.LPREngine_ReadFromMemFile(hEngine, pBuffer, bufferSize);
            if (hResult == IntPtr.Zero)
                throw new Exception("Error reading image");
            int count = DTKLPR5.LPRResult_GetPlatesCount(hResult);
            for (int i = 0; i < count; i++)
            {
                IntPtr hPlate = DTKLPR5.LPRResult_GetPlate(hResult, i);
                res.Add(new LicensePlate(hPlate));
            }
            processingTime = DTKLPR5.LPRResult_GetProcessingTime(hResult);
            DTKLPR5.LPRResult_Destroy(hResult);
            return res;
        }

        public List<LicensePlate> ReadFromURL(string url)
        {
            int dummy;
            return ReadFromURL(url, out dummy);
        }

        public List<LicensePlate> ReadFromURL(string url, out int processingTime)
        {
            List<LicensePlate> res = new List<LicensePlate>();
            IntPtr hResult = DTKLPR5.LPREngine_ReadFromURL(hEngine, url);
            if (hResult == IntPtr.Zero)
                throw new Exception("Error reading image");
            int count = DTKLPR5.LPRResult_GetPlatesCount(hResult);
            for (int i = 0; i < count; i++)
            {
                IntPtr hPlate = DTKLPR5.LPRResult_GetPlate(hResult, i);
                res.Add(new LicensePlate(hPlate));
            }
            processingTime = DTKLPR5.LPRResult_GetProcessingTime(hResult);
            DTKLPR5.LPRResult_Destroy(hResult);
            return res;
        }

        public List<LicensePlate> ReadFromImageBuffer(IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat)
        {
            int dummy;
            return ReadFromImageBuffer(pBuffer, width, height, stride, pixelFormat, out dummy);
        }

        public List<LicensePlate> ReadFromImageBuffer(IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat, out int processingTime)
        {
            List<LicensePlate> res = new List<LicensePlate>();
            IntPtr hResult = DTKLPR5.LPREngine_ReadFromImageBuffer(hEngine, pBuffer, width, height, stride, pixelFormat);
            if (hResult == IntPtr.Zero)
                throw new Exception("Error reading image");
            int count = DTKLPR5.LPRResult_GetPlatesCount(hResult);
            for (int i = 0; i < count; i++)
            {
                IntPtr hPlate = DTKLPR5.LPRResult_GetPlate(hResult, i);
                res.Add(new LicensePlate(hPlate));
            }
            processingTime = DTKLPR5.LPRResult_GetProcessingTime(hResult);
            DTKLPR5.LPRResult_Destroy(hResult);
            return res;
        }

        public int PutFrame(VideoFrame frame, long customData)
        {
            return DTKLPR5.LPREngine_PutFrame(hEngine, frame.hFrame, customData);
        }

        public int PutFrameImageBuffer(IntPtr pBuffer, int width, int height, int stride, PIXFMT pixelFormat, long timestamp, long customData)
        {
            return DTKLPR5.LPREngine_PutFrameImageBuffer(hEngine, pBuffer, width, height, stride, pixelFormat, timestamp, customData);
        }

        public static List<string> GetSupportedCountries()
        {
            int required_size = DTKLPR5.LPREngine_GetSupportedCountries(null, 0);
            StringBuilder builder = new StringBuilder(required_size);
            DTKLPR5.LPREngine_GetSupportedCountries(builder, required_size);
            List<string> res = new List<string>();
            foreach (string item in builder.ToString().Split(';'))
            {
                res.Add(item);
            }
            return res;
        }

        public static string GetLibraryVersion()
        {
            int required_size = DTKLPR5.LPREngine_GetLibraryVersion(null, 0);
            StringBuilder builder = new StringBuilder(required_size);
            DTKLPR5.LPREngine_GetLibraryVersion(builder, required_size);
            return builder.ToString();
        }

        static public int ActivateLicenseOnline(string licenseKey, string comments)
        {
            return DTKLPR5.LPREngine_ActivateLicenseOnline(licenseKey, comments);
        }
        static public int ActivateLicenseOnlineEx(string licenseKey, string comments, int channels, string security_key)
        {
            return DTKLPR5.LPREngine_ActivateLicenseOnlineEx(licenseKey, comments, channels, security_key);
        }
        static public void GetActivatedLicenseInfo(out string licenseKey, out string comments, out int channels, out DateTime expirationDate)
        {
            string dummy;
            GetActivatedLicenseInfo(out licenseKey, out comments, out channels, out expirationDate, out dummy);
        }
        static public void GetActivatedLicenseInfo(out string licenseKey, out string comments, out int channels, out DateTime expirationDate, out string usbDongleID)
        {
            StringBuilder licKey = new StringBuilder(512);
            StringBuilder comm = new StringBuilder(255);
            StringBuilder dongleID = new StringBuilder(255);
            ulong expDate_time_t;

            DTKLPR5.LPREngine_GetActivatedLicenseInfoEx(licKey, 512, comm, 255, out channels, out expDate_time_t, dongleID, 255);

            if (expDate_time_t != 0)
            {
                expirationDate = new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(expDate_time_t);
                if (expirationDate.Year < 2000)
                    expirationDate = DateTime.MaxValue;
            }
            else
            {
                expirationDate = DateTime.MaxValue;
            }
            licenseKey = licKey.ToString();
            comments = comm.ToString();
            usbDongleID = dongleID.ToString();
        }

        static public string GetSystemID()
        {
            StringBuilder sysID = new StringBuilder(50);
            DTKLPR5.LPREngine_GetSystemID(sysID, 50);
            return sysID.ToString();
        }

        static public int ActivateLicenseOffline(string activationCode)
        {
            return DTKLPR5.LPREngine_ActivateLicenseOffline(activationCode);
        }

        static public string GetActivationLink(string licenseKey, string comments)
        {
            StringBuilder actLink = new StringBuilder(255);
            if (DTKLPR5.LPREngine_GetActivationLink(licenseKey, comments, actLink, 255) > 0)
            {
                return actLink.ToString();
            }
            return "";
        }

        static public void SetNetLicenseServer(string ip_address, int port)
        {
            DTKLPR5.LPREngine_SetNetLicenseServer(ip_address, port);
        }

        static public void ReloadUSBDongles()
        {
            DTKLPR5.LPREngine_ReloadUSBDongles();
        }
    }


}
