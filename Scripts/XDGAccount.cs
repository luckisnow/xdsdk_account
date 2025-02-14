using System;
using System.Collections.Generic;
using System.Linq;
using XD.SDK.Account.Internal;
using XD.SDK.Common.Internal;

namespace XD.SDK.Account{
    public class XDGAccount
    {
        private static IXDGAccount platformWrapper;
        
        static XDGAccount() 
        {
            var interfaceType = typeof(IXDGAccount);
            var platformInterfaceType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(clazz => interfaceType.IsAssignableFrom(clazz));
            if (platformInterfaceType != null) {
                platformWrapper = Activator.CreateInstance(platformInterfaceType) as IXDGAccount;
            }
        }
        
        public static void Login(List<LoginType> loginTypes, Action<IXDGUser> callback, Action<IXDGError> errorCallback)
        {
            platformWrapper.Login(loginTypes, callback, errorCallback);
        }

        public static void LoginByType(LoginType loginType, Action<IXDGUser> callback, Action<IXDGError> errorCallback)
        {
            platformWrapper.LoginByType(loginType, callback, errorCallback);
        }

        public static void LoginByConsole(Action<IXDGUser> successCallback, Action failCallback, Action<IXDGError> errorCallback)
        {
            platformWrapper.LoginByConsole(successCallback, failCallback, errorCallback);
        }

        public static void Logout()
        {
            platformWrapper.Logout();
        }
        
        public static void AddUserStatusChangeCallback(Action<XDGUserStatusCodeType, string> callback){
            platformWrapper.AddUserStatusChangeCallback(callback);
        }
        
        public static void GetUser(Action<IXDGUser> callback, Action<IXDGError> errorCallback)
        {
            platformWrapper.GetUser(callback, errorCallback);
        }
        
        public static void OpenUserCenter(){
            platformWrapper.OpenUserCenter();
        }

        public static void OpenUnregister(){
            platformWrapper.OpenUnregister();
        }
        
        //641 FB token
        public static void IsTokenActiveWithType(LoginType loginType, Action<bool> callback){
            platformWrapper.IsTokenActiveWithType(loginType, callback);
        }
        
        //除了 Default 和 Guest
        public static void BindByType(LoginType loginType, Action<bool,IXDGError> callback){
            platformWrapper.BindByType(loginType, callback);
        }

    }
}