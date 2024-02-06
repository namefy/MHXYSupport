using MHXYSupport.Utility;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks.JYL;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        WindowsApi.RECT procRect = WindowsApi.GetPrecessRect(process);
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = false;
        #region 领取
        //bool success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Tasks.Const.JYRWL, Tasks.Const.JYRWLOffset);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Const.JYRWL);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Const.LQJYRWL);
        //if (!success) return;
        #endregion

        #region 跑环
        form.SetTextBoxMessage("经验链 开始");
        Regex regex = new(@"\d+");
        await Task.Run(() =>
        {
            while (true)
            {
                procRect = WindowsApi.GetPrecessRect(process);
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                success = Utility.Action.ClickTargetButton(process, ocrResult
                    , Const.KSZD
                    , Const.JYRWL_XC, Const.JYRWL_XW, Const.JYRWL_XR, Const.JYRWL_ZD
                    , Const.GM, Const.SJ);
                if (success)
                {
                    Thread.Sleep(Const.WaitTimeShort);
                    continue;
                }
                var region = ocrResult.Regions.Where(p => p.Text.Contains(Const.JYL)
                                                        && p.Rect.Center.X > procRect.X / 2.0)
                                            .OrderBy(p => p.Text.Length)
                                            .FirstOrDefault();
                if (region != default)
                {
                    WindowsApi.MouseLeftClick(new OpenCvSharp.Point(procRect.X + region.Rect.Center.X, procRect.Y + region.Rect.Center.Y));
                    form.AppendTextBoxMessage(region.Text);
                    Thread.Sleep(Const.WaitTimeMedium);
                    continue;
                }
                Thread.Sleep(Const.WaitTimeLong);
            }
        });

        #endregion
    }
}
