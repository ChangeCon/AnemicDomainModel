using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace Ticketing.Infrastructure.Domain
{
	[DataContract(IsReference = true)]
	public abstract class EntityBase<TId>
	{
		private List<BusinessRule> _brokenRules = new List<BusinessRule>();

		protected List<BusinessRule> BrokenRules
		{
			get
			{
				if (_brokenRules == null)
					_brokenRules = new List<BusinessRule>();
				return _brokenRules;
			}
		}

		[DataMember]
		public TId Id { get; set; }

		public abstract void Validate();

		public string GetBrokenRulesString()
		{
			StringBuilder issues = new StringBuilder();
			BrokenRules.Clear();
			Validate();
			if (BrokenRules.Count() > 0)
			{
				foreach (BusinessRule businessRule in BrokenRules)
					issues.AppendLine(businessRule.Rule);
			}

			return issues.ToString();
		}

		public List<string> GetSeparateBrokenRulesList()
		{
			List<string> separateIssues = new List<string>();
			BrokenRules.Clear();
			Validate();
			if (BrokenRules.Count() > 0)
			{
				foreach (BusinessRule businessRule in BrokenRules)
				{
					if (businessRule.IsSeparate)
					{
						separateIssues.Add(businessRule.Rule);
					}
				}
					
			}
			return separateIssues;
		}

		public IEnumerable<BusinessRule> GetBrokenRules()
		{
			BrokenRules.Clear();
			Validate();
			return BrokenRules;
		}

		protected void AddBrokenRule(BusinessRule businessRule)
		{
			BrokenRules.Add(businessRule);
		}

		public void ThrowExceptionIfInvalid()
		{
			string issues = GetBrokenRulesString();
			List<string> separateIssues = GetSeparateBrokenRulesList();

			if (!string.IsNullOrWhiteSpace(issues))
				throw new EntityIsInvalidException(issues.ToString(), separateIssues);
		}

		public override bool Equals(object entity)
		{
			return entity != null
					&& entity is EntityBase<TId>
					&& this == (EntityBase<TId>) entity;
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		public static bool operator ==(EntityBase<TId> entity1,
										EntityBase<TId> entity2)
		{
			if ((object) entity1 == null && (object) entity2 == null)
			{
				return true;
			}

			if ((object) entity1 == null || (object) entity2 == null)
			{
				return false;
			}

			if (entity1.Id.ToString() == entity2.Id.ToString())
			{
				return true;
			}

			return false;
		}

		public static bool operator !=(EntityBase<TId> entity1,
										EntityBase<TId> entity2)
		{
			return ( !( entity1 == entity2 ) );
		}
	}

}