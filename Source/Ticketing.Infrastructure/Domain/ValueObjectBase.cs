using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ticketing.Infrastructure.Domain
{
	[DataContract(IsReference = true)]
	public abstract class ValueObjectBase
	{
		private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected List<BusinessRule> BrokenRules
        {
            get { return _brokenRules ?? (_brokenRules = new List<BusinessRule>()); }
        }

		public ValueObjectBase()
		{
		}

		public IEnumerable<BusinessRule> GetBrokenRules()
		{
            BrokenRules.Clear();
			Validate();
            return BrokenRules;
		}

		public abstract void Validate();

		public void ThrowExceptionIfInvalid()
		{
            BrokenRules.Clear();
			Validate();
            if (BrokenRules.Any())
			{
				StringBuilder issues = new StringBuilder();
				List<string> separateIssues = new List<string>();
                foreach (var businessRule in BrokenRules)
				{
					issues.AppendLine(businessRule.Rule);
					if (businessRule.IsSeparate)
					{
						separateIssues.Add(businessRule.Rule);
					}
				}

				throw new ValueObjectIsInvalidException(issues.ToString(), separateIssues);
			}
		}

		protected void AddBrokenRule(BusinessRule businessRule)
		{
            BrokenRules.Add(businessRule);
		}
	}
}