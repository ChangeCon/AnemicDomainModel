using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticketing.Infrastructure.Domain
{
    public class BusinessRule
    {
        private string _property;
        private string _rule;
		private bool _isSeparate;

        public BusinessRule()
        { }

        public BusinessRule(string property, string rule, bool isSeparate = false)
        {
            this._property = property;
            this._rule = rule;
			this._isSeparate = isSeparate;
        }

        public string Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public string Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

		public bool IsSeparate
		{
			get { return _isSeparate; }
			set { _isSeparate = value; }
		}
    }
}
