using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BiddingService
{
    public static class ObjConverter
    {

        public static Object ObjectCoverter(Object MobileObject, Object WebObject)
        {

            if (MobileObject != null)
            {

                foreach (PropertyInfo propertyMobile in MobileObject.GetType().GetProperties())
                {

                    if (!propertyMobile.PropertyType.IsGenericType)
                    {

                        foreach (PropertyInfo propertyWeb in WebObject.GetType().GetProperties())
                        {

                            if (propertyWeb.Name.Equals(propertyMobile.Name))
                            {

                                propertyWeb.SetValue(WebObject, propertyMobile.GetValue(MobileObject,

                                null), null);
                                break;
                            }

                        }

                    }

                }

            }

            return WebObject;
        }
    }
}