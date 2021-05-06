using AutoMapper.Configuration.Annotations;
using System;
using System.Text.Json.Serialization;

namespace Libraries {
    public class Person : IIndexItem {
        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender {
            get { return PersonGender ? "male" : "female"; }
            set { PersonGender = value.ToLower() == "male"; }
        }

        public bool PersonGender { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

        [JsonPropertyName("registration_date")]
        public string RegistrationDate { get; set; }

        [Ignore]
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [Ignore]
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        private string location = null;
        public string Location {
            get {
                if (location is null)
                    return $"{Latitude},{Longitude}";
                return location;
            }
            set {
                location = value;
            }
        }

        public override int GetHashCode() {
            HashCode hash = new HashCode();
            hash.Add(Age);
            hash.Add(EyeColor);
            hash.Add(Name);
            hash.Add(Gender);
            hash.Add(Company);
            hash.Add(Email);
            hash.Add(Phone);
            hash.Add(Address);
            hash.Add(About);
            hash.Add(RegistrationDate);
            hash.Add(Latitude);
            hash.Add(Longitude);
            return hash.ToHashCode();
        }

        public override bool Equals(object obj) {
            return obj is Person person &&
                   Age == person.Age &&
                   EyeColor == person.EyeColor &&
                   Name == person.Name &&
                   Gender == person.Gender &&
                   Company == person.Company &&
                   Email == person.Email &&
                   Phone == person.Phone &&
                   Address == person.Address &&
                   About == person.About &&
                   RegistrationDate == person.RegistrationDate &&
                   Latitude == person.Latitude &&
                   Longitude == person.Longitude;
        }
    }
}
