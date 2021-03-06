﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Common
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        private string LoginUserKey = "secom_loginkey_2018";
        private string LoginProvider = Configs.GetValue("LoginProvider");

        public OperatorModel GetCurrent()
        {
            OperatorModel operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                if (!string.IsNullOrEmpty(WebHelper.GetCookie(LoginUserKey)))
                {
                    operatorModel = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey)).ToObject<OperatorModel>();
                }
                else
                {
                    operatorModel = null;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(WebHelper.GetSession(LoginUserKey)))
                {
                    operatorModel = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey)).ToObject<OperatorModel>();
                }
                else
                {
                    operatorModel = null;
                }
            }
            return operatorModel;
        }
        public void AddCurrent(OperatorModel operatorModel)
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(operatorModel.ToJson()), 60);
            }
            else
            {
                WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(operatorModel.ToJson()));
            }
            WebHelper.WriteCookie("secom_mac", Md5.md5(Net.GetMacByNetworkInterface().ToJson(), 32));
            WebHelper.WriteCookie("secom_licence", Licence.GetLicence());
        }
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
    }
}
