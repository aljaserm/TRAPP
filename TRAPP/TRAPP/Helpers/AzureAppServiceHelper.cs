#define OFFLINE_ASYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace TRAPP.Helpers
{
    public class AzureAppServiceHelper
    {
        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await App.MobileService.SyncContext.PushAsync();
                await App.PostTableGlobal.PullAsync("PostedUser","");
            }
            catch (MobileServicePushFailedException mspfex)
            {
                if(mspfex.PushResult!= null)
                {
                    syncErrors = mspfex.PushResult.Errors;
                }
            }
            catch(Exception ex)
            {

            }
            if (syncErrors != null)
            {
                foreach(var e in syncErrors)
                {
                    if (e.OperationKind == MobileServiceTableOperationKind.Update && e.Result!=null)
                    {
                      await e.CancelAndUpdateItemAsync(e.Result);
                    }
                    else
                    {
                        await e.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
        
    }
}
