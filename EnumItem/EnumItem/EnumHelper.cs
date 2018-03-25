using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EnumItem
{
    public class EnumHelper
    {
        enum State {
            Id = 0,
            Name = 1,
            IdAndName = 2,
        }
        public string GetKbn<T>(string key, int state = 1)
        {
            var type = typeof(T);
            foreach (var v in type.GetFields())
            {
                var val = v.GetValue(key);
                EnumAttribute[] items = (EnumAttribute[])v.GetCustomAttributes(typeof(EnumAttribute), false);
                foreach (var item in items)
                {
                    if (key.Equals(val))
                    {
                        if (state == (int)State.Id) return item.Id.ToString();
                        if (state == (int)State.IdAndName) return item.IdAndName;

                        return item.Name;
                    }
                }
            }
            return string.Empty;
        }
        public string GetKbn<T>(int key, int state = 1)
        {
            var type = typeof(T);
            foreach (var v in type.GetFields())
            {
                var val = (int)v.GetValue(key);
                EnumAttribute[] items = (EnumAttribute[])v.GetCustomAttributes(typeof(EnumAttribute), false);
                foreach (var item in items)
                {
                    if (key.Equals(val))
                    {
                        if (state == (int)State.Id) return item.Id.ToString();
                        if (state == (int)State.IdAndName) return item.IdAndName;

                        return item.Name;
                    }
                }
            }
            return string.Empty;
        }

        public IEnumerable<EnumItem> GetAll<T>()
        {
            var type = typeof(T);
            foreach (var v in type.GetFields())
            {
                EnumAttribute[] items = (EnumAttribute[])v.GetCustomAttributes(typeof(EnumAttribute), false);
                foreach (var item in items)
                {
                    var display = new EnumItem();
                    display.Id = item.Id;
                    display.Name = item.Name;
                    display.IdAndName = item.IdAndName;
                    yield return display;
                }
            }
        }
    }
}
