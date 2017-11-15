﻿using System;
using System.Reflection;

using Omu.ValueInjecter.Injections;

namespace WebUI.Mappers.Injections
{
    public class NormalToNullables : LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            //if (source == typeof(DateTime?))
                return source != Nullable.GetUnderlyingType(target);
            //return source == Nullable.GetUnderlyingType(target);
        }

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var val = sp.GetValue(source);

            if (val == null)
            {
                return;
            }
            //ignore int = 0 and DateTime = 1/01/0001
            else if (sp.PropertyType == typeof(int) && (int)val == default(int) ||
                sp.PropertyType == typeof(DateTime) || sp.PropertyType == typeof(DateTime?) && (DateTime)val == default(DateTime))
            {
                val = null;
            }

            tp.SetValue(target, val);
        }
    }
}