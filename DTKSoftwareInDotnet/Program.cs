using DTK.LPR;
using DTK.Video;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace DTKSoftwareInDotnet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LPRParams parameters = new LPRParams();
            parameters.Countries = "AZ";
            parameters.MinPlateWidth = 80;
            parameters.MaxPlateWidth = 300;

            // Adding test zone for detecting vechiles in specific rectangle zones
            // (this is test values for creating rectangle zone in left middle side in overall view)
            var recognitionZoneIndex = parameters.AddZone();
            var recognitionZonePoint1 = parameters.AddZonePoint(recognitionZoneIndex, 0, 300);
            var recognitionZonePoint2 = parameters.AddZonePoint(recognitionZoneIndex, 0, 900);
            var recognitionZonePoint3 = parameters.AddZonePoint(recognitionZoneIndex, 900, 300);
            var recognitionZonePoint4 = parameters.AddZonePoint(recognitionZoneIndex, 900, 900);


            LPREngine engine = new LPREngine(parameters, true, OnLicensePlateDetected);
            VideoCapture videoCap = new VideoCapture(OnFrameCaptured, OnCaptureError, engine);

            Console.WriteLine("Welcome to Test Product...");

            var selectedOption = string.Empty;
            while (selectedOption != "1" && selectedOption != "2")
            {
                Console.WriteLine("Please choose your option:\n1 for (from RTCP stream)\n2 for (from video file)");
                selectedOption = Console.ReadLine().Trim();
            }

            var sourceInformation = selectedOption == "1" ? "RTCP stream URL" : "video file path";

            var sourcePath = string.Empty;
            while (string.IsNullOrEmpty(sourcePath))
            {
                Console.WriteLine($"Please enter {sourceInformation}");
                sourcePath = Console.ReadLine().Trim();
            }

            if (selectedOption == "1")
                videoCap.StartCaptureFromIPCamera(sourcePath);

            if (selectedOption == "2")
                videoCap.StartCaptureFromFile(sourcePath, 1);

            Console.ReadKey();
        }


        static void OnFrameCaptured(VideoCapture videoCap, VideoFrame frame, object customObject)
        {
            try
            {
                LPREngine engine = (LPREngine)customObject;
                engine.PutFrame(frame, 0);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void OnLicensePlateDetected(LPREngine engine, LicensePlate plate)
        {
            Console.WriteLine(string.Format("Plate text: {0} Country: {1} Confidence: {2}",
                   plate.Text, plate.CountryCode, plate.Confidence));

            using (var stream = new MemoryStream())
            {
                plate.Image.Save(stream, ImageFormat.Jpeg);

                var image = Image.FromStream(stream);

                //TEST
                string carImgFilename = $"images/{plate.Text}-car-{DateTime.Now.Ticks}";
                image.Save(carImgFilename);
            }

            //    string plateImgFilename = $"images/{plate.Text}-plate-{DateTime.Now.Ticks}";
            //plate.PlateImage.Save(plateImgFilename, plate.PlateImage.RawFormat);

            plate.Dispose();
        }

        static void OnCaptureError(VideoCapture videoCap, ERR_CAPTURE errorCode, object customObject)
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