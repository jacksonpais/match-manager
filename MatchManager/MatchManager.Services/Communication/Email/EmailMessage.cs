﻿using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Enums;
using MatchManager.Services.Communication.Email.Interface;
using MatchManager.Services.Communication.Interface;
using MatchManager.Services.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MatchManager.Services.Communication.Email
{
    public class EmailMessage : IMessageService
    {
        public string CreateRegistrationVerificationMessage(string name, string linkToVerify, string mainEmail)
        {
            var stringtemplate = new StringBuilder();
            stringtemplate.Append("<table style='width: 500px; background-color: #fff; border-radius: 5px; border: 2px #33BDD1 solid; margin: 0 auto; font-family: Arial,Tahoma; font-size: 14px; color: #045489; line-height: 20px;'>");
            stringtemplate.Append("<tr><td style='padding-top:10px; vertical-align:middle;text-align:center;padding-bottom:5px;border-bottom:2px solid #33BDD1;background-color:#22788c'><p style='margin:0px auto; font-size: 18px; padding-bottom: 0px; color: #045489; text-align: center; '><img width='30' height='30' src='' style='margin-right: 5px;'><img img width='155' height='25' src=''></p></td></tr>");
            stringtemplate.Append("<tr><td style=' padding: 20px 25px 20px; vertical-align: top; text-align: left;'><p><strong>");
            stringtemplate.Append($"Dear { name }");
            stringtemplate.Append("</strong>, </p><p>Welcome to DestinEye – your destination for eye care.</p><p>We're excited to have you onboard with us. Wishing you a life full of clarity – now and forever.</p><p>Click the Link Below to Activate your Account</p><a target='_blank' ");
            stringtemplate.Append($"href='{linkToVerify}'");
            stringtemplate.Append($" style ='color:#045489;'><strong>CLICK HERE</strong> </a><p><strong>Regards, <br />DestinEye Team</strong></p></td></tr><tr><td style='padding: 0px 25px 20px 25px; vertical-align:top;text-align:left;'>Please Do not reply to this mail, it is an autogenerated email and will not be responded.<br />If you want to contact us, please write to us at <a href='mailto:{mainEmail}'>{mainEmail}</a></td></tr></table>");
            return stringtemplate.ToString();
        }
    }
}
