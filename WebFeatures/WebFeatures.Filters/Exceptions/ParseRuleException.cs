using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebFeatures.QueryFilters.Exceptions
{
    internal class ParseRuleException : Exception
    {
        public override string Message { get; }

        public ParseRuleException(
            string visitorName,
            string summary = null,
            [CallerMemberName] string methodName = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Cannot apply rule '{methodName}' to '{visitorName}'.");

            if (summary != null)
            {
                sb.AppendLine($"Summary: {summary}");
            }

            Message = sb.ToString();
        }
    }
}
