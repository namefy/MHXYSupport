using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYSupport.Utility;

public class Action
{
    public static bool ClickTargetButton(Process process, string imgPath, string keyword)
    {
        return ClickTargetButton(process, imgPath, keyword, new OpenCvSharp.Point(0, 0));
    }

    public static bool ClickTargetButton(Process process, string imgPath, string keyword, OpenCvSharp.Point offset)
    {
        (bool success, Sdcb.PaddleOCR.PaddleOcrResult? ocrResult) = FindKeyword(process, imgPath, keyword);
        if (success)
        {
            return ClickTargetButton(process, ocrResult, keyword, offset);
        }
        return success;
    }

    public static bool ClickTargetButton(Process process, Sdcb.PaddleOCR.PaddleOcrResult ocrResult, string keyword)
    {
        return ClickTargetButton(process, ocrResult, keyword, new OpenCvSharp.Point(0, 0));
    }

    public static bool ClickTargetButton(Process process, Sdcb.PaddleOCR.PaddleOcrResult ocrResult, params string[] keywords)
    {
        foreach (string keyword in keywords)
        {
            bool success = ClickTargetButton(process, ocrResult, keyword);
            if (success) return true;
        }
        return false;
    }

    public static bool ClickTargetButton(Process process, Sdcb.PaddleOCR.PaddleOcrResult ocrResult, string keyword, OpenCvSharp.Point offset)
    {
        if (ocrResult.Text.Contains(keyword))
        {
            WindowsApi.RECT procRect = WindowsApi.GetPrecessRect(process);
            var region = ocrResult.Regions.Where(p => p.Text.Contains(keyword)).OrderBy(p => p.Text.Length).FirstOrDefault();
            WindowsApi.MouseLeftClick(new OpenCvSharp.Point(procRect.X + region.Rect.Center.X + offset.X, procRect.Y + region.Rect.Center.Y + offset.Y));
            Debug.WriteLine($"{keyword}:{new OpenCvSharp.Point(procRect.X + region.Rect.Center.X + offset.X, procRect.Y + region.Rect.Center.Y + offset.Y)}");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 界面中关键字信息
    /// </summary>
    /// <param name="progressName"></param>
    /// <param name="imgPath"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static (bool, Sdcb.PaddleOCR.PaddleOcrResult?) FindKeyword(Process process, string imgPath, string keyword)
    {
        int i = 0;
        while (true)
        {
            WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
            var ocrResult = PaddleOCR.FindRegion(imgPath);
            if (ocrResult is not null && ocrResult.Text.Contains(keyword))
            {
                return (true, ocrResult);
            }
            i++;
            if (i == Const.RetryCount)
            {
                Console.WriteLine($"{keyword} not fund");
                return (false, null);
            }
            Thread.Sleep(Const.RetryInterval);
        }
    }

    /// <summary>
    /// 界面中不含关键字
    /// </summary>
    /// <param name="progressName"></param>
    /// <param name="imgPath"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static bool FindNoKeyword(Process process, string imgPath, string keyword)
    {
        int i = 0;
        while (true)
        {
            WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
            var ocrResult = PaddleOCR.FindRegion(imgPath);
            if (ocrResult is not null && !ocrResult.Text.Contains(keyword))
            {
                return true;
            }
            i++;
            if (i == Const.RetryCount)
            {
                Console.WriteLine($"{keyword} exist");
                return false;
            }
            Thread.Sleep(Const.RetryInterval);
        }
    }

}
