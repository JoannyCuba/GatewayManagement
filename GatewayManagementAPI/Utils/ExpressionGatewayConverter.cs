using AutoMapper;
using GatewayManagementCore.Entities;
using System.Linq.Expressions;

namespace GatewayManagementAPI.Utils
{
    public class ExpressionGatewayConverter : ITypeConverter<Expression<Func<Gateway, bool>>, Expression<Func<Models.Gateway, bool>>>
    {
        public Expression<Func<Models.Gateway, bool>> Convert(Expression<Func<Gateway, bool>> source, Expression<Func<Models.Gateway, bool>> destination, ResolutionContext context)
        {
            var parameter = Expression.Parameter(typeof(Models.Gateway), source.Parameters[0].Name);
            var visitor = new GatewayToModelsGatewayVisitor(parameter);
            var newBody = visitor.Visit(source.Body);
            return Expression.Lambda<Func<Models.Gateway, bool>>(newBody, parameter);
        }
    }


    public class GatewayToModelsGatewayVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public GatewayToModelsGatewayVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(Gateway))
            {
                var memberInfo = typeof(Models.Gateway).GetMember(node.Member.Name).FirstOrDefault();
                if (memberInfo != null)
                {
                    return Expression.MakeMemberAccess(Visit(node.Expression), memberInfo);
                }
            }
            return base.VisitMember(node);
        }
    }
}
