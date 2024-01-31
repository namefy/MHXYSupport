using MHXYWF.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYWF.Tasks.BT;

/// <summary>
/// 挖宝图
/// </summary>
public class WMain : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        string imgPath = MHXYWF.Const.ScreenshotDirectory + $"{process.Id}_{Const.DScreenshotName}";

        form.SetTextBoxMessage("挖宝图 开始");
        await Task.Run(() =>
        {
            int i = 0;
            form.AppendTextBoxMessage("挖宝图 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                var region = ocrResult.Regions.Where(p => p.Text.Contains(Const.SY)).OrderBy(p => p.Text.Length).FirstOrDefault();
                if (region == default)
                {
                    i++;
                    if (i > Const.MaxCount) break;
                    Thread.Sleep(Const.RetrySeconds * 1000);
                    continue;
                }
                WindowsApi.MouseLeftClick(new OpenCvSharp.Point(rect.X + region.Rect.Center.X, rect.Y + region.Rect.Center.Y));
                i = 0;
            }
        });
        form.AppendTextBoxMessage("挖宝图 完成");
    }
}
