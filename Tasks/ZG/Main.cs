using MHXYWF.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYWF.Tasks.ZG;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("捉鬼 开始");
        string imgPath = MHXYWF.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.ZGRW, Tasks.Const.ZGRWOffset);
        if (!success) return;
        await Task.Run(() =>
        {
            form.SetTextBoxMessage("捉鬼 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                Utility.Action.ClickTargetButton(process, ocrResult
                     , Tasks.Const.QD, Const.RC_ZG);
                var result = ocrResult.Regions.Where(p => p.Text.Contains(Const.ZGRW)
                                                        && p.Rect.Center.X >= rect.Width / 2.0)
                                            .OrderBy(p => p.Text.Length).FirstOrDefault();
                if (result != default)
                {
                    WindowsApi.MouseLeftClick(new OpenCvSharp.Point(rect.X + result.Rect.Center.X, rect.Y + result.Rect.Center.Y));
                }
                result = ocrResult.Regions.Where(p => p.Text.Contains(Const.RC_ZG)).OrderBy(p => p.Text.Length).FirstOrDefault();
                if (result != default)
                {
                    form.AppendTextBoxMessage($"当前进度：{Tasks.Const.ProgressRegex.Match(result.Text).Value}");
                }
                Thread.Sleep(Const.WaitSeconds * 1000);
            }
        });
        form.AppendTextBoxMessage("捉鬼 完成");
    }
}
