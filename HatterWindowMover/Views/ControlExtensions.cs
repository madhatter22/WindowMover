using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WindowMover.Views
{
    public static class ControlExtensions
    {
        public static void AddBinding<TControl, TViewModel, TViewModelProperty>(this TControl control, Expression<Func<TControl, TViewModelProperty>> controlPropertyExpression,
            TViewModel viewModel, Expression<Func<TViewModel, TViewModelProperty>> viewModelPropertyExpression)
            where TControl : Control
        {
            control.DataBindings.Add(GetPropertyName(controlPropertyExpression), viewModel,
                GetPropertyName(viewModelPropertyExpression));
        }

        public static void AddBinding<TControl, TViewModel, TViewModelProperty>(this TControl control, Expression<Func<TControl, TViewModelProperty>> controlPropertyExpression,
            TViewModel viewModel, Expression<Func<TViewModel, TViewModelProperty>> viewModelPropertyExpression, DataSourceUpdateMode dataSourceUpdateMode)
            where TControl : Control
        {
            control.DataBindings.Add(GetPropertyName(controlPropertyExpression), viewModel,
                GetPropertyName(viewModelPropertyExpression), false, dataSourceUpdateMode);
        }

        /// <summary>
        /// Gets the property name from the member expression.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TProperty">Property type</typeparam>
        /// <param name="property">The expression</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="property"/> is not a valid member expression.</exception>
        private static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> property)
        {
            var m = property.Body as MemberExpression;
            if (m != null && m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                return m.Member.Name;
            }

            throw new ArgumentException("Could not get MemberExpression", "property");
        }
    }
}
