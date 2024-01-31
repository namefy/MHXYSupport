using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models.Local;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR;

namespace MHXYSupport.Utility;

public class PaddleOCR
{
    public static PaddleOcrResult FindRegion(string imgPath)
    {
        FullOcrModel model = LocalFullModels.ChineseV3;

        using PaddleOcrAll all = new(model, PaddleDevice.Mkldnn())
        {
            AllowRotateDetection = true, /* 允许识别有角度的文字 */
            Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
        };
        // Load local file by following code:
        using Mat src = Cv2.ImRead(imgPath);
        return all.Run(src);
    }
}
