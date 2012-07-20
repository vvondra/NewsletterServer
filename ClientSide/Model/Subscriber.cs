using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ClientSide.Properties;

namespace ClientSide.Model
{
    /// <summary>
    /// Represents a newsletter subscriber
    /// </summary>
    public class Subscriber : IDataErrorInfo
    {
        #region Creation

        public static Subscriber CreateNewSubscriber()
        {
            return new Subscriber();
        }

        public static Subscriber CreateSubscriber(
            string name,
            bool isSubscribed,
            string email)
        {
            return new Subscriber {
                Name = name,
                IsSubscribed = isSubscribed,
                Email = email
            };
        }

        protected Subscriber()
        {
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the e-mail address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets/sets the subscribers name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets whether the subscriber is active
        /// </summary>
        public bool IsSubscribed { get; set; }

        /// <summary>
        /// Gets/sets the subscribers newsletter ID
        /// </summary>
        public int NewsletterID { get; set; }



        #endregion // State Properties

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion // IDataErrorInfo Members

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "Email", 
            "Name", 
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName) {
                case "Email":
                    error = this.ValidateEmail();
                    break;

                case "Name":
                    error = this.ValidateName();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                    break;
            }

            return error;
        }

        string ValidateEmail()
        {
            if (IsStringMissing(this.Email)) {
                return Resources.Subscriber_MissingEmail;
            } else if (!IsValidEmailAddress(this.Email)) {
                return Resources.Subscriber_InvalidEmail;
            }
            return null;
        }

        string ValidateName()
        {
            if (IsStringMissing(this.Name)) {
                return Resources.Subsciber_MissingName;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        #endregion // Validation
    }
}