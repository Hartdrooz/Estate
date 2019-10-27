using Agent.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agent.Domain.AggregateModel
{
    public class Agent : IAggregateRoot
    {
        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public short YearsOfExperience { get; }

        public int PropertySold { get; private set; }

        public string CompanyRef { get; private set; }

        public string PictureUrl { get; private set; }

        public Agent(string firstname,string lastname,short yearsOfExperience,string companyRef)
        {
            FirstName = firstname;
            LastName = lastname;
            YearsOfExperience = yearsOfExperience;
            CompanyRef = companyRef ?? "Freelancer";
        }

        public void WorkForCompany(string companyRef) 
        {
            CompanyRef = companyRef;
        }

        public void AddPropertySold() 
        {
            PropertySold += 1;
        }

        public void SetProfilePicture(string url) 
        {
            PictureUrl = url;
        }
    }
}
