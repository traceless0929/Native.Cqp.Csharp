using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Native.Csharp.Tool.Reflection
{
    public class MethodUtil
    {
        public static T runMethod<T>(string loadNameSpace,string loadType,string methodSignature,params object[] parameters)
        {
            if (string.IsNullOrEmpty(methodSignature))
            {
                return default(T);
            }
            // 1.Load(命名空间名称)，GetType(命名空间.类名)
            Type type = Assembly.Load(loadNameSpace).GetType(loadType);
            //2.GetMethod(需要调用的方法名称)
            MethodInfo method = type.GetMethod(methodSignature);
            // 3.调用的实例化方法（非静态方法）需要创建类型的一个实例
            object obj = Activator.CreateInstance(type);
            // 4.调用方法，如果调用的是一个静态方法，就不需要第3步（创建类型的实例）
            // 相应地调用静态方法时，Invoke的第一个参数为null
            object result = method.Invoke(obj, parameters);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result));
        }

        public static T runStaticMethod<T>(string loadNameSpace, string loadType, string methodSignature, params object[] parameters)
        {
            if (string.IsNullOrEmpty(methodSignature))
            {
                return default(T);
            }
            // 1.Load(命名空间名称)，GetType(命名空间.类名)
            Type type = Assembly.Load(loadNameSpace).GetType(loadType);
            //2.GetMethod(需要调用的方法名称)
            MethodInfo method = type.GetMethod(methodSignature);
            // 相应地调用静态方法时，Invoke的第一个参数为null
            object result = method.Invoke(null, parameters);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result));
        }
    }
}
