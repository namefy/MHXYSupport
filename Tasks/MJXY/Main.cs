using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYSupport.Tasks.MJXY;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("秘境降妖 开始");
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.MJXY, Tasks.Const.MJXYOffset);
        if (!success) return;
        await Task.Run(() =>
        {
            form.SetTextBoxMessage("秘境降妖 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                Utility.Action.ClickTargetButton(process, ocrResult
                     ,Const.TZ,Const.JRZD);
                var result = ocrResult.Regions.Where(p => p.Text.Contains(Tasks.Const.MJXY)
                                                        && p.Rect.Center.X >= rect.Width / 2.0)
                                            .OrderBy(p => p.Text.Length).FirstOrDefault();
                if (result != default)
                {
                    WindowsApi.MouseLeftClick(new OpenCvSharp.Point(rect.X + result.Rect.Center.X, rect.Y + result.Rect.Center.Y));
                }
                Thread.Sleep(Tasks.Const.RetryTime);
            }
        });
        form.AppendTextBoxMessage("秘境降妖 完成");
    }
}
