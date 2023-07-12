using AutoMapper;
using GatewayManagementCore.Entities;
using System.Linq.Expressions;

namespace GatewayManagementAPI.Utils
{
    public class ExpressionDeviceConverter : ITypeConverter<Expression<Func<PeripheralDevice, bool>>, Expression<Func<Models.PeripheralDevice, bool>>>
    {
        public Expression<Func<Models.PeripheralDevice, bool>> Convert(Expression<Func<PeripheralDevice, bool>> source, Expression<Func<Models.PeripheralDevice, bool>> destination, ResolutionContext context)
        {
            var parameter = Expression.Parameter(typeof(Models.PeripheralDevice), source.Parameters[0].Name);
            var visitor = new PeripheralDeviceToModelsPeripheralDeviceVisitor(parameter);
            var newBody = visitor.Visit(source.Body);
            return Expression.Lambda<Func<Models.PeripheralDevice, bool>>(newBody, parameter);
        }
    }


    public class PeripheralDeviceToModelsPeripheralDeviceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public PeripheralDeviceToModelsPeripheralDeviceVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(PeripheralDevice))
            {
                var memberInfo = typeof(Models.PeripheralDevice).GetMember(node.Member.Name).FirstOrDefault();
                if (memberInfo != null)
                {
                    return Expression.MakeMemberAccess(Visit(node.Expression), memberInfo);
                }
            }
            return base.VisitMember(node);
        }
    }
}
