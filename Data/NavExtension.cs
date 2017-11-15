using System;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Data
{
    public static class NavExtension
    {
        public static DataServiceQuery<T> ExpandEntity<T>(this DataServiceQuery<T> source, string expandExpression)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");

            if (string.IsNullOrEmpty(expandExpression))
                throw new ArgumentException("sortExpression is null or empty.", "sortExpression");

            var parts = expandExpression.Split(' ');
            var propertyName = "";
            var tType = typeof(T);

            if (parts.Length > 0 && parts[0] != "")
            {
                propertyName = parts[0];


                PropertyInfo prop = tType.GetProperty(propertyName);

                if (prop == null)
                {
                    throw new ArgumentException(string.Format("No property '{0}' on type '{1}'", propertyName, tType.Name));
                }

                var funcType = typeof(Func<,>)
                    .MakeGenericType(tType, prop.PropertyType);

                var lambdaBuilder = typeof(Expression)
                    .GetMethods()
                    .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                    .MakeGenericMethod(funcType);

                var parameter = Expression.Parameter(tType);
                var propExpress = Expression.Property(parameter, prop);
                
                var expandLambda = lambdaBuilder
                    .Invoke(null, new object[] { propExpress, new ParameterExpression[] { parameter } });

                var expander = tType
                    .GetMethods()
                    .FirstOrDefault(x => x.Name == "Expand" && x.GetParameters().Length == 2)
                    .MakeGenericMethod(new[] { tType, prop.PropertyType });

                return (DataServiceQuery<T>)expander
                    .Invoke(null, new object[] { source, expandLambda });
            }

            return source;
        }

        public static Expression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return Expression.Lambda(body, param);
        }

    }
}
