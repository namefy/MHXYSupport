using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks;

public class Const
{
    public const string HD = "活动";
    public static OpenCvSharp.Point HDOffset = new(0, -10);
    public const string SMRW = "师门任务";
    public static OpenCvSharp.Point SMRWOffset = new(170, 25);
    public const string BTRW = "宝图任务";
    public static OpenCvSharp.Point BTRWOffset = new(170, 25);
    public const string YB = "运镖";
    public static OpenCvSharp.Point YBOffset = new(190, 25);
    public const string JYRWL = "经验任务链";
    public static OpenCvSharp.Point JYRWLOffset = new(170, 25);
    public const string SJQY = "三界奇缘";
    public static OpenCvSharp.Point SJQYOffset = new(170, 25);
    public const string ZGRW = "捉鬼任务";
    public static OpenCvSharp.Point ZGRWOffset = new(170, 25);
    public const string MJXY = "秘境降妖";
    public static OpenCvSharp.Point MJXYOffset = new(170, 25);
    public const string BPRW = "帮派任务";
    public static OpenCvSharp.Point BPRWOffset = new(170, 25);
    public static Regex ProgressRegex = new(@"\d+\/\d+");
    public const string QD = "确定";
    public const string SJ = "上交";
    public const string GM = "购买";
    public const string SY = "使用";
    public const string ZD = "自动";
    public const string JX = "点击任意地方继续";
    public const int RetryTime = 5 * 1000;
    public const int RetryCount = 5;
    
}