using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtTool.Service
{
    public class ConvertService
    {
        public static List<T> SetListFromForm<T>(NameValueCollection collection) where T : new()
        {
            List<T> t = new List<T>();
            try
            {
                var properties = new T().GetType().GetProperties(); 
                var allkeys = collection.AllKeys.Where(p => properties.Select(k => k.Name.ToLower()).Contains(p.ToLower())).ToList();
                var subNum = collection.GetValues(allkeys[0]).Length;
                for (int i = 0; i < subNum; i++)
                {
                    var model = new T();
                    foreach (var key in allkeys)
                    {
                        if (collection.GetValues(key).Count() == i)
                            continue;
                        string pval = collection.GetValues(key)[i];
                        var pro = properties.FirstOrDefault(p => p.Name.ToLower() == key.ToLower());
                        if (!string.IsNullOrEmpty(pval) && pro != null)
                        {
                            var type = pro.PropertyType;
                            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
                            if (type.Name == typeof(Nullable<>).Name)
                            {
                                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                NullableConverter nullableConverter = new NullableConverter(pro.PropertyType);
                                //将convertsionType转换为nullable对的基础基元类型
                                type = nullableConverter.UnderlyingType;
                            }
                            pro.SetValue(model,
                                type == typeof(Guid) ? Guid.Parse(pval) : Convert.ChangeType(pval, type), null);
                        }
                    }
                    t.Add(model);
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }


            return t;
        }

        public static T SetContractFromForm<T>(NameValueCollection collection) where T : new()
        {
            var properties = new T().GetType().GetProperties(); 
            var allkeys = collection.AllKeys.Where(p => properties.Select(k => k.Name.ToLower()).Contains(p.ToLower())).ToList();
            var model = new T();
            foreach (var key in allkeys)
            {
                string pval = collection[key];
                var pro = properties.FirstOrDefault(p => p.Name.ToLower() == key.ToLower());
                if (!string.IsNullOrEmpty(pval) && pro != null)
                {
                    var type = pro.PropertyType;
                    //判断convertsionType类型是否为泛型，因为nullable是泛型类,
                    if (type.Name == typeof(Nullable<>).Name)
                    {
                        //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                        NullableConverter nullableConverter = new NullableConverter(pro.PropertyType);
                        //将convertsionType转换为nullable对的基础基元类型
                        type = nullableConverter.UnderlyingType;
                    }
                    pro.SetValue(model,
                        type == typeof(Guid) ? Guid.Parse(pval) : Convert.ChangeType(pval, type), null);
                }
            }
            return model;
        }

        /// <summary>
        /// str的长度小于strlength时，需在str前后填补空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string ConvertStr(string str, int strLength)
        {
            if (str == null)
                return "".PadRight(strLength);
            int zimu = 0, shuzi = 0, hanzi = 0, fuhao = 0,space=0;
            foreach (char item in str)
            {
                if (item >= 'a' && item <= 'z' || item >= 'A' && item <= 'Z')
                    zimu++;
                else if (item >= '0' && item <= '9')
                    shuzi++;
                else if (item == ' '||item=='.' || item == '+')
                    space++;
                else if (item >= 0x4e00 && item <= 0x9fbb)
                    hanzi++;
                else
                    fuhao++;
            }
            var len = (hanzi+fuhao)*2+ zimu + shuzi+space;
            if (len >= strLength)
            return str;
            var spacelen = strLength - len;
            var trim = spacelen / 2;
           return str.PadLeft(trim + str.Length).PadRight(strLength- hanzi - fuhao);

        }
    }
}
