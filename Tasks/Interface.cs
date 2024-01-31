using System.Diagnostics;
namespace MHXYSupport.Tasks;

public interface IMain
{
    public Task Run(Form1 form, Process process);
}
