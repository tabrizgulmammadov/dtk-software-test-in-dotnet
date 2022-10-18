using DTKSoftwareInDotnet.Common.Session;
using DTKSoftwareInDotnet.Helpers;
using DTKSoftwareInDotnet.SDK.DTKSoftware;
using Newtonsoft.Json;
using System.Reflection;

namespace DTKSoftwareInDotnet.Services
{
    public class DTKVideoStreamHandler : IBaseVideoStreamHandler
    {
        private readonly string currentDeviceMacAddress;

        public DTKVideoStreamHandler(ISessionService sessionService)
        {
            currentDeviceMacAddress = sessionService.GetCurrentDeviceMacAddress();
        }

        public void Process()
        {
            var configuration = VideoStreamHelper.GetVideoStreamConfiguration();

            LPRParams parameters = new LPRParams();
            parameters.Countries = configuration.CountryCode;
            parameters.MinPlateWidth = 80;
            parameters.MaxPlateWidth = 300;

            // Adding test zone for detecting vechiles in specific rectangle zones
            // (this is test values for creating rectangle zone in left middle side in overall view)
            //var recognitionZoneIndex = parameters.AddZone();
            //var recognitionZonePoint1 = parameters.AddZonePoint(recognitionZoneIndex, 0, 300);
            //var recognitionZonePoint2 = parameters.AddZonePoint(recognitionZoneIndex, 0, 900);
            //var recognitionZonePoint3 = parameters.AddZonePoint(recognitionZoneIndex, 900, 300);
            //var recognitionZonePoint4 = parameters.AddZonePoint(recognitionZoneIndex, 900, 900);

            LPREngine engine = new LPREngine(parameters, true, OnLicensePlateDetected);
            VideoCapture videoCap = new VideoCapture(OnFrameCaptured, OnCaptureError, engine);

            if (!configuration.IsLocaleFile)
                videoCap.StartCaptureFromIPCamera(configuration.Url);

            if (configuration.IsLocaleFile)
                videoCap.StartCaptureFromFile(configuration.Url, 1);
        }

        private void OnFrameCaptured(VideoCapture videoCap, VideoFrame frame, object customObject)
        {
            try
            {
                LPREngine engine = (LPREngine)customObject;
                engine.PutFrame(frame, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error has occured.\nError message: {ex.Message}.\nStaceTrace: {ex.StackTrace}.");
            }
        }

        private void OnLicensePlateDetected(LPREngine engine, LicensePlate plate)
        {
            Console.WriteLine($"Plate text: {plate.Text} Country: {plate.CountryCode} Confidence: {plate.Confidence}\nCoordinates X: {plate.X} Y: {plate.Y}\nHeight: {plate.Height} Width: {plate.Width}\nDeviceMacAddress: {currentDeviceMacAddress}\n\n");

            var rootDirectory = $"{Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location)}/Images/{plate.Text}";

            if (!Directory.Exists(rootDirectory))
                Directory.CreateDirectory(rootDirectory);

            plate.Image.Save($"{rootDirectory}/car_{plate.DateTime:dd-MM-yyyy-HH-mm-ss.fff}__X_{plate.X}_Y_{plate.Y}_height_{plate.Height}_width_{plate.Width}.jpg");
            plate.PlateImage.Save($"{rootDirectory}/plate_{plate.DateTime:dd-MM-yyyy-HH-mm-ss.fff}.jpg");

            plate.Dispose();
        }

        private void OnCaptureError(VideoCapture videoCap, ERR_CAPTURE errorCode, object customObject)
        {
            if (errorCode == ERR_CAPTURE.EOF)
            {
                Console.WriteLine($"Error occured: {JsonConvert.SerializeObject(customObject)}");
            }
            if (errorCode == ERR_CAPTURE.READ_FRAME || errorCode == ERR_CAPTURE.OPEN_VIDEO)
            {
                Console.WriteLine($"Error occured: {JsonConvert.SerializeObject(customObject)}");
            }
        }
    }
}
