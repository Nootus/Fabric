using Nootus.Fabric.Mobile.Dialog;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Core
{
    public class NTCommand: Command
    {
        public NTCommand(Func<Task> execute): base(async () => 
                                            {
                                                try
                                                {
                                                    await execute();
                                                }
                                                catch(System.Exception exp)
                                                {
                                                    DependencyService.Get<IDialogService>().DisplayAlert(AlertMode.Error, exp.Message);
                                                }
                                            })
        {

        }
    }
}
