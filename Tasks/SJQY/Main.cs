using MHXYSupport.Extensions;
using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks.SJQY;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("加载题库 开始");
        string topicStr = await File.ReadAllTextAsync(Const.TopFilePath);
        var topicDic = topicStr.ToEntity<Dictionary<string, string[]>>();
        form.AppendTextBoxMessage("加载题库 结束");

        form.AppendTextBoxMessage("三界奇缘 开始");
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.SJQY, Tasks.Const.SJQYOffset);
        if (!success) return;
        WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
        await Task.Run(() =>
        {
            Regex regex = new(@"(?<=第)\d+(?=题)");
            Regex regex2 = new(@"(?<=：)\S+?(?=[？|（|]{1})");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                if (ocrResult.Text.Contains(Const.EndKeyword)) return;
                if (!regex.IsMatch(ocrResult.Text)) continue;
                string fullTopic = ocrResult.Regions.Where(p => regex.IsMatch(p.Text)).OrderBy(p => p.Text.Length).First().Text;
                string topic = regex2.Match(fullTopic).Value;
                if (!topicDic.TryGetValue(topic, out var answers))
                {
                    form.AppendTextBoxMessage($"{topic} 题库不存在 {Environment.NewLine} {Const.WaitSeconds}秒后重新加载题库");
                    Thread.Sleep(1000 * Const.WaitSeconds);
                    topicStr = File.ReadAllText(Const.TopFilePath);
                    topicDic = topicStr.ToEntity<Dictionary<string, string[]>>();
                    continue;
                }
                List<string> answered = new();
                while (true)
                {
                    foreach (string answer in answers)
                    {
                        if (answered.Contains(answer)) continue;
                        var result = ocrResult.Regions.Where(p => p.Text == answer).FirstOrDefault();
                        if (result == default) continue;
                        WindowsApi.MouseLeftClick(new OpenCvSharp.Point(rect.X + result.Rect.Center.X, rect.Y + result.Rect.Center.Y));
                        answered.Add(answer);
                        if (answered.Count == answers.Length) break;
                        Thread.Sleep(1000);
                        WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                        ocrResult = PaddleOCR.FindRegion(imgPath);
                    }
                    Thread.Sleep(1000);
                    if (answered.Count == answers.Length) break;
                }
            }
        });
        form.AppendTextBoxMessage("三界奇缘 结束");
    }
}
