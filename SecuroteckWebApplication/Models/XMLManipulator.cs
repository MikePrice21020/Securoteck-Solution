using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebApplication1.Models
{
    public sealed class XMLManipulator
    {
        private static readonly Lazy<XMLManipulator> manipulator = new Lazy<XMLManipulator>(() => new XMLManipulator());
        public static XMLManipulator Manipluator { get { return manipulator.Value; } }

        private XmlDocument doc = new XmlDocument();

        private SortedDictionary<string,User> activeUsers = new SortedDictionary<string, User>();
        
        private XMLManipulator()
        {
            doc.Load(HttpContext.Current.Server.MapPath("~/App_Data/ServerData.xml"));
        }

        //public User ValidateUserByAPIKey(string APIKey)
        //{
        //    try
        //    {
        //        return activeUsers[APIKey];
        //    }
        //    catch
        //    {
        //        XmlNode userNode = doc.SelectSingleNode(String.Format("Data/User[API_Key={0}]", APIKey));
        //        if (userNode != null)
        //        {
        //            User newUser = new User(userNode);
        //            activeUsers.Add(newUser.ApiKey,newUser);
        //            return newUser;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}