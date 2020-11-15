using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Bases;
namespace Domain.AggregatesModel.PersonAggregate
{
    public class Email : ValueObject
    {

        public string EmailAddress { get; private set; }
        public Email() { }

        private Email(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
        static public Email Create(string emailAddress)

        {

            if (Validate(emailAddress))

            {

                return new Email(emailAddress);

            }

            throw new ArgumentException("emailAddress");

        }


        static public bool Validate(string emailAddress)
        {

            string patternStrict = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

            Regex regexStrict = new Regex(patternStrict);

            return regexStrict.IsMatch(emailAddress);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EmailAddress;
        }
    }
}